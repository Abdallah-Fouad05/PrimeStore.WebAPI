using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using PrimeStore.data.Entities;
using PrimeStore.data.Entities.Identity;
using PrimeStore.data.Helper;
using PrimeStore.data.Helper.Status;
using PrimeStore.infrastructure.Context;
using PrimeStore.service.Abstracts;

namespace SchoolProject.Service.Implementations
{
    public class UserService : IUserService
    {
        #region Fields
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailService _emailsService;
        private readonly ICartService _CartService;
        private readonly ApplicationDbContext _applicationDBContext;
        #endregion
        #region Constructors
        public UserService(UserManager<User> userManager,
                                      IHttpContextAccessor httpContextAccessor,
                                      IEmailService emailsService,
                                      ApplicationDbContext applicationDBContext,
                                      ICartService cartService)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _emailsService = emailsService;
            _applicationDBContext = applicationDBContext;
            _CartService = cartService;
        }
        #endregion
        #region Handle Functions
        public async Task<string> AddUserAsync(User user, string password)
        {
            var trans = await _applicationDBContext.Database.BeginTransactionAsync();
            try
            {
                //if Email is Exist
                var existUser = await _userManager.FindByEmailAsync(user.Email);
                //email is Exist
                if (existUser != null) return "EmailIsExist";

                //if username is Exist
                var userByUserName = await _userManager.FindByNameAsync(user.UserName);
                //username is Exist
                if (userByUserName != null) return "UserNameIsExist";

                user.StatusId = (int)UserStatusEnum.Active;
                //Create
                var createResult = await _userManager.CreateAsync(user, password);
                //Failed
                if (!createResult.Succeeded)
                    return string.Join(",", createResult.Errors.Select(x => x.Description).ToList());

                await _userManager.AddToRoleAsync(user, "User");

                //Send Confirm Email
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var resquestAccessor = _httpContextAccessor.HttpContext.Request;
                var returnUrl = resquestAccessor.Scheme + "://" + resquestAccessor.Host + "/Api/V1/Authentication/" + $"ConfirmEmail?UserId={user.Id}&Code={code}";
                var message = $"To Confirm Email Click Link: <a href='{returnUrl}'>Link Of Confirmation</a>";
                //$"/Api/V1/Authentication/ConfirmEmail?userId={user.Id}&code={code}";
                //message or body /Api/V1/Authentication
                await _emailsService.SendEmailAsync(user.Email, message, "Confirm Email");

                // Add User Cart
                var User_EX = await _userManager.FindByEmailAsync(user.Email);

                if (User_EX == null)
                {
                    return "Failed";
                }

                var UserCart = new Cart
                {
                    UserId = User_EX.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = null
                };

                await _CartService.AddCartAsync(UserCart);

                await trans.CommitAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                return "Failed";
            }

        }

        public async Task<string> UpdateUserStatus(int UserId, int StatusId)
        {
            var user = await _userManager.FindByIdAsync(UserId.ToString());

            if (user == null)
            {
                return ResultString.NotFound;
            }

            user.StatusId = StatusId;

            await _userManager.UpdateAsync(user);

            return ResultString.Success;
        }
        #endregion
    }
}
