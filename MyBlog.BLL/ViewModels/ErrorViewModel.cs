namespace MyBlog.BLL.ViewModels
{
    /// <summary>
    /// ������ ������ ������
    /// </summary>
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}