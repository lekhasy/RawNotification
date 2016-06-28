Trong namespace QLKH.Models này có hai class là AppSetting và BackupSchedule
BackupSchedule biểu diễn một tác vụ backup. một tác vụ backup có thể có một hoặc nhiều công việc.
Một công việc được biểu diễn bằng lớp Work trong Package WorkExcuter.
AppSetting có nhiệm vụ lưu trữ các tác vụ backup.
* Lưu ý:
Việc một tác vụ backup bị hủy hay được thêm mới không phụ thuộc vào lớp Appsetting.
lớp BackupSchedule sẽ tương tác trực tiếp với các lớp trong Package WorkExcuter để đăng ký các công việc cần thực thi cho nó.
Lớp AppSetting chỉ có nhiệm vụ lưu trữ các BackupSchedule để show lên cho người dùng xem.

Tuy nhiên, đó lại là điểm yếu của cách tổ chưc này.
lớp Appsetting sẽ lưu một file xuống ổ đĩa, và Package WorkExcuter cũng lưu một file xuống.
hai file đó ít nhiều vẫn có sự liên quan đến nhau.
Sự không nhất quán trong dữ liệu sẽ xảy ra nếu có sự cố đột ngột khiến chương trình bị tắt khi một file được lưu xuống mà file kia chưa được lưu.
vì vậy, khi khởi động ứng dụng, ta cần gọi hàm CheckAndFixError để kiểm tra và sửa lỗi.