CREATE TABLE NHANVIEN
(
	MANV 	CHAR(4) PRIMARY KEY,
	HOTEN	VARCHAR(40),
	EMAIL	VARCHAR(50),
	SODT	VARCHAR(20),
	NGSINH	SMALLDATETIME,
	LVL		SMALLINT,
	MACM	CHAR(4),
	GHICHU	VARCHAR(80),
);
CREATE TABLE CHUYENMON
(
	MACM	CHAR(4) PRIMARY KEY,
	TENCM	VARCHAR(15),
);
CREATE TABLE TAIKHOAN
(
	MATK	CHAR(4) PRIMARY KEY,
	MANV	CHAR(4),
	MAQH	CHAR(4),
	EMAIL	VARCHAR(50),
	PASS	VARCHAR(20),
);
CREATE TABLE QUYENHAN
(
	MAQH	CHAR(4) PRIMARY KEY,
	TENQH	VARCHAR(15),
);
CREATE TABLE LOAISK
(
	MALSK	CHAR(4) PRIMARY KEY,
	TENLSK	VARCHAR(20),
	MONEYMIN	MONEY,
	MONEYMAX	MONEY,
);
CREATE TABLE DUAN
(
	MADA	CHAR(4) PRIMARY KEY,
	MALSK	CHAR(4),
	MAOWNER	CHAR(4),
	TENDA	VARCHAR(80),
	NGANSACH	MONEY,
	TSTART	SMALLDATETIME,
	TEND	SMALLDATETIME,
	STAT	VARCHAR(15),

);
CREATE TABLE CONGVIEC
(
	MACV	CHAR(4) PRIMARY KEY,
	MADA	CHAR(4),
	MANV	CHAR(4),
	MACM	CHAR(4),
	TENCV	VARCHAR (80),
	TSTART	SMALLDATETIME,
	TEND	SMALLDATETIME,
	NGANSACH	MONEY,
	DADUNG	MONEY,
	TIENDO	SMALLINT,
	YCDINHKEM	VARCHAR(20),
	TEPDINHKEM	VARCHAR(50),
);
--------------------------------------------------------------------------------
--TẠO TRIGGER CHO ĐIỀU KIỆN NGÂN SÁCH
CREATE OR ALTER TRIGGER Check_NGANSACH
ON DUAN
AFTER INSERT, UPDATE
AS
BEGIN
    -- Khai báo các biến
    DECLARE @MALSK CHAR(10);
    DECLARE @MONEYMIN INT;
    DECLARE @MONEYMAX INT;
    DECLARE @TENLSK NVARCHAR(100);

    -- Lặp qua các hàng được chèn hoặc cập nhật
    DECLARE cur CURSOR FOR
    SELECT i.MALSK
    FROM inserted i;

    OPEN cur;
    FETCH NEXT FROM cur INTO @MALSK;

    WHILE @@FETCH_STATUS = 0
    BEGIN
        -- Lấy giá trị MONEYMIN, MONEYMAX và TENLSK từ bảng LOAISK tương ứng với MALSK
        SELECT @MONEYMIN = MONEYMIN, @MONEYMAX = MONEYMAX, @TENLSK = TENLSK
        FROM LOAISK
        WHERE MALSK = @MALSK;

        -- Kiểm tra điều kiện và nếu không thỏa mãn, phát sinh lỗi
        IF EXISTS (
            SELECT 1
            FROM inserted i
            WHERE i.NGANSACH > @MONEYMAX OR i.NGANSACH < @MONEYMIN
        )
        BEGIN
            DECLARE @ErrorMessage NVARCHAR(100);
            SET @ErrorMessage = N'Ngân sách phải nằm trong khoảng ' + CAST(@MONEYMIN AS NVARCHAR(10)) + N' và ' + CAST(@MONEYMAX AS NVARCHAR(10)) + N' cho sự kiện ' + @TENLSK;
            RAISERROR(@ErrorMessage, 16, 1);
            ROLLBACK TRANSACTION;
            CLOSE cur;
            DEALLOCATE cur;
            RETURN;
        END;

        FETCH NEXT FROM cur INTO @MALSK;
    END;

    CLOSE cur;
    DEALLOCATE cur;
END;
----------------------------------------------------------------------------
-- TÌNH TRẠNG DỰ ÁN
ALTER TABLE DUAN
ADD TINHTRANG varchar(20);

ALTER TABLE DUAN ADD CONSTRAINT Check_TinhTrang CHECK (TINHTRANG in ('Completed','On-going','Delayed','Canceled'))

-- NGÂN SÁCH CÔNG VIỆC
ALTER TABLE CONGVIEC ADD CONSTRAINT Check_NganSachCV CHECK (NGANSACH >=0) 

-- KIỂM TRA TỔNG NGÂN SÁCH CÔNG VIỆC
CREATE OR ALTER TRIGGER Check_CongViec_Ngansach
ON DUAN
AFTER INSERT, UPDATE
AS
BEGIN
    -- Khai báo biến để lưu trữ NGANSACH của DUAN
    DECLARE @DuanNgansach INT;

    -- Lấy NGANSACH của DUAN
    SELECT @DuanNgansach = NGANSACH FROM DUAN WHERE MADA IN (SELECT MADA FROM inserted);

    -- Kiểm tra tổng NGANSACH của CÔNG VIỆC so với NGANSACH của DỰ ÁN
    IF EXISTS (
        SELECT 1
        FROM inserted i
        INNER JOIN (
            SELECT MADA, SUM(NGANSACH) AS TotalNgansach
            FROM CONGVIEC
            GROUP BY MADA
        ) cv ON i.MADA = cv.MADA
        WHERE cv.TotalNgansach > @DuanNgansach
    )
    BEGIN
        DECLARE @ErrorMessage NVARCHAR(100);
        SET @ErrorMessage = N'Tổng ngân sách cho công việc không được vượt quá ' + CAST(@DuanNgansach AS NVARCHAR(10));
        RAISERROR (@ErrorMessage, 16, 1);
        ROLLBACK TRANSACTION;
    END;
END;

-- KIỂM TRA NGÂN SÁCH ĐÃ DÙNG SO VỚI NGÂN SÁCH CÔNG VIỆC
CREATE OR ALTER TRIGGER Check_CongViec_Ngansach_Dung
ON CONGVIEC
AFTER INSERT, UPDATE
AS
BEGIN
    -- Kiểm tra ngân sách đã dùng so với ngân sách được cấp cho công việc
    IF EXISTS (
        SELECT 1
        FROM inserted i
        WHERE i.DADUNG > i.NGANSACH
           OR i.DADUNG < 0
    )
    BEGIN
        RAISERROR (N'Không được dùng quá ngân sách được cấp cho công việc', 16, 1);
        ROLLBACK TRANSACTION;
    END;
END;

-- KIỂM TRA CÔNG VIỆC ĐÚNG CHUYÊN MÔN CỦA NHÂN VIÊN
CREATE OR ALTER TRIGGER Check_PhanCong_ChuyenMon
ON CONGVIEC
AFTER INSERT, UPDATE
AS
BEGIN
    -- Kiểm tra phân công công việc đúng với chuyên môn của nhân viên
    IF EXISTS (
        SELECT 1
        FROM inserted i
        INNER JOIN NHANVIEN nv ON i.MANV = nv.MANV
        INNER JOIN PHANCONG p ON i.MACV = p.MACV
        WHERE nv.MACM <> i.MACM
    )
    BEGIN
        RAISERROR (N'Nhân viên phải được phân công công việc đúng với chuyên môn của mình', 16, 1);
        ROLLBACK TRANSACTION;
    END;
END;

