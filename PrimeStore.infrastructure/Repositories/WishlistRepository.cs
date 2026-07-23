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
    public class WishlistRepository : GenericRepositoryAsync<Wishlist>,IWishlistRepository
    {
        #region Feilds
        private DbSet<Wishlist> _Wishlist;
        #endregion

        #region Constructor
        public WishlistRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _Wishlist = dbContext.Set<Wishlist>();
        }

        #endregion
    }
}
