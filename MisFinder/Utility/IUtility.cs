using Microsoft.AspNetCore.Http;

namespace MisFinder.Utility
{
    public interface IUtility
    {
        string SaveImageToFolder(IFormFile file);
        void StoreInFolder(IFormFile file, string fileName);
        bool IsImageExtensionAllowed(IFormFile file);
        bool IsSizeAllowed(IFormFile file);

    }
}