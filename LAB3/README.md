# LAB 3 – HỆ THỐNG QUẢN LÝ KHÁCH SẠN

> Bài thực hành **Phân tích và thiết kế hướng đối tượng** – Trường Đại học Công nghệ TP.HCM (HUTECH).

## 1. Giới thiệu

**Quản lý khách sạn** là bài thực hành phân tích, thiết kế và xây dựng ứng dụng hỗ trợ nghiệp vụ của khách sạn: quản lý phòng và tiện nghi, đặt/nhận phòng, ghi nhận dịch vụ, trả phòng, lập hóa đơn, thanh toán và thống kê.

Dự án trong repository được đặt tại thư mục `QuanLyKhachSan`. Tài liệu báo cáo LAB 3 được lưu dưới định dạng Word trong cùng repository.

## 2. Mục tiêu

- Phân tích yêu cầu và quy tắc nghiệp vụ của hệ thống khách sạn.
- Thiết kế các mô hình UML: Use Case, Class, State, Sequence và Activity Diagram.
- Thiết kế cơ sở dữ liệu SQL Server với khóa chính, khóa ngoại và các ràng buộc dữ liệu.
- Xây dựng ứng dụng quản lý bằng C# Windows Forms theo kiến trúc phân lớp.
- Xây dựng và kiểm thử các kịch bản nghiệp vụ trọng tâm.

## 3. Chức năng theo yêu cầu bài LAB

| Nhóm chức năng | Nội dung |
|---|---|
| Quản lý danh mục | Khu vực, nhân viên, loại tiện nghi, dịch vụ, quy định đền bù. |
| Phòng và tiện nghi | Quản lý phòng, sức chứa, đơn giá; theo dõi lắp đặt/luân chuyển thiết bị. |
| Đặt và nhận phòng | Lưu thông tin khách; kiểm tra sức chứa, lịch đặt chồng lấn; ghi nhận người lưu trú. |
| Sử dụng dịch vụ | Ghi phiếu dịch vụ; cộng dồn cùng dịch vụ trong cùng ngày. |
| Trả phòng | Kiểm tra tiện nghi, ghi nhận đền bù nếu phát sinh, hoàn tất trả phòng. |
| Hóa đơn và thanh toán | Tổng hợp tiền phòng và dịch vụ; hỗ trợ tiền mặt, chuyển khoản, thẻ, ví điện tử. |
| Thống kê | Tra cứu và tổng hợp dữ liệu theo khoảng thời gian. |

**Lưu ý:** Bảng trên mô tả *phạm vi yêu cầu của LAB 3*, không phải xác nhận mọi chức năng đã được lập trình đầy đủ trong repository.

## 4. Công nghệ đề xuất trong tài liệu LAB

| Thành phần | Công nghệ |
|---|---|
| Ngôn ngữ | C# |
| Giao diện | Windows Forms (WinForms) |
| IDE | Visual Studio 2022 |
| Framework | .NET Framework 4.7.2 |
| Cơ sở dữ liệu | Microsoft SQL Server (LocalDB hoặc Express) |
| Kết nối dữ liệu | ADO.NET (`System.Data.SqlClient`) |
| Mô hình | UI / Forms → Services → Data → SQL Server |

## 5. Cấu trúc dự án

Cấu trúc ở mức repository theo ảnh cung cấp:

```text
LAB3/
├── QuanLyKhachSan/                   # Thư mục source code
├── Phạm Bùi Tiến Hoàng_1250080061_Lab3.docx  # Báo cáo Word
└── README.md                         # Hướng dẫn dự án
```

Tài liệu hướng dẫn LAB đề xuất tổ chức code theo các thành phần:

```text
QuanLyKhachSan/
├── Data/            # Kết nối và truy vấn CSDL (ví dụ: Db.cs)
├── Services/        # Xử lý nghiệp vụ
├── Forms/           # Các màn hình WinForms
├── Database/        # Script SQL nếu được đưa vào repository
└── App.config       # Cấu hình kết nối nếu dự án dùng file này
```

Các tên thư mục/file trong khối thứ hai là **cấu trúc tham khảo từ tài liệu LAB**; cần đối chiếu với source code thực tế.

## 6. Cài đặt và chạy dự án

### Yêu cầu

- Máy tính Windows.
- Visual Studio 2022 với workload **.NET desktop development**.
- **.NET Framework 4.7.2 Developer Pack/Targeting Pack**.
- SQL Server LocalDB/Express và SQL Server Management Studio (SSMS) nếu cần quản lý CSDL.

### Các bước thực hiện

1. Tải hoặc clone repository về máy.
2. Mở thư mục `QuanLyKhachSan` và mở file solution `*.sln` (nếu có) bằng Visual Studio 2022.
3. Tạo CSDL tên **`QuanLyKhachSan`** trên SQL Server.
4. Nếu repository có file `Database/QuanLyKhachSan.sql`, mở file đó trong SSMS và chạy để tạo bảng, ràng buộc và dữ liệu mẫu. **Kiểm tra script trước khi chạy** vì một số script khởi tạo có lệnh xóa bảng cũ.
5. Kiểm tra chuỗi kết nối SQL Server trong `App.config` hoặc lớp kết nối tương ứng. Ví dụ khi dùng LocalDB:

   ```xml
   <connectionStrings>
     <add name="QuanLyKhachSanDB"
          connectionString="Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=QuanLyKhachSan;Integrated Security=True"
          providerName="System.Data.SqlClient" />
   </connectionStrings>
   ```

6. Chọn **Build → Build Solution** để biên dịch.
7. Nhấn **F5** để chạy ứng dụng.

> Tên SQL Server instance có thể khác trên từng máy. Hãy thay `Data Source` bằng tên instance đang sử dụng. Vị trí cấu hình kết nối và tên solution phải kiểm tra theo project thực tế.

## 7. Các bảng dữ liệu chính theo tài liệu thiết kế

- **Danh mục và phòng:** `NhanVien`, `KhuVuc`, `Phong`, `LoaiTienNghi`, `TienNghi`, `PhieuLapDat`.
- **Đặt phòng:** `KhachHang`, `PhieuDatPhong`, `ChiTietDatPhong`, `NguoiLuuTru`.
- **Dịch vụ:** `DichVu`, `PhieuSuDungDV`, `ChiTietPhieuSuDungDV`.
- **Đền bù:** `QuyDinhDenBu`, `PhieuDenBu`, `ChiTietPhieuDenBu`.
- **Thanh toán:** `HoaDon`, `ThanhToan`.

Tổng cộng **18 bảng** trong thiết kế tham khảo của bài LAB. Cơ sở dữ liệu thực tế có thể được điều chỉnh theo phần triển khai.

## 8. Các trường hợp kiểm thử cần chú ý

- Không cho đặt phòng vượt quá sức chứa.
- Không cho đặt trùng phòng trong các khoảng thời gian giao nhau.
- Một thiết bị không được lắp tại hai phòng trong cùng một ngày.
- Cùng dịch vụ phát sinh nhiều lần trong một ngày phải được cộng dồn đúng.
- Hóa đơn phải tính đúng tiền phòng và dịch vụ.
- Ghi nhận thanh toán bằng các phương thức được hỗ trợ, kể cả nhiều giao dịch cho một hóa đơn.

## 9. Tài liệu liên quan

- Báo cáo LAB 3: `Phạm Bùi Tiến Hoàng_1250080061_Lab3.docx`.
- Đề bài / tài liệu tham khảo: **Bài 3 – Hệ thống quản lý khách sạn**.

---

**Ghi chú:** README này được soạn từ ảnh cấu trúc repository và tài liệu hướng dẫn LAB 3. Chưa có đủ nội dung source code thực tế để xác minh mức độ hoàn thiện của từng Form và chức năng.
