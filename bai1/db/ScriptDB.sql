CREATE DATABASE SalesDB;
GO
USE SalesDB;
GO

CREATE TABLE NhanVien (
    MaNV INT IDENTITY(1,1) PRIMARY KEY,
    TenNV NVARCHAR(100) NOT NULL,
    SoDienThoai VARCHAR(15) UNIQUE,
    ChucVu NVARCHAR(50)
);

CREATE TABLE KhachHang (
    MaKH INT IDENTITY(1,1) PRIMARY KEY,
    TenKH NVARCHAR(100) NOT NULL,
    DienThoai VARCHAR(15) UNIQUE,
    DiemTichLuy INT DEFAULT 0
);

CREATE TABLE SanPham (
    MaSP VARCHAR(20) PRIMARY KEY,
    TenSP NVARCHAR(150) NOT NULL,
    GiaBan DECIMAL(18,2) CHECK (GiaBan > 0),
    SoLuongTon INT CHECK (SoLuongTon >= 0)
);

CREATE TABLE HoaDon (
    MaHD INT IDENTITY(1,1) PRIMARY KEY,
    NgayLap DATETIME DEFAULT GETDATE(),
    MaNV INT FOREIGN KEY REFERENCES NhanVien(MaNV),
    MaKH INT NULL FOREIGN KEY REFERENCES KhachHang(MaKH),
    TongTien DECIMAL(18,2)
);

CREATE TABLE ChiTietHoaDon (
    MaHD INT FOREIGN KEY REFERENCES HoaDon(MaHD),
    MaSP VARCHAR(20) FOREIGN KEY REFERENCES SanPham(MaSP),
    SoLuong INT CHECK (SoLuong > 0),
    DonGia DECIMAL(18,2),
    PRIMARY KEY (MaHD, MaSP)
);
GO

INSERT INTO NhanVien (TenNV, SoDienThoai, ChucVu) VALUES 
(N'Nguyễn Minh Quân', '0901234567', N'Quản lý'),
(N'Nguyễn Văn An', '0912345678', N'Thu ngân');

INSERT INTO KhachHang (TenKH, DienThoai, DiemTichLuy) VALUES 
(N'Trần Thị Bình', '0987654321', 15),
(N'Lê Văn Cường', '0977889900', 5);

INSERT INTO SanPham (MaSP, TenSP, GiaBan, SoLuongTon) VALUES 
('SP001', N'Sữa tươi Vinamilk 1L', 35000, 50),
('SP002', N'Mì Hảo Hảo Tôm Chua Cay (Thùng)', 115000, 30),
('SP003', N'Nước ngọt Coca Cola 1.5L', 22000, 100),
('SP004', N'Dầu ăn Neptune 2L', 98000, 15),
('SP005', N'Gạo ST25 Túi 5Kg', 185000, 20);
GO
