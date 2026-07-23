using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PrimeStore.data.Entities;
using PrimeStore.infrastructure.Abstracts;
using PrimeStore.infrastructure.Context;
using PrimeStore.infrastructure.InfrastructureBases;

namespace PrimeStore.infrastructure.Repositories
{
    public class ReviewRepository : GenericRepositoryAsync<Review>, IReviewRepository
    {
        #region Fields
        private DbSet<Review> _Reviews;
        #endregion

        #region Constructors
        public ReviewRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _Reviews = dbContext.Set<Review>();
        }
        #endregion
    }
}
