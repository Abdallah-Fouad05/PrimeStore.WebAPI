namespace PrimeStore.data.Results
{
    public class UserRoles
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public bool HasRole { get; set; }
    }
    public class ManageUserRoleResult
    {
        public int UserId { get; set; }
        public List<UserRoles> Roles { get; set; }
    }
}

