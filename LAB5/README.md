# Quản lý công ty du lịch

Ứng dụng quản lý công ty du lịch Văn Hóa Việt, xây dựng theo bài tập **Bài 6 – Quản lý công ty du lịch**. Chương trình hỗ trợ quản lý dữ liệu tour, đăng ký khách, phân công hướng dẫn viên, lưu khảo sát và xem thống kê.

README mô tả chức năng của phiên bản mã nguồn hiện tại, cách cài đặt và quy trình kiểm thử.

## 1. Công nghệ sử dụng

| Thành phần | Công nghệ |
| --- | --- |
| Ngôn ngữ | C# |
| Giao diện | Windows Forms |
| Nền tảng | .NET Framework 4.7.2 |
| Cơ sở dữ liệu | SQL Server |
| Truy cập dữ liệu | ADO.NET, `System.Data.SqlClient` |
| Cấu hình kết nối | `App.config` |
| Solution | `QuanLyCongTyDuLich.sln` |

Dự án sử dụng các thư viện hệ thống được khai báo trong file `QuanLyCongTyDuLich.csproj`.

## 2. Chức năng hiện có

| Màn hình | Chức năng |
| --- | --- |
| `FrmMain` | Điều hướng 8 chức năng, lọc theo nhóm, tìm kiếm chức năng và kiểm tra kết nối SQL Server. |
| `FrmDanhMuc` | Xem, thêm, xóa phương tiện, điểm bán vé, hướng dẫn viên và điểm tham quan. |
| `FrmTour` | Xem, thêm, sửa, xóa thông tin cơ bản của tour; cập nhật số ngày, số đêm, đơn giá và trạng thái mở bán. |
| `FrmChuyenLe` | Xem, thêm, xóa chuyến dành cho khách lẻ; nhập ngày đi, ngày về, điểm đón và trạng thái. |
| `FrmDangKyLe` | Đăng ký khách lẻ theo chuyến, chọn điểm bán vé, tính thành tiền và ghi nhận thanh toán khi đăng ký. |
| `FrmDangKyDoan` | Đăng ký đoàn khách, nhập lịch đi, điểm đón, số người, tiền cọc, tổng tiền dự kiến và lựa chọn mua bảo hiểm. |
| `FrmPhanCongHDV` | Phân công hướng dẫn viên cho chuyến hoặc đoàn; nhập thù lao và kiểm tra trùng lịch. |
| `FrmKetThucKhaoSat` | Lưu điểm đánh giá và góp ý của khách. Chức năng hiện tại của màn hình này là ghi khảo sát. |
| `FrmLuongThongKe` | Xem tổng lương hướng dẫn viên, tổng tiền đăng ký và danh sách tour. |

**Cách đọc báo cáo hiện tại:** lương bằng lương cơ bản cộng thù lao của tất cả phân công, chưa lọc theo tháng. Mục doanh thu cộng thành tiền khách lẻ và tổng tiền dự kiến của đoàn còn hiệu lực, chưa phản ánh riêng số tiền thực thu.

## 3. Cấu trúc mã nguồn

| Đường dẫn | Vai trò |
| --- | --- |
| `Program.cs` | Điểm khởi chạy ứng dụng, mở `FrmMain`. |
| `Forms/` | Các cửa sổ giao diện và xử lý sự kiện. |
| `Forms/FrmMain.cs` | Menu chính, nhóm chức năng, tìm kiếm và kiểm tra kết nối. |
| `Forms/UiStyle.cs` | Màu sắc, kiểu nút, bảng dữ liệu và định dạng chung cho các form. |
| `Services/` | Các lớp xử lý nghiệp vụ và truy vấn cho tour, danh mục, đăng ký, phân công và thống kê. |
| `Data/Db.cs` | Đọc chuỗi kết nối; thực hiện truy vấn, cập nhật và tạo tham số SQL. |
| `Database/QuanLyCongTyDuLich.sql` | Tạo cơ sở dữ liệu, bảng, ràng buộc và dữ liệu mẫu. |
| `App.config` | Khai báo .NET Framework và chuỗi kết nối SQL Server. |
| `HUONG_DAN_GIAO_DIEN.md` | Hướng dẫn giao diện đã điều chỉnh. |
| `QuanLyCongTyDuLich.sln` | File mở solution trong Visual Studio. |
| `QuanLyCongTyDuLich.csproj` | Cấu hình biên dịch và danh sách file của dự án. |

Một số form hiện vẫn truy vấn trực tiếp qua `Db`; việc đưa toàn bộ xử lý dữ liệu vào lớp service là phần có thể tiếp tục cải thiện.

## 4. Yêu cầu môi trường

- Windows để chạy ứng dụng Windows Forms.
- Visual Studio có workload **.NET desktop development**.
- .NET Framework **4.7.2 Developer Pack/Targeting Pack** để biên dịch dự án.
- SQL Server đang hoạt động, cùng tài khoản có quyền tạo cơ sở dữ liệu khi khởi tạo.
- SQL Server Management Studio (SSMS) hoặc công cụ thực thi script SQL.

Cấu hình mặc định dùng instance `.\SQLEXPRESS` và xác thực Windows. Nếu máy sử dụng instance khác, cần sửa chuỗi kết nối theo hướng dẫn bên dưới.

## 5. Cài đặt và chạy chương trình

### Bước 1: Chuẩn bị mã nguồn

Giải nén dự án và mở thư mục chứa `QuanLyCongTyDuLich.sln`. Đặt README này trong cùng thư mục, thay file README cũ nếu có.

### Bước 2: Khởi tạo cơ sở dữ liệu

1. Mở SSMS và kết nối đến SQL Server trên máy.
2. Mở file `Database/QuanLyCongTyDuLich.sql`.
3. Chạy toàn bộ script.
4. Kiểm tra cơ sở dữ liệu `QuanLyCongTyDuLich` đã xuất hiện.

Có thể kiểm tra bảng tour bằng truy vấn:

```sql
USE QuanLyCongTyDuLich;
SELECT TOP (10) * FROM dbo.Tour;
```

> **Chú ý:** script có lệnh `DROP TABLE IF EXISTS`. Chạy lại sẽ xóa dữ liệu trong các bảng mà script tạo lại. Chỉ thực hiện trên cơ sở dữ liệu thử nghiệm hoặc sau khi đã sao lưu dữ liệu cần giữ. Việc thay đổi giao diện không yêu cầu chạy lại script.

### Bước 3: Cấu hình kết nối

Mở `App.config` và kiểm tra nội dung:

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <startup>
    <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.7.2" />
  </startup>
  <connectionStrings>
    <add name="QuanLyCongTyDuLichDB"
         connectionString="Server=.\SQLEXPRESS;Database=QuanLyCongTyDuLich;Trusted_Connection=True;TrustServerCertificate=True;"
         providerName="System.Data.SqlClient" />
  </connectionStrings>
</configuration>
```

- Thay giá trị `Server` bằng đúng tên máy chủ/instance dùng khi kết nối trong SSMS.
- Giữ tên cấu hình `QuanLyCongTyDuLichDB` vì `Data/Db.cs` đọc theo tên này.
- `Trusted_Connection=True` sử dụng tài khoản Windows hiện tại.
- Sau khi sửa cấu hình, build lại ứng dụng để cập nhật file cấu hình trong thư mục chạy.

### Bước 4: Mở và chạy bằng Visual Studio

1. Mở `QuanLyCongTyDuLich.sln`.
2. Chọn `QuanLyCongTyDuLich` làm **Startup Project** nếu cần.
3. Chọn **Build → Rebuild Solution**.
4. Nhấn **F5** để chạy.
5. Tại màn hình chính, chọn **Kiểm tra kết nối** rồi mở các chức năng.

Nếu dùng dòng lệnh, mở **Developer Command Prompt** tại thư mục solution:

```bat
msbuild QuanLyCongTyDuLich.sln /p:Configuration=Debug
.\bin\Debug\QuanLyCongTyDuLich.exe
```

Đây là dự án .NET Framework theo cấu trúc csproj cũ; hướng dẫn biên dịch ở trên sử dụng MSBuild.

## 6. Sử dụng giao diện chính

Giao diện dùng tông xanh, kem và vàng; `UiStyle` áp dụng định dạng chung cho các màn hình. Menu được chia thành các nhóm:

| Nhóm | Chức năng |
| --- | --- |
| Tổng quan | Toàn bộ 8 chức năng. |
| Thiết lập | Quản lý danh mục; quản lý tour. |
| Đăng ký | Đăng ký khách lẻ; đăng ký đoàn khách. |
| Điều hành | Chuyến khách lẻ; phân công hướng dẫn viên; khảo sát. |
| Báo cáo | Lương và thống kê. |

Ô tìm kiếm hỗ trợ từ khóa tiếng Việt có dấu hoặc không dấu. Kết quả được lọc trong nhóm đang chọn.

| Phím tắt tại màn hình chính | Tác dụng |
| --- | --- |
| `Ctrl + F` | Đưa con trỏ vào ô tìm kiếm. |
| `Esc` khi ô tìm kiếm đang được chọn | Xóa từ khóa. |
| `Ctrl + 1` | Quản lý danh mục. |
| `Ctrl + 2` | Quản lý tour. |
| `Ctrl + 3` | Chuyến khách lẻ. |
| `Ctrl + 4` | Đăng ký khách lẻ. |
| `Ctrl + 5` | Đăng ký đoàn khách. |
| `Ctrl + 6` | Phân công hướng dẫn viên. |
| `Ctrl + 7` | Khảo sát. |
| `Ctrl + 8` | Lương và thống kê. |

Trình tự sử dụng để kiểm tra luồng chính: kiểm tra kết nối → chuẩn bị danh mục → tạo tour → tạo chuyến khách lẻ hoặc đăng ký đoàn → phân công hướng dẫn viên → ghi khảo sát → xem thống kê. Khi kiểm tra khảo sát theo yêu cầu đề bài, dùng chuyến/đoàn đã kết thúc.

## 7. Quy tắc dữ liệu đang áp dụng

- Tour có số ngày lớn hơn 0; số đêm từ 0 đến nhỏ hơn số ngày; đơn giá không âm.
- Khách lẻ đăng ký từ **1 đến 11 người**, thanh toán khi đăng ký; thành tiền bằng số người nhân đơn giá tour.
- Đoàn khách có **trên 12 người**; tiền cọc lớn hơn 0 và tổng tiền dự kiến không nhỏ hơn tiền cọc.
- Trường hợp **đúng 12 người** hiện bị từ chối ở cả hai loại đăng ký. Đây là quy ước hiện tại vì đề bài chưa xác định loại đăng ký cho trường hợp này.
- Ngày kết thúc/ngày về không được trước ngày đi.
- Một chuyến khách lẻ chỉ có một hướng dẫn viên; một đoàn có thể được phân công nhiều hướng dẫn viên.
- Hướng dẫn viên không được có hai phân công giao nhau về thời gian. Hai khoảng có chung ngày đầu/cuối cũng được xem là trùng lịch.
- Điểm khảo sát từ **1 đến 5**; mỗi đăng ký chỉ có một phiếu khảo sát.

Các quy tắc trên được thực hiện qua mã xử lý và ràng buộc SQL. Chúng cần được kiểm tra bằng cả dữ liệu hợp lệ, dữ liệu sai và dữ liệu tại giá trị biên.

## 8. Hướng dẫn kiểm thử

Bộ kiểm thử đi kèm được cung cấp thành file riêng **`TestCase_QuanLyCongTyDuLich.xlsx`**, gồm **70 test case**:

| Sheet | Nội dung |
| --- | --- |
| `TestCase` | Điều kiện trước, dữ liệu, bước thực hiện, kết quả mong đợi, kết quả thực tế, trạng thái và minh chứng. |
| `HuongDan` | Hướng dẫn thực hiện và các quy ước dùng trong kiểm thử. |

Các ca kiểm thử bao phủ giao diện chính, danh mục, tour, chuyến khách lẻ, đăng ký lẻ, đăng ký đoàn, phân công hướng dẫn viên, khảo sát và thống kê. Bộ ca có kiểm tra hợp lệ, không hợp lệ, giá trị biên và đối chiếu yêu cầu đề bài.

### Cách thực hiện

1. Chuẩn bị cơ sở dữ liệu thử nghiệm và xác nhận kết nối thành công.
2. Đọc sheet `HuongDan`, sau đó chọn ca cần chạy trong sheet `TestCase`.
3. Đảm bảo đúng điều kiện trước và nhập đúng dữ liệu của ca.
4. Thực hiện các bước, ghi kết quả quan sát vào cột **Kết quả thực tế**.
5. Chọn trạng thái theo bảng bên dưới.
6. Ghi ảnh chụp, nội dung lỗi hoặc mã lỗi vào cột **Minh chứng / mã lỗi**.

| Trạng thái | Khi sử dụng |
| --- | --- |
| Chưa chạy | Chưa thực hiện ca kiểm thử. |
| Pass | Đã chạy và kết quả thực tế đúng với kết quả mong đợi. |
| Fail | Đã chạy nhưng kết quả khác với kết quả mong đợi. |
| Blocked | Không thể thực hiện do điều kiện trước hoặc phụ thuộc chưa sẵn sàng. |

**Trạng thái ban đầu:** 70 ca đều là **Chưa chạy**, phần kết quả thực tế để trống. Bộ test case là kế hoạch kiểm thử, chưa phải báo cáo kết quả chạy ứng dụng. Những ca đối chiếu yêu cầu có thể phát hiện chức năng chưa hoàn thiện.

File Excel được cung cấp riêng với mã nguồn; có thể đặt cạnh README khi đóng gói bài nộp. Phiên bản này chưa được xác nhận bằng việc chạy ứng dụng trên Windows với SQL Server, nên cần thực hiện kiểm thử trước khi nộp.

## 9. Xử lý lỗi thường gặp

| Hiện tượng | Cách kiểm tra |
| --- | --- |
| Không kết nối được SQL Server | Kiểm tra dịch vụ SQL Server, tên instance trong `Server`, quyền của tài khoản Windows và thử kết nối cùng instance bằng SSMS. |
| Không tìm thấy cơ sở dữ liệu hoặc bảng | Kiểm tra script đã chạy thành công và `Database=QuanLyCongTyDuLich` trỏ đúng cơ sở dữ liệu. |
| `NullReferenceException` khi đọc `Db.ConnectionString` | Kiểm tra mục `connectionStrings` có khóa `QuanLyCongTyDuLichDB`. Build lại và kiểm tra file `QuanLyCongTyDuLich.exe.config` nằm cạnh file exe. |
| Báo thiếu reference assemblies của .NET Framework 4.7.2 | Cài Developer Pack/Targeting Pack phù hợp rồi build lại. |
| Lỗi trùng mã khi thêm dữ liệu | Dùng mã mới hoặc kiểm tra bản ghi đã tồn tại. |
| Không xóa được bản ghi đang được tham chiếu | Kiểm tra dữ liệu liên quan; giữ ràng buộc khóa ngoại để bảo vệ dữ liệu. |
| Bảng dữ liệu có nhiều cột | Sử dụng thanh cuộn ngang để xem các cột còn lại. |

## 10. Các phần cần hoàn thiện theo yêu cầu bài toán

| Nội dung | Công việc còn lại |
| --- | --- |
| Hành trình và khách sạn | Bổ sung giao diện quản lý điểm dừng, phương tiện, điểm tham quan theo tour và thông tin khách sạn. |
| Thông tin đoàn và bảo hiểm | Nhập đầy đủ thông tin cơ quan/người đại diện; quản lý danh sách thành viên phục vụ mua bảo hiểm. |
| Hủy đăng ký và thanh toán đoàn | Bổ sung thao tác hủy, xử lý tiền cọc và thanh toán phần còn lại sau tour. |
| Tự tính ngày và tổng tiền | Đối chiếu ngày kết thúc với số ngày của tour; tự tính tổng tiền dự kiến thay vì nhập tay. |
| Đăng ký đoàn | Dùng transaction cho thao tác tạo đoàn và đăng ký để tránh lưu dữ liệu dở dang khi có lỗi. |
| Khảo sát | Kiểm tra tour đã kết thúc trước khi cho ghi khảo sát. |
| Lương và doanh thu | Tính lương theo tháng kết thúc tour; bổ sung bộ lọc thời gian và tách số tiền thực thu khỏi tổng tiền dự kiến. |
| Tổ chức mã và kiểm tra đầu vào | Đưa truy vấn còn nằm trong form sang service; cải thiện thông báo lỗi và kiểm tra dữ liệu trước khi lưu. |

Danh sách này giúp đối chiếu phạm vi mã nguồn hiện tại với đề bài và ghi nhận kết quả kiểm thử trung thực.

