--TẠO KHOÁ NGOẠI CHO CÁC BẢNG TRONG DATABASE (DEMO 1 CASE)
ALTER TABLE NHANVIEN
ADD CONSTRAINT FK_CHUYENMON_NHANVIEN
FOREIGN KEY(MACM) 
REFERENCES CHUYENMON(MACM)

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
            SET @ErrorMessage = 'NGANSACH must be between ' + CAST(@MONEYMIN AS NVARCHAR(10)) + ' and ' + CAST(@MONEYMAX AS NVARCHAR(10)) + ' for ' + @TENLSK;
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

-- XOÁ TRIGGER
DROP TRIGGER IF EXISTS Check_NGANSACH;

-- INSERT DATA ĐỂ KIỂM TRA TRIGGER
INSERT INTO DUAN (MADA,MALSK,NGANSACH)
VALUES ('DA2','SMNR',10000000000)

-- XOÁ CÁC DATA DÙNG ĐỂ KIỂM TRA TRIGGER
DELETE FROM DUAN

-- MỘT SỐ LỆNH SELECT
SELECT *
FROM DUAN

SELECT *
FROM LOAISK

SELECT *
FROM CONGVIEC
