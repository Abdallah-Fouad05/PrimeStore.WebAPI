namespace PrimeStore.core.Features.Users.Queries.Results
{
    public class UserStatusResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class GetUserPaginatedListResponse
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string? ImageUrl { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public UserStatusResponse? Status { get; set; }

    }
}
