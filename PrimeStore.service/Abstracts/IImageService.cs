using Microsoft.AspNetCore.Http;
using PrimeStore.data.Entities;

namespace PrimeStore.service.Abstracts
{
    public interface IImageService
    {
        Task<string> AddImageAsync(IFormFile Image);
        Task<string> DeleteImageAsync(int ImageId);
        IQueryable<Image> GetImagesQueryable();
    }
}
