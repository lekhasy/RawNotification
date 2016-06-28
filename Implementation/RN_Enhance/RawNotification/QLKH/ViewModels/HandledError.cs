using System;

namespace QLKH.ViewModels
{
    public class HandledError
    {

        public string Description { get; }

        public string Title { get; }

        public Exception ErrorException { get; }

        public HandledError(Exception Ex, string Errortitle, string ErrorDescription)
        {
            ErrorException = Ex;
            Title = Errortitle;
            Description = ErrorDescription;
        }
    }

    public static class ErrorTemplates
    {
        private static string Title_DBInteractionError = "Lỗi tương tác với cơ sở dữ liệu";

        private static string Des_DBInteractionError = @"Nguyên nhân: - Kết nối tới server gặp vấn đề
Giải pháp: - Kiểm tra lại kết nối tới server
Nguyên nhân: - Nếu đang muốn thêm hoặc xóa dữ liệu, có thể các ràng buộc dữ liệu của Database không cho phép làm điều đó
Giải pháp: - Làm mới lại dữ liệu xem các đối tượng liên quan tới việc chỉnh sửa này còn tồn tại hay không, liên hệ admin để xem lại quyền truy cập DB
xem file log để biết thêm chi tiết";

        private static string Title_FileSecurityError = "Lỗi bảo mật file hệ thống";
        private static string Des_FileSecurityError = @"Không thể khởi tạo file do ứng dụng không có quyền với thư mục ghi file log.
                    ứng dụng sẽ hoạt động mà không ghi file";

        private static string Title_PathTooLongException = "Lỗi đường dẫn quá dài";
        private static string Des_PathTooLongException = @"Không thể khởi tạo file do đường dẫn quá dài.
                    Độ dài tối đa là 260 ký tự";



        public static HandledError GetDBInteractionError(Exception ex)
        {
            return new HandledError(ex, Title_DBInteractionError, Des_DBInteractionError);
        }

        public static HandledError GetFileSecurityError(Exception ex)
        {
            return new HandledError(ex, Title_FileSecurityError, Des_FileSecurityError);
        }

        public static HandledError GetPathTooLongException(Exception ex)
        {
            return new HandledError(ex, Title_PathTooLongException, Des_PathTooLongException);
        }


    }
}