namespace Clinic.ViewModels
{
    public class ApiResponse<T>: IApiResponse where T: class
    {
        public bool Succeed { get; set; }
        public string Error { get; set; }
        public T Data { get; set; }
    }

    public class ApiListResponse<T> : IApiResponse where T : class
    {
        public bool Succeed { get; set; }
        public string Error { get; set; }
        public T Data { get; set; }
    }

    public interface IApiResponse
    {
        public bool Succeed { get; set; }
        public string Error { get; set; }
    }
}
