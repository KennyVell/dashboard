using dashboard.Interfaces;

namespace dashboard.Services
{
    public class FileUploadService : IFileUploadService
    {
        private readonly IDataProcessingService _dataProcessingService;

        public FileUploadService(IDataProcessingService dataProcessingService)
        {
            _dataProcessingService = dataProcessingService;
        }

        public void UploadFile(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                _dataProcessingService.ProcessFile(filePath);
            }
        }
    }
}
