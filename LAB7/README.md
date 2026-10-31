# LAB 7 – HỆ THỐNG QUẢN LÝ NHÂN SỰ

**Môn học:** Thực hành Phân tích thiết kế hướng đối tượng  
**Đề tài:** Quản lý nhân sự tại Công ty sơn Đông Á  
**Nền tảng dự kiến:** C# WinForms (.NET Framework 4.7.2) – SQL Server  
**Kiến trúc:** 3 lớp `UI → Service → Data`

## 1. Giới thiệu

LAB 7 tập trung phân tích, thiết kế và xây dựng ứng dụng hỗ trợ quản lý nhân sự tại Công ty sơn Đông Á. Hệ thống bao quát quá trình tuyển dụng, thử việc, ký hợp đồng, quản lý quyết định nhân sự, phân công ca làm việc, chấm công, phụ trội, tính lương, khen thưởng và kỷ luật.

Các nghiệp vụ chính trong đề bài:

- **Tuyển dụng – thử việc:** Lập quyết định tuyển dụng, ghi nhận phòng ban, thời gian và mức lương thử việc.
- **Hợp đồng lao động:** Quản lý hợp đồng ngắn hạn và dài hạn; hồ sơ BHXH, BHYT cho hợp đồng dài hạn theo phạm vi đề bài.
- **Quyết định nhân sự:** Ghi nhận quyết định lương, chức vụ, phụ cấp và các lần điều chỉnh.
- **Phân công – chấm công:** Lập kế hoạch ca làm hằng tuần, ghi nhận ngày công và giờ phụ trội; giờ phụ trội tính theo hệ số **1,5** được nêu trong đề.
- **Tính lương:** Tổng hợp thông tin từ quyết định lương, chức vụ, chấm công, phụ trội và các khoản thưởng/phạt, bảo hiểm.
- **Khen thưởng – kỷ luật:** Quản lý quyết định đối với cá nhân hoặc phòng ban; ghi nhận phiếu chi thưởng và phiếu thu phạt tương ứng.

## 2. Nội dung LAB 7

### Phân tích và thiết kế UML

Bài thực hành yêu cầu thực hiện **6 nội dung**:

1. Xác định các Class, thuộc tính, phương thức và thuộc tính định danh (ID).
2. Xác định quan hệ, bản số và xây dựng **Class Diagram**.
3. Xác định Actor, Use Case và xây dựng **Use Case Diagram tổng quát**.
4. Xây dựng **Use Case Diagram chi tiết** quá trình từ nhận thử việc đến ký hợp đồng chính thức.
5. Xây dựng **Activity Diagram** cho quy trình ở mục 4.
6. Xây dựng **Sequence Diagram** cho nghiệp vụ lập quyết định khen thưởng cá nhân hoặc tập thể.

### Cơ sở dữ liệu và ứng dụng

- Thiết kế cơ sở dữ liệu SQL Server theo mô hình phân tích.
- Xây dựng ứng dụng WinForms bằng C# trên .NET Framework 4.7.2.
- Tổ chức mã nguồn theo ba lớp UI, Service và Data.
- Xây dựng dữ liệu mẫu, các ca kiểm thử và bảng truy vết giữa yêu cầu – UML – chức năng – cơ sở dữ liệu – kiểm thử.

## 3. Công nghệ sử dụng

| Thành phần | Công nghệ |
|---|---|
| Ngôn ngữ lập trình | C# |
| Ứng dụng giao diện | Windows Forms (WinForms) |
| Framework | .NET Framework 4.7.2 |
| IDE | Visual Studio |
| Hệ quản trị CSDL | Microsoft SQL Server |
| Công cụ quản trị CSDL | SQL Server Management Studio (SSMS) |
| Truy cập CSDL | ADO.NET (`System.Data.SqlClient`) |
| Mô hình hóa UML | StarUML / draw.io |

## 4. Kiến trúc và cấu trúc thư mục

```text
QLNhanSu/
├── Data/
│   └── SqlData.cs                # Kết nối và thao tác với SQL Server
├── Service/
│   └── QuanLyService.cs         # Kiểm tra và xử lý nghiệp vụ
├── UI/
│   ├── FrmMain.cs               # Thanh điều hướng, mở các Form con
│   ├── FrmDuLieuBase.cs         # Giao diện CRUD dùng chung
│   ├── FrmNhanVien.cs
│   ├── FrmPhongBan.cs
│   ├── FrmChucVu.cs
│   ├── FrmTuyenDung.cs
│   ├── FrmHopDong.cs
│   ├── FrmQuyetDinhLuong.cs
│   ├── FrmQuyetDinhChucVu.cs
│   ├── FrmBaoHiem.cs
│   ├── FrmCaLamViec.cs
│   ├── FrmKeHoachPhanCong.cs
│   ├── FrmChiTietPhanCong.cs
│   ├── FrmChamCong.cs
│   ├── FrmChiTietChamCong.cs
│   ├── FrmPhuTroi.cs
│   ├── FrmChiTietPhuTroi.cs
│   ├── FrmBangLuong.cs
│   ├── FrmChiTietLuong.cs
│   ├── FrmKhenThuong.cs
│   ├── FrmPhieuChiKhenThuong.cs
│   ├── FrmKyLuat.cs
│   └── FrmPhieuThuNopPhat.cs
├── Program.cs                   # Điểm khởi chạy ứng dụng
└── README.md
```

> Cấu trúc trên là phương án tổ chức mã nguồn đã thống nhất cho LAB 7; chỉ những tệp đã tạo thực tế trong project mới được coi là đã triển khai.

**Quy tắc thiết kế giao diện:** Mỗi nghiệp vụ có Form riêng trong thư mục `UI`. Các control WinForms được khởi tạo bằng mã C#, **không yêu cầu kéo thả Designer**. `FrmMain` chỉ phục vụ điều hướng; `FrmDuLieuBase` chia sẻ phần bảng dữ liệu và thao tác CRUD.

## 5. Thiết kế cơ sở dữ liệu

**Tên database:** `QLNhanSu`

Thiết kế đề xuất có **21 bảng**:

| Nhóm | Bảng |
|---|---|
| Nhân sự | `NhanVien`, `PhongBan`, `ChucVu` |
| Tuyển dụng – hợp đồng | `QuyetDinhTuyenDung`, `HopDongLaoDong` |
| Quyết định – bảo hiểm | `QuyetDinhLuong`, `QuyetDinhChucVu`, `BaoHiem` |
| Phân công – chấm công | `CaLamViec`, `KeHoachPhanCong`, `ChiTietPhanCong`, `BangChamCong`, `ChiTietChamCong` |
| Phụ trội | `PhieuPhuTroi`, `ChiTietPhuTroi` |
| Bảng lương | `BangLuong`, `ChiTietLuong` |
| Khen thưởng – kỷ luật | `QuyetDinhKhenThuong`, `QuyetDinhKyLuat`, `PhieuChiKhenThuong`, `PhieuThuNopPhat` |

Các bảng liên kết với nhau bằng **Primary Key (PK)** và **Foreign Key (FK)**. Những quy tắc phức tạp như hợp đồng đủ điều kiện bảo hiểm, mức lương áp dụng theo thời điểm và xử lý phiếu thưởng/phạt cần tiếp tục kiểm tra ở lớp Service.

## 6. Hướng dẫn chạy project

### 6.1. Chuẩn bị

- Cài **Visual Studio** với workload phát triển ứng dụng desktop .NET.
- Có **.NET Framework 4.7.2 Developer Pack/Targeting Pack** khi tạo hoặc build project.
- Cài **SQL Server** và **SSMS**; đảm bảo tài khoản hiện tại có quyền sử dụng database.

### 6.2. Tạo cơ sở dữ liệu

1. Mở SSMS và kết nối tới SQL Server.
2. Tạo database tên `QLNhanSu`.
3. Chạy script tạo **21 bảng**, khóa chính và khóa ngoại (script SQL của bài thực hành).
4. Chạy script thêm dữ liệu mẫu, nếu có.
5. Kiểm tra bảng trong `Databases → QLNhanSu → Tables`.

Kiểm tra nhanh số bảng:

```sql
USE QLNhanSu;
SELECT COUNT(*) AS SoLuongBang
FROM sys.tables
WHERE name <> 'sysdiagrams';
```

**Kết quả mong đợi:** `21` bảng, nếu đã tạo đủ theo thiết kế.

### 6.3. Cấu hình chuỗi kết nối

Mở `Data/SqlData.cs` và điều chỉnh `Data Source` trùng với tên SQL Server trong SSMS:

```csharp
private readonly string connectionString =
    @"Data Source=.\SQLEXPRESS;
      Initial Catalog=QLNhanSu;
      Integrated Security=True;
      TrustServerCertificate=True;";
```

Ví dụ `Data Source` có thể là `localhost`, `.\SQLEXPRESS` hoặc tên instance trên máy bạn. Không sử dụng nguyên giá trị mẫu nếu máy đang kết nối server khác.

### 6.4. Chạy ứng dụng

1. Mở solution/project `QLNhanSu` trong Visual Studio.
2. Đảm bảo `Program.cs` khởi chạy `new FrmMain()`.
3. Nhấn **Ctrl + Shift + B** để build.
4. Nhấn **F5** để chạy.
5. Chọn các chức năng trên menu để mở từng Form và kiểm tra dữ liệu từ SQL Server.

## 7. Phạm vi triển khai và kiểm thử

Phiên bản giao diện được hướng dẫn trong LAB 7 là **khung ứng dụng WinForms với các Form riêng**, giao diện tạo bằng code và các thao tác thêm/sửa/xóa/xem dữ liệu qua lớp dùng chung.

Các nghiệp vụ chuyên biệt như **duyệt kết quả thử việc, ký hợp đồng, đăng ký bảo hiểm, tính lương tự động, tính phụ trội và quy trình phê duyệt khen thưởng/kỷ luật** cần được hiện thực và kiểm thử riêng; không mặc định chúng đã hoàn thành chỉ vì đã có Form hoặc bảng SQL.

| Test case đề xuất | Nội dung |
|---|---|
| TC01 | Lập quyết định tuyển dụng và lưu thông tin thử việc |
| TC02 | Lập hợp đồng dài hạn, kiểm tra quy trình bảo hiểm |
| TC03 | Ghi nhận phụ trội, kiểm tra hệ số 1,5 |
| TC04 | Tính lương với phụ cấp, chấm công và khấu trừ |
| TC05 | Lập quyết định khen thưởng và phiếu chi |
| TC06 | Lập quyết định kỷ luật và phiếu thu phạt |

**Trạng thái:** Đây là các trường hợp kiểm thử dự kiến, chưa phải kết quả PASS/FAIL trên máy thực tế. Khi kiểm thử, cần bổ sung dữ liệu đầu vào, kết quả mong đợi, kết quả thực tế và ảnh minh chứng.

## 8. Thành phần nộp bài đề xuất

- Báo cáo Word: mô tả bài toán; 6 yêu cầu UML; thiết kế SQL Server; giao diện WinForms; kiểm thử.
- File mô hình UML hoặc ảnh sơ đồ: Class, Use Case tổng quát, Use Case chi tiết, Activity, Sequence.
- Project C# WinForms (nếu được yêu cầu).
- Script SQL tạo bảng và dữ liệu mẫu (nếu được yêu cầu).
- `README.md` hướng dẫn cấu hình và chạy chương trình.

---

**Ghi chú:** README này mô tả đề bài và phương án triển khai đã trao đổi. Trước khi nộp, hãy cập nhật lại danh sách tệp, chức năng và kết quả kiểm thử theo đúng những gì thực sự có trong project.
