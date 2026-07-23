using AutoMapper;
using PrimeStore.core.Features.Reviews.Commands.Models;
using PrimeStore.core.Features.Reviews.Queries.Results;
using PrimeStore.data.Entities;
using PrimeStore.data.Entities.Identity;

namespace PrimeStore.core.Mapping.ReviewMapping
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<AddReviewCommand, Review>();
            CreateMap<EditReviewCommand, Review>();
            CreateMap<User, UserReviewResponse>();
            CreateMap<Review, GetProductReviewsResponse>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));
        }
    }
}
