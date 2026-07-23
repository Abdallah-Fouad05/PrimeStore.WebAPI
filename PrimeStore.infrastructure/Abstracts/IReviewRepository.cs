using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrimeStore.data.Entities;
using PrimeStore.infrastructure.InfrastructureBases;

namespace PrimeStore.infrastructure.Abstracts
{
    public interface IReviewRepository : IGenericRepositoryAsync<Review>
    {

    }
}
