# LAB 2 – HỆ THỐNG QUẢN LÝ THƯ VIỆN

> Bài thực hành môn **Phân tích và thiết kế hướng đối tượng (OOSD)** – Hệ thống quản lý thư viện bằng C# WinForms và SQL Server.

## 1. Giới thiệu

LAB 2 mô phỏng quy trình quản lý sách và mượn – trả sách tại thư viện. Đề tài kết hợp **phân tích nghiệp vụ, mô hình UML, thiết kế cơ sở dữ liệu và xây dựng ứng dụng Windows Forms** để hỗ trợ công việc của thủ thư và nhân viên quản lý sách.

Repository chứa thư mục dự án **`QuanLyThuVien`** và **báo cáo LAB 2**. Các chức năng bên dưới được tổng hợp theo tài liệu yêu cầu LAB; phạm vi đã lập trình hoặc kiểm thử thực tế cần đối chiếu với mã nguồn trong thư mục dự án.

## 2. Chức năng và nghiệp vụ

| Nhóm chức năng | Nội dung |
|---|---|
| Danh mục | Quản lý nhân viên, thể loại và nhà xuất bản. |
| Sách | Quản lý đầu sách, năm xuất bản, thể loại, nhà xuất bản và số lượng hiện có. |
| Độc giả – thẻ | Lưu hồ sơ độc giả; cấp mới/gia hạn thẻ; theo dõi hạn sử dụng và lệ phí năm. |
| Mượn sách | Kiểm tra điều kiện mượn; lập phiếu mượn và cập nhật số lượng sách. |
| Trả sách – phạt | Ghi nhận trả sách; xử lý các trường hợp trả trễ, mất hoặc hư hỏng; lập phiếu phạt khi cần. |
| Thống kê | Tổng hợp số lượt mượn, sách quá hạn, mất, hư hỏng và phí phạt theo thời gian. |

### Quy tắc nghiệp vụ chính

- Độc giả cần có **thẻ còn hiệu lực** và đã đóng lệ phí năm để mượn sách.
- Mỗi độc giả chỉ có **một thẻ đang hoạt động** tại một thời điểm.
- Mỗi độc giả được mượn **tối đa 3 cuốn sách khác nhau**; nếu còn sách quá hạn chưa trả thì không được mượn thêm.
- Không cho mượn sách hết số lượng; không lặp một đầu sách trong cùng phiếu mượn.
- Ngày hẹn trả không được trước ngày mượn.
- Trả trễ, làm mất hoặc làm hư hỏng sách cần ghi nhận thông tin phạt theo từng cuốn.

## 3. Công nghệ sử dụng

- **Ngôn ngữ:** C#
- **Giao diện:** Windows Forms (WinForms)
- **Nền tảng:** .NET Framework 4.7.2
- **IDE:** Visual Studio 2022
- **Cơ sở dữ liệu:** Microsoft SQL Server (LocalDB hoặc SQL Server Express)
- **Truy cập dữ liệu:** ADO.NET (`System.Data.SqlClient`)
- **Tổ chức mã nguồn theo tài liệu thiết kế:** Forms → Services → Data → SQL Server
- **Mô hình hóa:** UML (Use Case, Class, Activity, Sequence) và ERD

## 4. Cấu trúc repository

```text
LAB2/
├── QuanLyThuVien/                         # Thư mục dự án ứng dụng
├── Phạm Bùi Tiến Hoàng_1250080061_Lab2.docx  # Báo cáo LAB 2
└── README.md                              # Tài liệu này
```

Trong tài liệu hướng dẫn LAB, dự án WinForms được tổ chức theo các thành phần dự kiến:

```text
QuanLyThuVien/
├── Forms/        # Giao diện: FrmMain, FrmDanhMuc, FrmSach, ...
├── Services/     # Xử lý nghiệp vụ thư viện
├── Data/         # Truy cập SQL Server (Db.cs)
├── Database/     # SQL script, nếu được lưu trong dự án
└── App.config    # Chuỗi kết nối cơ sở dữ liệu
```

> Cấu trúc bên trong `QuanLyThuVien/` ở trên là **cấu trúc tham chiếu từ tài liệu LAB**, không phải danh sách tệp đã xác minh từ GitHub.

## 5. Cơ sở dữ liệu

Theo tài liệu thiết kế, CSDL có tên **`QuanLyThuVienDB`**, gồm các bảng chính:

| Bảng | Mục đích |
|---|---|
| `NhanVien` | Lưu thông tin nhân viên. |
| `TheLoai` | Danh mục thể loại sách. |
| `NhaXuatBan` | Thông tin nhà xuất bản. |
| `DauSach` | Danh mục đầu sách và tồn kho. |
| `DocGia` | Thông tin độc giả. |
| `TheDocGia` | Thẻ thư viện, hạn dùng và lệ phí. |
| `PhieuMuon` | Thông tin phiếu mượn. |
| `ChiTietPhieuMuon` | Những đầu sách thuộc một phiếu mượn và tình trạng trả. |
| `PhieuPhat` | Tiền phạt khi trả trễ, mất hoặc hư hỏng. |

Các ràng buộc quan trọng gồm **PK, FK, UNIQUE, CHECK** và transaction để tránh cập nhật dở dang khi mượn – trả sách.

## 6. Hướng dẫn chạy dự án

### Bước 1: Chuẩn bị môi trường

Cài **Visual Studio 2022** (workload **.NET desktop development**) và **SQL Server LocalDB/Express**. Có thể sử dụng SSMS để quản lý CSDL.

### Bước 2: Tạo hoặc khôi phục CSDL

1. Mở SQL Server Management Studio (SSMS).
2. Tìm script tạo CSDL trong dự án, ví dụ `QuanLyThuVien/Database/QuanLyThuVien.sql` **nếu repository có tệp này**.
3. Chạy script để tạo CSDL `QuanLyThuVienDB` và dữ liệu mẫu (nếu script cung cấp).
4. Xác nhận các bảng được tạo thành công.

### Bước 3: Cấu hình kết nối

Trong `App.config`, kiểm tra chuỗi kết nối và đổi `Data Source` theo tên SQL Server trên máy:

```xml
<connectionStrings>
  <add name="QuanLyThuVienDb"
       connectionString="Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=QuanLyThuVienDB;Integrated Security=True;TrustServerCertificate=True;MultipleActiveResultSets=True"
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

Nếu máy sử dụng `SQLEXPRESS`, thay `(LocalDB)\MSSQLLocalDB` bằng `.\SQLEXPRESS`. Tên chuỗi kết nối phải khớp với code hiện có.

### Bước 4: Mở và chạy

1. Mở thư mục `QuanLyThuVien` trong Visual Studio và chọn tệp **`.sln`** (hoặc mở project `.csproj` nếu không có solution).
2. Kiểm tra **Target Framework = .NET Framework 4.7.2**.
3. Nhấn **Ctrl + Shift + B** để Build.
4. Nhấn **F5** để chạy chương trình.
5. Thử mở danh mục sách, độc giả/thẻ và chức năng mượn – trả.

Nếu chương trình báo lỗi kết nối, hãy kiểm tra tên instance SQL Server, việc tồn tại của database và quyền kết nối.

## 7. Các Form theo thiết kế LAB

- `FrmMain`: Trang chủ, điều hướng các chức năng.
- `FrmDanhMuc`: Nhân viên, thể loại, nhà xuất bản.
- `FrmSach`: Danh mục và tìm kiếm sách.
- `FrmDocGia`: Quản lý độc giả, cấp và gia hạn thẻ.
- `FrmMuonTra`: Mượn sách, trả sách, ghi nhận phạt.
- `FrmThongKe`: Thống kê theo khoảng thời gian.

*Tên Form là tên được nêu trong tài liệu thiết kế LAB; không mặc định tất cả đã được hiện thực trong mã nguồn hiện tại.*

## 8. Kiểm thử đề xuất

| Trường hợp | Kết quả mong đợi |
|---|---|
| Thẻ hết hạn hoặc chưa đóng lệ phí | Từ chối mượn. |
| Đang mượn 3 cuốn, muốn mượn thêm | Từ chối mượn. |
| Còn sách quá hạn chưa trả | Từ chối mượn thêm. |
| Đầu sách hết số lượng | Không lập phiếu; không trừ tồn kho. |
| Mượn hợp lệ | Lưu phiếu, chi tiết và giảm tồn kho. |
| Trả đúng hạn, sách bình thường | Ghi nhận trả, tăng tồn kho, không phát sinh phạt. |
| Trả trễ, mất hoặc hư hỏng | Ghi nhận tình trạng và phiếu phạt theo quy định. |

> Đây là **kịch bản kiểm thử**; README không khẳng định tất cả đã chạy đạt cho đến khi được kiểm chứng trên project.

## 9. Tài liệu báo cáo

- [Báo cáo LAB 2](<Phạm Bùi Tiến Hoàng_1250080061_Lab2.docx>)
- [Thư mục mã nguồn QuanLyThuVien](./QuanLyThuVien/)

---

**Đề tài:** LAB 2 – Quản lý thư viện  
**MSSV:** `1250080061`  
**Mục đích:** Bài thực hành phân tích, thiết kế hướng đối tượng và phát triển ứng dụng C# WinForms.
