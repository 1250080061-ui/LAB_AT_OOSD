# LAB 4 – HỆ THỐNG e-SHOPPING

## 1. Giới thiệu

Đây là bài tập Lab 4 môn Phân tích và Thiết kế Hệ thống Hướng đối tượng. Chương trình mô phỏng hệ thống **e-SHOPPING**, cho phép khách hàng xem sản phẩm, quản lý giỏ hàng, đăng ký, đăng nhập, đặt hàng và thanh toán.

Ứng dụng được xây dựng bằng **C# WinForms (.NET Framework 4.7.2)** và **SQL Server**. Giao diện có thể hoạt động ở hai chế độ:

- Chưa kết nối SQL: sử dụng dữ liệu mẫu trong bộ nhớ.
- Đã kết nối SQL: đọc và ghi dữ liệu trực tiếp trên SQL Server.

## 2. Công nghệ sử dụng

- C# WinForms
- .NET Framework 4.7.2
- SQL Server / SQL Server Management Studio 2022
- ADO.NET (`System.Data.SqlClient`)
- Visual Studio 2022
- Git và GitHub

## 3. Chức năng chính

- Xem danh sách sản phẩm theo nhóm.
- Xem thông tin chi tiết sản phẩm.
- Thêm sản phẩm vào giỏ hàng.
- Tăng, giảm số lượng hoặc xóa sản phẩm khỏi giỏ hàng.
- Đăng ký tài khoản khách hàng.
- Đăng nhập hệ thống.
- Nhập thông tin người nhận.
- Chọn khu vực và phương thức giao hàng.
- Tính phí giao hàng và tổng tiền.
- Kiểm tra thông tin thẻ thanh toán.
- Ghi nhận đơn hàng và hiển thị kết quả đặt hàng thành công.

## 4. Danh sách 7 Form

| STT | Form | Chức năng |
|---:|---|---|
| 1 | `FrmMain` | Trang chính và điều hướng chức năng |
| 2 | `FrmProducts` | Hiển thị, lọc và xem chi tiết sản phẩm |
| 3 | `FrmCart` | Quản lý giỏ hàng và số lượng sản phẩm |
| 4 | `FrmLogin` | Đăng nhập tài khoản |
| 5 | `FrmRegister` | Đăng ký tài khoản khách hàng |
| 6 | `FrmCheckout` | Nhập thông tin giao hàng và thanh toán |
| 7 | `FrmOrderSuccess` | Hiển thị kết quả đặt hàng thành công |

## 5. Kiến trúc chương trình

Chương trình được tổ chức theo các lớp:

```text
Giao diện WinForms
        ↓
Service / Xử lý nghiệp vụ
        ↓
Repository / Truy cập dữ liệu
        ↓
SQL Server hoặc dữ liệu mẫu
```

Các file chính:

```text
EShoppingLab4
├── Program.cs
├── App.config
├── Models.cs
├── Services.cs
├── Data.cs
├── FrmMain.cs
├── FrmMain.Designer.cs
├── FrmProducts.cs
├── FrmProducts.Designer.cs
├── FrmCart.cs
├── FrmCart.Designer.cs
├── FrmLogin.cs
├── FrmLogin.Designer.cs
├── FrmRegister.cs
├── FrmRegister.Designer.cs
├── FrmCheckout.cs
├── FrmCheckout.Designer.cs
├── FrmOrderSuccess.cs
└── FrmOrderSuccess.Designer.cs
```

## 6. Chạy chương trình khi chưa kết nối SQL

Mở `App.config` và đặt:

```xml
<add key="UseSql" value="false"/>
```

Chương trình sẽ sử dụng dữ liệu mẫu từ các lớp:

- `MockProductRepository`
- `MockAccountRepository`
- `MockOrderRepository`

Tài khoản đăng nhập mẫu:

```text
Tên đăng nhập: demo
Mật khẩu: 123456
```

## 7. Chạy chương trình khi đã kết nối SQL

### Bước 1: Tạo cơ sở dữ liệu

Mở SQL Server Management Studio 2022, chạy file:

```text
EShoppingLab4_SSMS22_Setup.sql
```

Script sẽ tạo cơ sở dữ liệu `EShoppingLab4`, các bảng cần thiết và dữ liệu sản phẩm mẫu.

### Bước 2: Cấu hình chuỗi kết nối

Trong `App.config`, sửa `Data Source` cho đúng tên SQL Server trên máy:

```xml
<connectionStrings>
  <add name="EShoppingDb"
       connectionString="Data Source=.\SQLEXPRESS;Initial Catalog=EShoppingLab4;Integrated Security=True;TrustServerCertificate=True"
       providerName="System.Data.SqlClient"/>
</connectionStrings>
```

### Bước 3: Bật chế độ SQL

Đổi cấu hình:

```xml
<add key="UseSql" value="true"/>
```

Sau đó chọn **Build → Rebuild Solution** và chạy chương trình.

## 8. Dữ liệu kiểm thử thanh toán

Có thể sử dụng dữ liệu thử nghiệm sau:

```text
Loại thẻ: VISA
Số thẻ: 4111111111111111
CVV: 123
Ngày hết hạn: chọn một tháng trong tương lai
```

Không sử dụng thông tin thẻ thật. Chương trình chỉ mô phỏng quá trình xác thực thanh toán và chỉ lưu số thẻ đã che, ví dụ `************1111`.

## 9. Các bảng dữ liệu chính

- `ProductGroups`: nhóm sản phẩm.
- `Products`: thông tin và tồn kho sản phẩm.
- `Customers`: thông tin khách hàng.
- `Accounts`: tài khoản đăng nhập.
- `DeliveryMethods`: phương thức giao hàng.
- `ShippingRates`: phí giao hàng.
- `Orders`: thông tin đơn hàng.
- `OrderItems`: chi tiết sản phẩm trong đơn hàng.
- `PaymentTransactions`: kết quả giao dịch thanh toán.

## 10. Lưu ý

- Không đưa các thư mục `.vs`, `bin` và `obj` lên GitHub.
- Không lưu mật khẩu SQL Server hoặc thông tin thẻ thật trong source code.
- Khi `UseSql=false`, dữ liệu chỉ được lưu tạm thời và mất khi đóng chương trình.
- Khi `UseSql=true`, tài khoản và đơn hàng được lưu trong SQL Server.

## 11. Thông tin sinh viên

- Họ và tên: *Điền họ và tên sinh viên*
- Mã số sinh viên: `1250080061`
- Bài thực hành: Lab 4 – e-SHOPPING
