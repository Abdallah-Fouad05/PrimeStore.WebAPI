using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PrimeStore.data.Entities.Identity;

namespace PrimeStore.data.Entities.Status
{
    public class UserStatus
    {
        [Key]
        public int StatusId { get; set; }
        public string Name { get; set; }

        [InverseProperty(nameof(User.UserStatus))]
        public List<User> users { get; set; }
    }

}
