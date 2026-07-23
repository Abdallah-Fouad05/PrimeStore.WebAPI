using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using PrimeStore.data.Entities;
using PrimeStore.data.Helper;
using PrimeStore.infrastructure.Abstracts;
using PrimeStore.service.Abstracts;

namespace PrimeStore.service.Implementations
{
    public class ImageService : IImageService
    {
        #region Fields
        private readonly IImageRepository _ImageRepository;
        private readonly IHttpContextAccessor _HttpContextAccessor;
        private readonly IWebHostEnvironment _WebHostEnvironment;
        #endregion

        #region constructor
        public ImageService(IImageRepository imageRepository, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment webHostEnvironment)
        {
            _ImageRepository = imageRepository;
            _HttpContextAccessor = httpContextAccessor;
            _WebHostEnvironment = webHostEnvironment;
        }
        #endregion

        #region services
        public async Task<string> AddImageAsync(IFormFile NewImage)
        {

            var context = _HttpContextAccessor.HttpContext.Request;
            var baseUrl = context.Scheme + "://" + context.Host;
            var imageUrl = await UploadImage(NewImage);

            switch (imageUrl)
            {
                case "NoImage": return "NoImage";
                case "FailedToUploadImage": return "FailedToUploadImage";
            }

            Image Image = new Image() { ImageUrl = baseUrl + imageUrl };

            try
            {
                await _ImageRepository.AddAsync(Image);
                return "Success";
            }
            catch (Exception)
            {
                return "FailedInAdd";
            }
        }
        private async Task<string> UploadImage(IFormFile image)
        {
            var path = _WebHostEnvironment.WebRootPath + "/";
            var extention = Path.GetExtension(image.FileName);
            var fileName = Guid.NewGuid().ToString().Replace("-", string.Empty) + extention;
            if (image.Length > 0)
            {
                try
                {
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (FileStream filestreem = File.Create(path + fileName))
                    {
                        await image.CopyToAsync(filestreem);
                        await filestreem.FlushAsync();
                        return $"/{fileName}";
                    }
                }
                catch (Exception)
                {
                    return "FailedToUploadImage";
                }
            }
            else
            {
                return "NoImage";
            }
        }
        public async Task<string> DeleteImageAsync(int ImageId)
        {
            var image = await _ImageRepository.GetByIdAsync(ImageId);
            if (image == null)
            {
                return ResultString.NotFound;
            }
            var trans = _ImageRepository.BeginTransaction();
            try
            {
                await _ImageRepository.DeleteAsync(image);
                trans.Commit();
                return ResultString.Success;
            }
            catch
            {
                trans.Rollback();
                return ResultString.Failure;
            }
        }

        public IQueryable<Image> GetImagesQueryable()
        {
            return _ImageRepository.GetTableNoTracking().AsQueryable();
        }
        #endregion
    }
}
