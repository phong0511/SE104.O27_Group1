﻿CREATE TABLE NhanVien
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