/* ============================================================
   BAI 6 - QUAN LY CONG TY DU LICH VAN HOA VIET
   SQL Server / LocalDB
   Database: QuanLyCongTyDuLich

   Quy uoc:
   - Khach le: 1-11 nguoi, thanh toan khi dang ky.
   - Khach doan: tren 12 nguoi, dat coc va thanh toan sau tour.
   - Truong hop dung 12 nguoi chua duoc de bai quy dinh.
   - Chay lai script se xoa cac bang cu trong database nay.
   ============================================================ */

IF DB_ID(N'QuanLyCongTyDuLich') IS NULL
    CREATE DATABASE QuanLyCongTyDuLich;
GO

USE QuanLyCongTyDuLich;
GO

/* Xoa bang theo thu tu phu thuoc khoa ngoai */
DROP TABLE IF EXISTS dbo.KhaoSat;
DROP TABLE IF EXISTS dbo.ThanhToanDoan;
DROP TABLE IF EXISTS dbo.PhanCongHDV;
DROP TABLE IF EXISTS dbo.DangKyLe;
DROP TABLE IF EXISTS dbo.ThanhVienDoan;
DROP TABLE IF EXISTS dbo.DangKyDoan;
DROP TABLE IF EXISTS dbo.DoanKhach;
DROP TABLE IF EXISTS dbo.ChuyenLe;
DROP TABLE IF EXISTS dbo.TourDiemThamQuan;
DROP TABLE IF EXISTS dbo.TourPhuongTien;
DROP TABLE IF EXISTS dbo.TourDiemDung;
DROP TABLE IF EXISTS dbo.HuongDanVien;
DROP TABLE IF EXISTS dbo.DiemBanVe;
DROP TABLE IF EXISTS dbo.DiemThamQuan;
DROP TABLE IF EXISTS dbo.PhuongTien;
DROP TABLE IF EXISTS dbo.Tour;
GO

CREATE TABLE dbo.Tour
(
    MaTour          VARCHAR(20) NOT NULL CONSTRAINT PK_Tour PRIMARY KEY,
    TenTour         NVARCHAR(180) NOT NULL,
    SoNgay          INT NOT NULL,
    SoDem           INT NOT NULL,
    DonGiaKhach     DECIMAL(18,2) NOT NULL,
    MoTa            NVARCHAR(1000) NULL,
    DangMoBan       BIT NOT NULL CONSTRAINT DF_Tour_DangMoBan DEFAULT (1),
    CONSTRAINT CK_Tour_SoNgay CHECK (SoNgay > 0),
    CONSTRAINT CK_Tour_SoDem CHECK (SoDem >= 0 AND SoDem < SoNgay),
    CONSTRAINT CK_Tour_DonGia CHECK (DonGiaKhach >= 0)
);

CREATE TABLE dbo.PhuongTien
(
    MaPT        VARCHAR(20) NOT NULL CONSTRAINT PK_PhuongTien PRIMARY KEY,
    TenPT       NVARCHAR(120) NOT NULL CONSTRAINT UQ_PhuongTien_Ten UNIQUE,
    GhiChu      NVARCHAR(300) NULL
);

CREATE TABLE dbo.DiemThamQuan
(
    MaDiemTQ    VARCHAR(20) NOT NULL CONSTRAINT PK_DiemThamQuan PRIMARY KEY,
    TenDiemTQ   NVARCHAR(180) NOT NULL,
    DiaDiem     NVARCHAR(250) NOT NULL,
    NoiDung     NVARCHAR(1000) NULL,
    YNghia      NVARCHAR(1000) NULL
);

CREATE TABLE dbo.DiemBanVe
(
    MaDiemBan   VARCHAR(20) NOT NULL CONSTRAINT PK_DiemBanVe PRIMARY KEY,
    TenDiemBan  NVARCHAR(150) NOT NULL,
    DiaChi      NVARCHAR(250) NOT NULL,
    DienThoai   VARCHAR(20) NULL
);

CREATE TABLE dbo.HuongDanVien
(
    MaHDV           VARCHAR(20) NOT NULL CONSTRAINT PK_HuongDanVien PRIMARY KEY,
    HoTen           NVARCHAR(120) NOT NULL,
    DienThoai       VARCHAR(20) NULL,
    LuongCoBan      DECIMAL(18,2) NOT NULL,
    DangLamViec     BIT NOT NULL CONSTRAINT DF_HDV_DangLam DEFAULT (1),
    CONSTRAINT CK_HDV_Luong CHECK (LuongCoBan >= 0)
);

CREATE TABLE dbo.TourDiemDung
(
    MaTour              VARCHAR(20) NOT NULL,
    ThuTu               INT NOT NULL,
    TenDiemDung         NVARCHAR(180) NOT NULL,
    DoiPhuongTien       BIT NOT NULL CONSTRAINT DF_TDD_DoiPT DEFAULT (0),
    CoNoiAn             BIT NOT NULL CONSTRAINT DF_TDD_NoiAn DEFAULT (0),
    CoKhachSan          BIT NOT NULL CONSTRAINT DF_TDD_KhachSan DEFAULT (0),
    HangSaoKhachSan     INT NULL,
    GhiChu              NVARCHAR(500) NULL,
    CONSTRAINT PK_TourDiemDung PRIMARY KEY (MaTour, ThuTu),
    CONSTRAINT FK_TDD_Tour FOREIGN KEY (MaTour) REFERENCES dbo.Tour(MaTour),
    CONSTRAINT CK_TDD_ThuTu CHECK (ThuTu > 0),
    CONSTRAINT CK_TDD_HangSao CHECK
    (
        (CoKhachSan = 0 AND HangSaoKhachSan IS NULL)
        OR
        (CoKhachSan = 1 AND HangSaoKhachSan BETWEEN 2 AND 5)
    )
);

CREATE TABLE dbo.TourPhuongTien
(
    MaTour          VARCHAR(20) NOT NULL,
    ThuTuChang      INT NOT NULL,
    MaPT            VARCHAR(20) NOT NULL,
    GhiChu          NVARCHAR(300) NULL,
    CONSTRAINT PK_TourPhuongTien PRIMARY KEY (MaTour, ThuTuChang, MaPT),
    CONSTRAINT FK_TPT_Tour FOREIGN KEY (MaTour) REFERENCES dbo.Tour(MaTour),
    CONSTRAINT FK_TPT_PhuongTien FOREIGN KEY (MaPT) REFERENCES dbo.PhuongTien(MaPT),
    CONSTRAINT CK_TPT_ThuTu CHECK (ThuTuChang > 0)
);

CREATE TABLE dbo.TourDiemThamQuan
(
    MaTour      VARCHAR(20) NOT NULL,
    MaDiemTQ    VARCHAR(20) NOT NULL,
    ThuTu       INT NOT NULL,
    CONSTRAINT PK_TourDiemThamQuan PRIMARY KEY (MaTour, MaDiemTQ),
    CONSTRAINT UQ_TDTQ_Tour_ThuTu UNIQUE (MaTour, ThuTu),
    CONSTRAINT FK_TDTQ_Tour FOREIGN KEY (MaTour) REFERENCES dbo.Tour(MaTour),
    CONSTRAINT FK_TDTQ_Diem FOREIGN KEY (MaDiemTQ) REFERENCES dbo.DiemThamQuan(MaDiemTQ),
    CONSTRAINT CK_TDTQ_ThuTu CHECK (ThuTu > 0)
);

CREATE TABLE dbo.ChuyenLe
(
    MaChuyen        VARCHAR(20) NOT NULL CONSTRAINT PK_ChuyenLe PRIMARY KEY,
    MaTour          VARCHAR(20) NOT NULL,
    NgayDi          DATE NOT NULL,
    NgayVe          DATE NOT NULL,
    DiaDiemDon      NVARCHAR(250) NOT NULL,
    TrangThai       NVARCHAR(40) NOT NULL CONSTRAINT DF_ChuyenLe_TrangThai DEFAULT (N'Mở đăng ký'),
    CONSTRAINT FK_ChuyenLe_Tour FOREIGN KEY (MaTour) REFERENCES dbo.Tour(MaTour),
    CONSTRAINT CK_ChuyenLe_Ngay CHECK (NgayVe >= NgayDi),
    CONSTRAINT CK_ChuyenLe_TrangThai CHECK (TrangThai IN (N'Mở đăng ký', N'Đóng đăng ký'))
);

CREATE TABLE dbo.DoanKhach
(
    MaDoan                  VARCHAR(20) NOT NULL CONSTRAINT PK_DoanKhach PRIMARY KEY,
    TenCoQuanDaiDien        NVARCHAR(180) NOT NULL,
    DiaChi                  NVARCHAR(250) NOT NULL,
    DienThoai               VARCHAR(20) NOT NULL,
    NguoiDaiDien            NVARCHAR(120) NOT NULL
);

CREATE TABLE dbo.DangKyDoan
(
    SoDKDoan                VARCHAR(20) NOT NULL CONSTRAINT PK_DangKyDoan PRIMARY KEY,
    MaDoan                  VARCHAR(20) NOT NULL,
    MaTour                  VARCHAR(20) NOT NULL,
    NgayDangKy              DATETIME2 NOT NULL,
    NgayDi                  DATE NOT NULL,
    NgayKetThucDuKien       DATE NOT NULL,
    SoNguoi                 INT NOT NULL,
    DiaDiemDon              NVARCHAR(250) NOT NULL,
    MuaBaoHiem              BIT NOT NULL CONSTRAINT DF_DKDoan_BaoHiem DEFAULT (0),
    TienCoc                 DECIMAL(18,2) NOT NULL,
    DaThanhToanCoc          BIT NOT NULL,
    TongTienDuKien          DECIMAL(18,2) NOT NULL,
    TrangThai               NVARCHAR(40) NOT NULL CONSTRAINT DF_DKDoan_TrangThai DEFAULT (N'Đã đăng ký'),
    CONSTRAINT FK_DKDoan_Doan FOREIGN KEY (MaDoan) REFERENCES dbo.DoanKhach(MaDoan),
    CONSTRAINT FK_DKDoan_Tour FOREIGN KEY (MaTour) REFERENCES dbo.Tour(MaTour),
    CONSTRAINT CK_DKDoan_SoNguoi CHECK (SoNguoi > 12),
    CONSTRAINT CK_DKDoan_Tien CHECK (TienCoc > 0 AND TongTienDuKien >= TienCoc),
    CONSTRAINT CK_DKDoan_Ngay CHECK (NgayKetThucDuKien >= NgayDi),
    CONSTRAINT CK_DKDoan_TrangThai CHECK (TrangThai IN (N'Đã đăng ký', N'Hủy - mất cọc', N'Đã hoàn tất thanh toán'))
);

CREATE TABLE dbo.ThanhVienDoan
(
    SoDKDoan    VARCHAR(20) NOT NULL,
    STT         INT NOT NULL,
    HoTen       NVARCHAR(120) NOT NULL,
    NgaySinh    DATE NULL,
    SoGiayTo    NVARCHAR(40) NULL,
    CONSTRAINT PK_ThanhVienDoan PRIMARY KEY (SoDKDoan, STT),
    CONSTRAINT FK_ThanhVienDoan_DangKyDoan FOREIGN KEY (SoDKDoan) REFERENCES dbo.DangKyDoan(SoDKDoan),
    CONSTRAINT CK_ThanhVienDoan_STT CHECK (STT > 0)
);

CREATE TABLE dbo.DangKyLe
(
    SoDKLe          VARCHAR(20) NOT NULL CONSTRAINT PK_DangKyLe PRIMARY KEY,
    MaChuyen        VARCHAR(20) NOT NULL,
    MaDiemBan       VARCHAR(20) NOT NULL,
    NgayDangKy      DATETIME2 NOT NULL,
    TenNguoiDangKy  NVARCHAR(120) NOT NULL,
    DienThoai       VARCHAR(20) NOT NULL,
    SoNguoi         INT NOT NULL,
    ThanhTien       DECIMAL(18,2) NOT NULL,
    DaThanhToan     BIT NOT NULL CONSTRAINT DF_DKLe_DaThanhToan DEFAULT (1),
    TrangThai       NVARCHAR(40) NOT NULL CONSTRAINT DF_DKLe_TrangThai DEFAULT (N'Đã đăng ký'),
    CONSTRAINT FK_DKLe_Chuyen FOREIGN KEY (MaChuyen) REFERENCES dbo.ChuyenLe(MaChuyen),
    CONSTRAINT FK_DKLe_DiemBan FOREIGN KEY (MaDiemBan) REFERENCES dbo.DiemBanVe(MaDiemBan),
    CONSTRAINT CK_DKLe_SoNguoi CHECK (SoNguoi BETWEEN 1 AND 11),
    CONSTRAINT CK_DKLe_ThanhTien CHECK (ThanhTien >= 0),
    CONSTRAINT CK_DKLe_ThanhToan CHECK (DaThanhToan = 1),
    CONSTRAINT CK_DKLe_TrangThai CHECK (TrangThai IN (N'Đã đăng ký', N'Đã hủy'))
);

CREATE TABLE dbo.PhanCongHDV
(
    MaPC            VARCHAR(20) NOT NULL CONSTRAINT PK_PhanCongHDV PRIMARY KEY,
    MaHDV           VARCHAR(20) NOT NULL,
    LoaiDoiTuong    VARCHAR(10) NOT NULL,
    MaChuyen        VARCHAR(20) NULL,
    SoDKDoan        VARCHAR(20) NULL,
    NgayBatDau      DATE NOT NULL,
    NgayKetThuc     DATE NOT NULL,
    ThuLaoTour      DECIMAL(18,2) NOT NULL,
    CONSTRAINT FK_PC_HDV FOREIGN KEY (MaHDV) REFERENCES dbo.HuongDanVien(MaHDV),
    CONSTRAINT FK_PC_Chuyen FOREIGN KEY (MaChuyen) REFERENCES dbo.ChuyenLe(MaChuyen),
    CONSTRAINT FK_PC_Doan FOREIGN KEY (SoDKDoan) REFERENCES dbo.DangKyDoan(SoDKDoan),
    CONSTRAINT CK_PC_Loai CHECK (LoaiDoiTuong IN ('LE', 'DOAN')),
    CONSTRAINT CK_PC_Target CHECK
    (
        (LoaiDoiTuong = 'LE' AND MaChuyen IS NOT NULL AND SoDKDoan IS NULL)
        OR
        (LoaiDoiTuong = 'DOAN' AND SoDKDoan IS NOT NULL AND MaChuyen IS NULL)
    ),
    CONSTRAINT CK_PC_Ngay CHECK (NgayKetThuc >= NgayBatDau),
    CONSTRAINT CK_PC_ThuLao CHECK (ThuLaoTour >= 0)
);

CREATE UNIQUE INDEX UX_PC_ChuyenLe
ON dbo.PhanCongHDV(MaChuyen)
WHERE MaChuyen IS NOT NULL;

CREATE TABLE dbo.ThanhToanDoan
(
    SoTT            VARCHAR(20) NOT NULL CONSTRAINT PK_ThanhToanDoan PRIMARY KEY,
    SoDKDoan        VARCHAR(20) NOT NULL,
    NgayThanhToan   DATETIME2 NOT NULL,
    SoTien          DECIMAL(18,2) NOT NULL,
    GhiChu          NVARCHAR(300) NULL,
    CONSTRAINT FK_TTDoan_DangKyDoan FOREIGN KEY (SoDKDoan) REFERENCES dbo.DangKyDoan(SoDKDoan),
    CONSTRAINT CK_TTDoan_SoTien CHECK (SoTien > 0)
);

CREATE TABLE dbo.KhaoSat
(
    MaKhaoSat       VARCHAR(20) NOT NULL CONSTRAINT PK_KhaoSat PRIMARY KEY,
    LoaiKhach       VARCHAR(10) NOT NULL,
    SoDKLe          VARCHAR(20) NULL,
    SoDKDoan        VARCHAR(20) NULL,
    NgayGui         DATE NOT NULL,
    NgayPhanHoi     DATE NULL,
    DiemDanhGia     INT NULL,
    GopY            NVARCHAR(1500) NULL,
    CONSTRAINT FK_KS_DangKyLe FOREIGN KEY (SoDKLe) REFERENCES dbo.DangKyLe(SoDKLe),
    CONSTRAINT FK_KS_DangKyDoan FOREIGN KEY (SoDKDoan) REFERENCES dbo.DangKyDoan(SoDKDoan),
    CONSTRAINT CK_KS_Loai CHECK (LoaiKhach IN ('LE', 'DOAN')),
    CONSTRAINT CK_KS_Diem CHECK (DiemDanhGia IS NULL OR DiemDanhGia BETWEEN 1 AND 5),
    CONSTRAINT CK_KS_PhanHoi CHECK (NgayPhanHoi IS NULL OR NgayPhanHoi >= NgayGui),
    CONSTRAINT CK_KS_Target CHECK
    (
        (LoaiKhach = 'LE' AND SoDKLe IS NOT NULL AND SoDKDoan IS NULL)
        OR
        (LoaiKhach = 'DOAN' AND SoDKDoan IS NOT NULL AND SoDKLe IS NULL)
    )
);

CREATE UNIQUE INDEX UX_KS_Le ON dbo.KhaoSat(SoDKLe) WHERE SoDKLe IS NOT NULL;
CREATE UNIQUE INDEX UX_KS_Doan ON dbo.KhaoSat(SoDKDoan) WHERE SoDKDoan IS NOT NULL;
CREATE INDEX IX_PC_HDV_Ngay ON dbo.PhanCongHDV(MaHDV, NgayBatDau, NgayKetThuc);
GO

/* ======================== DU LIEU MAU ======================== */

INSERT INTO dbo.Tour(MaTour, TenTour, SoNgay, SoDem, DonGiaKhach, MoTa, DangMoBan)
VALUES
('T001', N'Miền Tây 3 ngày 2 đêm', 3, 2, 2500000, N'TP.HCM - Mỹ Tho - Cần Thơ - TP.HCM', 1),
('T002', N'Đà Lạt 4 ngày 3 đêm', 4, 3, 3200000, N'TP.HCM - Đà Lạt - TP.HCM', 1),
('T003', N'Hà Nội - Hạ Long 5 ngày 4 đêm', 5, 4, 8900000, N'TP.HCM - Hà Nội - Hạ Long - TP.HCM', 1);

INSERT INTO dbo.PhuongTien(MaPT, TenPT, GhiChu)
VALUES
('PT01', N'Xe du lịch', NULL),
('PT02', N'Máy bay', NULL),
('PT03', N'Tàu hỏa', NULL),
('PT04', N'Tàu thủy', NULL);

INSERT INTO dbo.DiemBanVe(MaDiemBan, TenDiemBan, DiaChi, DienThoai)
VALUES
('DB01', N'Điểm bán Quận 1', N'12 Lê Lợi, Quận 1, TP.HCM', '0281000001'),
('DB02', N'Điểm bán Thủ Đức', N'5 Võ Văn Ngân, TP. Thủ Đức', '0281000002');

INSERT INTO dbo.HuongDanVien(MaHDV, HoTen, DienThoai, LuongCoBan, DangLamViec)
VALUES
('HDV01', N'Nguyễn Minh Anh', '0903000001', 9000000, 1),
('HDV02', N'Trần Quốc Bình', '0903000002', 9500000, 1),
('HDV03', N'Lê Thu Cúc', '0903000003', 8500000, 1);

INSERT INTO dbo.DiemThamQuan(MaDiemTQ, TenDiemTQ, DiaDiem, NoiDung, YNghia)
VALUES
('DTQ01', N'Chợ nổi Cái Răng', N'Cần Thơ', N'Tham quan chợ trên sông', N'Nét văn hóa sông nước miền Tây'),
('DTQ02', N'Chùa Vĩnh Tràng', N'Mỹ Tho, Tiền Giang', N'Tham quan kiến trúc chùa', N'Di tích kiến trúc nghệ thuật cấp quốc gia'),
('DTQ03', N'Hồ Xuân Hương', N'Đà Lạt', N'Dạo quanh hồ trung tâm', N'Biểu tượng thành phố Đà Lạt'),
('DTQ04', N'Vịnh Hạ Long', N'Quảng Ninh', N'Du thuyền tham quan vịnh', N'Di sản thiên nhiên thế giới'),
('DTQ05', N'Văn Miếu - Quốc Tử Giám', N'Hà Nội', N'Tham quan di tích', N'Trường đại học đầu tiên của Việt Nam');

INSERT INTO dbo.TourDiemDung(MaTour, ThuTu, TenDiemDung, DoiPhuongTien, CoNoiAn, CoKhachSan, HangSaoKhachSan, GhiChu)
VALUES
('T001', 1, N'Mỹ Tho', 0, 1, 0, NULL, NULL),
('T001', 2, N'Cần Thơ', 0, 1, 1, 3, NULL),
('T001', 3, N'TP.HCM', 0, 0, 0, NULL, N'Kết thúc tour'),
('T002', 1, N'Đà Lạt', 0, 1, 1, 3, NULL),
('T002', 2, N'TP.HCM', 0, 0, 0, NULL, N'Kết thúc tour'),
('T003', 1, N'Hà Nội', 1, 1, 1, 4, N'Đổi sang xe du lịch'),
('T003', 2, N'Hạ Long', 1, 1, 1, 5, N'Đi tàu thủy trên vịnh'),
('T003', 3, N'TP.HCM', 0, 0, 0, NULL, N'Kết thúc tour');

INSERT INTO dbo.TourPhuongTien(MaTour, ThuTuChang, MaPT, GhiChu)
VALUES
('T001', 1, 'PT01', NULL),
('T001', 2, 'PT01', NULL),
('T001', 3, 'PT01', NULL),
('T002', 1, 'PT01', NULL),
('T002', 2, 'PT01', NULL),
('T003', 1, 'PT02', N'TP.HCM - Hà Nội'),
('T003', 2, 'PT01', N'Hà Nội - Hạ Long'),
('T003', 2, 'PT04', N'Tham quan vịnh'),
('T003', 3, 'PT02', N'Hà Nội - TP.HCM');

INSERT INTO dbo.TourDiemThamQuan(MaTour, MaDiemTQ, ThuTu)
VALUES
('T001', 'DTQ02', 1),
('T001', 'DTQ01', 2),
('T002', 'DTQ03', 1),
('T003', 'DTQ05', 1),
('T003', 'DTQ04', 2);

INSERT INTO dbo.ChuyenLe(MaChuyen, MaTour, NgayDi, NgayVe, DiaDiemDon, TrangThai)
VALUES
('CL001', 'T001', '20260905', '20260907', N'Nhà Văn hóa Thanh Niên, Quận 1', N'Đóng đăng ký'),
('CL002', 'T001', '20261115', '20261117', N'Nhà Văn hóa Thanh Niên, Quận 1', N'Mở đăng ký'),
('CL003', 'T002', '20261120', '20261123', N'Công viên 23/9, Quận 1', N'Mở đăng ký');

INSERT INTO dbo.DangKyLe(SoDKLe, MaChuyen, MaDiemBan, NgayDangKy, TenNguoiDangKy, DienThoai, SoNguoi, ThanhTien, DaThanhToan, TrangThai)
VALUES
('DKL001', 'CL001', 'DB01', '20260820T09:00:00', N'Phạm Văn Long', '0912000001', 2, 5000000, 1, N'Đã đăng ký'),
('DKL002', 'CL002', 'DB02', '20261001T10:00:00', N'Võ Thị Mai', '0912000002', 3, 7500000, 1, N'Đã đăng ký');

INSERT INTO dbo.DoanKhach(MaDoan, TenCoQuanDaiDien, DiaChi, DienThoai, NguoiDaiDien)
VALUES
('DK01', N'Công ty CP Phần mềm Sao Việt', N'25 Nguyễn Thị Minh Khai, Quận 3, TP.HCM', '0283900001', N'Lê Văn Hải'),
('DK02', N'Gia đình ông Trần Văn Nam', N'8 Phan Xích Long, Phú Nhuận, TP.HCM', '0909111222', N'Trần Văn Nam');

INSERT INTO dbo.DangKyDoan(SoDKDoan, MaDoan, MaTour, NgayDangKy, NgayDi, NgayKetThucDuKien, SoNguoi, DiaDiemDon, MuaBaoHiem, TienCoc, DaThanhToanCoc, TongTienDuKien, TrangThai)
VALUES
('DD001', 'DK01', 'T001', '20260801T08:30:00', '20260910', '20260912', 20, N'25 Nguyễn Thị Minh Khai, Quận 3', 0, 10000000, 1, 50000000, N'Đã đăng ký'),
('DD002', 'DK02', 'T002', '20260925T14:00:00', '20261210', '20261213', 15, N'8 Phan Xích Long, Phú Nhuận', 0, 12000000, 1, 48000000, N'Đã đăng ký');

INSERT INTO dbo.ThanhVienDoan(SoDKDoan, STT, HoTen, NgaySinh, SoGiayTo)
VALUES
('DD001', 1, N'Lê Văn Hải', '19800101', N'079000000001'),
('DD001', 2, N'Nguyễn Thị Lan', '19850512', N'079000000002'),
('DD002', 1, N'Trần Văn Nam', '19750120', N'079000000003');

INSERT INTO dbo.PhanCongHDV(MaPC, MaHDV, LoaiDoiTuong, MaChuyen, SoDKDoan, NgayBatDau, NgayKetThuc, ThuLaoTour)
VALUES
('PC001', 'HDV01', 'LE', 'CL001', NULL, '20260905', '20260907', 1500000),
('PC002', 'HDV02', 'DOAN', NULL, 'DD001', '20260910', '20260912', 2000000),
('PC003', 'HDV03', 'DOAN', NULL, 'DD001', '20260910', '20260912', 2000000);

INSERT INTO dbo.ThanhToanDoan(SoTT, SoDKDoan, NgayThanhToan, SoTien, GhiChu)
VALUES
('TT001', 'DD001', '20260912T16:00:00', 40000000, N'Thanh toán phần còn lại sau tour');

INSERT INTO dbo.KhaoSat(MaKhaoSat, LoaiKhach, SoDKLe, SoDKDoan, NgayGui, NgayPhanHoi, DiemDanhGia, GopY)
VALUES
('KS001', 'LE', 'DKL001', NULL, '20260908', '20260910', 5, N'Hướng dẫn viên nhiệt tình'),
('KS002', 'DOAN', NULL, 'DD001', '20260913', '20260914', 4, N'Tour tổ chức tốt');

GO

PRINT N'Đã tạo và nạp dữ liệu mẫu cho CSDL QuanLyCongTyDuLich.';

SELECT TABLE_NAME
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_TYPE = 'BASE TABLE'
ORDER BY TABLE_NAME;
