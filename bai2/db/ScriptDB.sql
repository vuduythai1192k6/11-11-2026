CREATE DATABASE HRMDB;
GO
USE HRMDB;
GO

CREATE TABLE PhongBan (
    MaPB INT IDENTITY(1,1) PRIMARY KEY,
    TenPB NVARCHAR(100) NOT NULL,
    Mota NVARCHAR(255)
);

CREATE TABLE NhanVien (
    MaNV INT IDENTITY(1,1) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    NgaySinh DATETIME,
    CCCD VARCHAR(20) UNIQUE,
    MaPB INT FOREIGN KEY REFERENCES PhongBan(MaPB),
    LuongCoBan DECIMAL(18,2) CHECK (LuongCoBan > 0),
    HeSoLuong FLOAT DEFAULT 1.0
);

CREATE TABLE BangChamCong (
    MaCong INT IDENTITY(1,1) PRIMARY KEY,
    MaNV INT FOREIGN KEY REFERENCES NhanVien(MaNV),
    ThangNam VARCHAR(7) NOT NULL,
    SoNgayCong FLOAT DEFAULT 0 CHECK (SoNgayCong >= 0 AND SoNgayCong <= 31),
    SoGioOT FLOAT DEFAULT 0 CHECK (SoGioOT >= 0),
    DaChot BIT DEFAULT 0,
    CONSTRAINT UQ_ChamCong UNIQUE (MaNV, ThangNam)
);

CREATE TABLE BangLuong (
    MaLuong INT IDENTITY(1,1) PRIMARY KEY,
    MaNV INT FOREIGN KEY REFERENCES NhanVien(MaNV),
    ThangNam VARCHAR(7) NOT NULL,
    ThucLinh DECIMAL(18,2),
    CONSTRAINT UQ_BangLuong UNIQUE (MaNV, ThangNam)
);
GO

INSERT INTO PhongBan (TenPB, Mota) VALUES 
(N'Phòng Kỹ thuật Phần mềm', N'Phát triển hệ thống ERP & Core'),
(N'Phòng Kế toán - Hành chính', N'Quản lý tài chính và nhân sự');

INSERT INTO NhanVien (HoTen, NgaySinh, CCCD, MaPB, LuongCoBan, HeSoLuong) VALUES 
(N'Nguyễn Minh Quân', '2005-01-01', '001205001234', 1, 10000000, 2.34),
(N'Phạm Minh Tuấn', '2000-05-15', '001200005678', 1, 12000000, 2.67),
(N'Hoàng Thu Trang', '2002-09-20', '001202009876', 2, 9000000, 1.86);

INSERT INTO BangChamCong (MaNV, ThangNam, SoNgayCong, SoGioOT, DaChot) VALUES 
(1, '11/2026', 22, 10, 0),
(2, '11/2026', 20, 5, 0),
(3, '11/2026', 22, 0, 0);
GO
