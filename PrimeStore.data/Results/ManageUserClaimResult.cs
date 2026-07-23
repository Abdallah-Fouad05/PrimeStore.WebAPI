namespace PrimeStore.data.Results
{
    public class UserClaim
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public bool Value { get; set; }
    }
    public class ManageUserClaimResult
    {
        public int UserId { get; set; }
        public List<UserClaim> Claims { get; set; }
    }
}

