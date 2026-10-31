# LAB 6 Quản lý hồ sơ bệnh án

## 1. Giới thiệu

Đây là chương trình quản lý hồ sơ bệnh án của bệnh nhân được xây dựng cho bài LAB 6. Hệ thống hỗ trợ quy trình từ đăng ký bệnh nhân lần đầu, cấp hồ sơ bệnh án, tiếp nhận khám, xét nghiệm, lập toa thuốc, thanh toán viện phí đến khi hoàn tất đợt điều trị.

Giao diện được xây dựng hoàn toàn bằng mã C# WinForms. Dự án không sử dụng thao tác kéo thả Control và không phụ thuộc vào các file `Designer.cs`.

## 2. Mục tiêu

- Quản lý tập trung thông tin bệnh nhân và lịch sử điều trị.
- Liên kết đúng phiếu khám, chỉ định xét nghiệm, kết quả xét nghiệm và toa thuốc.
- Quản lý lệ phí năm, lịch khám định kỳ và viện phí.
- Bảo đảm tính toàn vẹn dữ liệu bằng khóa chính, khóa ngoại, `UNIQUE`, `CHECK` và transaction.
- Rollback toàn bộ dữ liệu nếu một thao tác ghi nhiều bảng bị lỗi.

## 3. Công nghệ sử dụng

| Thành phần | Công nghệ |
|---|---|
| Ngôn ngữ | C# |
| Giao diện | Windows Forms viết bằng code |
| Framework | .NET 8.0 |
| Cơ sở dữ liệu | Microsoft SQL Server |
| Công cụ quản lý CSDL | SQL Server Management Studio |
| Thư viện kết nối | Microsoft.Data.SqlClient 6.1.7 |
| Môi trường phát triển | Visual Studio 2022 trở lên |

## 4. Chức năng chính

### 4.1 Đăng nhập

- Xác thực tên đăng nhập và mật khẩu.
- Mật khẩu được băm bằng SHA-256 trước khi so sánh.
- Chỉ tài khoản đang hoạt động mới được đăng nhập.

### 4.2 Quản lý bệnh nhân

- Đăng ký bệnh nhân lần đầu.
- Thu lệ phí năm và cấp hồ sơ bệnh án trong cùng một transaction.
- Lưu ảnh bệnh nhân 3x4 trong SQL Server.
- Cập nhật và tra cứu bệnh nhân theo mã, họ tên hoặc số điện thoại.

### 4.3 Tiếp nhận và khám bệnh

- Chọn hồ sơ bệnh án đang hoạt động.
- Tạo đợt điều trị mới.
- Lập phiếu khám và phân công bác sĩ.
- Ghi triệu chứng, dấu hiệu sinh tồn và chẩn đoán.

### 4.4 Xét nghiệm

- Tạo chỉ định xét nghiệm từ phiếu khám.
- Chọn loại xét nghiệm.
- Nhập kết quả và kết luận xét nghiệm.
- Mỗi chỉ định chỉ có tối đa một kết quả chính thức.

### 4.5 Toa thuốc

- Lập toa thuốc từ phiếu khám.
- Thêm nhiều thuốc vào một toa.
- Kiểm tra số lượng và liều dùng.
- Lưu toa thuốc và toàn bộ chi tiết toa trong cùng một transaction.

### 4.6 Lịch khám định kỳ

- Đặt lịch theo quý và năm.
- Không cho phép một bệnh nhân đặt hai lịch trong cùng một quý.
- Hỗ trợ hủy lịch khám.

### 4.7 Viện phí

- Lập hóa đơn cho đợt điều trị.
- Chuyển trạng thái đợt điều trị sang chờ thanh toán.
- Xác nhận thanh toán và hoàn tất đợt điều trị.
- Không cho phép hoàn tất đợt điều trị khi hóa đơn chưa thanh toán.

## 5. Cấu trúc dự án

```text
QuanLyBenhAn
├── Data
│   └── Db.cs
├── Database
│   └── Database.sql
├── Forms
│   ├── LoginForm.cs
│   ├── MainForm.cs
│   ├── PatientForm.cs
│   ├── ExaminationForm.cs
│   ├── LaboratoryForm.cs
│   ├── PrescriptionForm.cs
│   ├── AppointmentForm.cs
│   └── BillingForm.cs
├── Security
│   └── PasswordHasher.cs
├── UI
│   └── Ui.cs
├── Program.cs
└── QuanLyBenhAn.csproj
```

### Ý nghĩa các thư mục

- `Data`: chứa lớp kết nối và thực thi câu lệnh SQL.
- `Database`: chứa script tạo database, bảng, ràng buộc, dữ liệu mẫu và stored procedure.
- `Forms`: chứa toàn bộ màn hình của chương trình.
- `Security`: chứa chức năng băm mật khẩu.
- `UI`: chứa các hàm tạo Control và định dạng giao diện dùng chung.

## 6. Các bảng chính

| Bảng | Nội dung |
|---|---|
| `TaiKhoan` | Tài khoản đăng nhập và vai trò |
| `BenhNhan` | Thông tin hành chính bệnh nhân |
| `HoSoBenhAn` | Hồ sơ bệnh án của bệnh nhân |
| `LePhiNam` | Lệ phí quản lý hằng năm |
| `DotDieuTri` | Các đợt điều trị của hồ sơ |
| `PhieuKham` | Thông tin khám và chẩn đoán |
| `ChiDinhXetNghiem` | Yêu cầu xét nghiệm |
| `KetQuaXetNghiem` | Kết quả xét nghiệm chính thức |
| `ToaThuoc` | Thông tin chung của toa thuốc |
| `ChiTietToaThuoc` | Thuốc, số lượng và liều dùng |
| `LichKhamDinhKy` | Lịch khám theo quý |
| `HoaDonVienPhi` | Hóa đơn của đợt điều trị |

## 7. Stored procedure và transaction

| Stored procedure | Chức năng |
|---|---|
| `sp_DangKyBenhNhanLanDau` | Tạo bệnh nhân, thu lệ phí và cấp hồ sơ |
| `sp_TiepNhanKham` | Tạo đợt điều trị và phiếu khám |
| `sp_TraKetQuaXetNghiem` | Lưu kết quả và cập nhật trạng thái chỉ định |
| `sp_LapToaThuoc` | Lưu toa thuốc và các dòng thuốc |
| `sp_LapHoaDon` | Tạo hóa đơn và chuyển sang chờ thanh toán |
| `sp_ThanhToanVaHoanTat` | Thanh toán và hoàn tất đợt điều trị |

Các stored procedure sử dụng `BEGIN TRAN`, `COMMIT` và `ROLLBACK`. Nếu một câu lệnh trong transaction thất bại, toàn bộ dữ liệu đã ghi trong transaction đó sẽ được hoàn tác.

## 8. Yêu cầu cài đặt

- Windows 10 hoặc Windows 11.
- Visual Studio 2022 phiên bản 17.8 trở lên.
- Workload `.NET desktop development`.
- .NET 8 SDK.
- SQL Server và SQL Server Management Studio.

## 9. Khởi tạo cơ sở dữ liệu

1. Mở SQL Server Management Studio.
2. Kết nối đến SQL Server đang sử dụng.
3. Chọn `New Query`.
4. Mở file `Database/Database.sql`.
5. Chạy toàn bộ script bằng nút `Execute`.
6. Kiểm tra database `Lab6_QuanLyBenhAn` đã xuất hiện.

Script sẽ tạo:

- Database và các bảng cần thiết.
- Khóa chính, khóa ngoại và các ràng buộc dữ liệu.
- Stored procedure cho các nghiệp vụ nhiều bảng.
- Tài khoản quản trị.
- Dữ liệu mẫu gồm khoa, bác sĩ, thuốc và loại xét nghiệm.

## 10. Cấu hình chuỗi kết nối

Mở file `Data/Db.cs` và sửa `Server` cho giống chính xác `Server name` đang sử dụng trong SSMS.

### SQL Server Express

```csharp
public const string ConnectionString =
    @"Server=LOQ\SQLEXPRESS;Database=Lab6_QuanLyBenhAn;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;";
```

### SQL Server LocalDB

```csharp
public const string ConnectionString =
    @"Server=(localdb)\MSSQLLocalDB;Database=Lab6_QuanLyBenhAn;Integrated Security=True;TrustServerCertificate=True;";
```

### SQL Server default instance

```csharp
public const string ConnectionString =
    @"Server=.;Database=Lab6_QuanLyBenhAn;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;";
```

## 11. Chạy chương trình

1. Mở file `QuanLyBenhAn.csproj` bằng Visual Studio.
2. Chờ Visual Studio khôi phục package NuGet.
3. Chọn `Build > Rebuild Solution`.
4. Nhấn `F5` để chạy chương trình.

Tài khoản mẫu:

```text
Tên đăng nhập: admin
Mật khẩu: 123456
```

## 12. Thứ tự kiểm thử đề xuất

1. Đăng nhập bằng tài khoản quản trị.
2. Đăng ký bệnh nhân và cấp hồ sơ bệnh án.
3. Tiếp nhận bệnh nhân, tạo đợt điều trị và phiếu khám.
4. Tạo chỉ định xét nghiệm.
5. Nhập và trả kết quả xét nghiệm.
6. Lập toa thuốc có ít nhất một dòng thuốc.
7. Đặt lịch khám định kỳ.
8. Lập hóa đơn viện phí.
9. Thanh toán và hoàn tất đợt điều trị.
10. Tra cứu lại dữ liệu để kiểm tra liên kết giữa các bảng.

## 13. Một số quy tắc nghiệp vụ

- Số điện thoại bệnh nhân không được trùng.
- Mỗi bệnh nhân chỉ có một hồ sơ bệnh án đang hoạt động.
- Một bệnh nhân chỉ đóng một lệ phí trong cùng một năm.
- Một chỉ định xét nghiệm chỉ có một kết quả chính thức.
- Một phiếu khám chỉ có tối đa một toa thuốc chính thức.
- Toa thuốc phải có ít nhất một thuốc.
- Số lượng thuốc phải lớn hơn 0 và liều dùng không được để trống.
- Một bệnh nhân chỉ có một lịch khám trong cùng quý và năm.
- Đợt điều trị chỉ được hoàn tất sau khi hóa đơn đã thanh toán.

## 14. Kiểm tra dữ liệu bằng SSMS

```sql
USE Lab6_QuanLyBenhAn;

SELECT * FROM BenhNhan;
SELECT * FROM HoSoBenhAn;
SELECT * FROM LePhiNam;
SELECT * FROM DotDieuTri;
SELECT * FROM PhieuKham;
SELECT * FROM ChiDinhXetNghiem;
SELECT * FROM KetQuaXetNghiem;
SELECT * FROM ToaThuoc;
SELECT * FROM ChiTietToaThuoc;
SELECT * FROM LichKhamDinhKy;
SELECT * FROM HoaDonVienPhi;
```

## 15. Hình ảnh giao diện cần chụp

- Form đăng nhập.
- Form đăng ký bệnh nhân lần đầu.
- Form quản lý và tra cứu bệnh nhân.
- Form tiếp nhận và khám bệnh.
- Form chỉ định và trả kết quả xét nghiệm.
- Form lập toa thuốc.
- Form lịch khám định kỳ.
- Form hóa đơn và thanh toán viện phí.

Các ảnh này có thể được đưa vào mục hình ảnh giao diện trong báo cáo Word của LAB 6.

## 16. Thông tin sinh viên

```text
Họ và tên: ................................................
MSSV: ......................................................
Lớp: .......................................................
Nhóm thực hành: ............................................
```

## 17. Ghi chú

Dự án được xây dựng cho mục đích học tập và thực hành phân tích thiết kế hệ thống, SQL Server, transaction và Windows Forms. Trước khi chạy trên máy khác, cần cập nhật lại chuỗi kết nối SQL Server trong `Data/Db.cs`.
