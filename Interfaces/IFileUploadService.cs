namespace dashboard.Interfaces
{
    public interface IFileUploadService
    {
        Task UploadFile(IFormFile file);
    }
}