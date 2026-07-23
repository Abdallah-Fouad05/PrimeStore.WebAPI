using PrimeStore.data.Entities;
using PrimeStore.infrastructure.Abstracts;
using PrimeStore.infrastructure.Context;
using PrimeStore.infrastructure.InfrastructureBases;

namespace PrimeStore.infrastructure.Repositories
{
    public class ImageRepository : GenericRepositoryAsync<Image>, IImageRepository
    {
        public ImageRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
