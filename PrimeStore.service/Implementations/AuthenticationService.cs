using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PrimeStore.data.Entities.Identity;
using PrimeStore.data.Helper;
using PrimeStore.Data.Entities.Identity;
using PrimeStore.Data.Helpers;
using PrimeStore.infrastructure.Context;
using PrimeStore.Infrustructure.Abstracts;
using PrimeStore.service.Abstracts;
using PrimeStore.Service.Abstracts;
namespace PrimeStore.Service.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        #region Fields
        private readonly JwtSettings _JwtSettings;
        private readonly UserManager<User> _UserManager;
        private readonly ApplicationDbContext _DbContext;
        private readonly IEmailService _EmailService;
        private readonly IRefreshTokenRepository _RefreshTokenRepository;
        #endregion 

        #region Constructors
        public AuthenticationService(JwtSettings jwtSettings, IRefreshTokenRepository refreshTokenRepository,
            UserManager<User> userManager, ApplicationDbContext dbContext, IEmailService emailService)
        {
            _JwtSettings = jwtSettings;
            _RefreshTokenRepository = refreshTokenRepository;
            _UserManager = userManager;
            _DbContext = dbContext;
            _EmailService = emailService;
        }


        #endregion

        #region Handle Functions
        public async Task<JwtAuthResult> GetJWTToken(User user)
        {

            var JwtToken = await GenerateJWTToken(user);

            JwtSecurityToken jwtSecurityToken = JwtToken.Item1;

            string AccessToken = JwtToken.Item2;

            var refreshToken = GenerateRefreshToken(user.UserName);

            var UserRefreshToken = new UserRefreshToken
            {
                AddedTime = DateTime.UtcNow,
                ExpiryDate = refreshToken.ExpireAt,
                RefreshToken = refreshToken.TokenString,
                UserId = user.Id,
                IsRevoked = false,
                IsUsed = false,
                JwtId = jwtSecurityToken.Id,
                Token = AccessToken
            };

            await _RefreshTokenRepository.AddAsync(UserRefreshToken);

            return new JwtAuthResult { AccessToken = AccessToken, refreshToken = refreshToken };
        }
        public async Task<JwtAuthResult> GetRefreshToken(User user, JwtSecurityToken jwtToken, DateTime? expiryDate)
        {
            var (jwtSecurityToken, newToken) = await GenerateJWTToken(user);

            var response = new JwtAuthResult();

            response.AccessToken = newToken;

            var refreshTokenResult = new RefreshToken();

            refreshTokenResult.UserName = user.UserName;

            var refreshTokenGen = GenerateRefreshToken(user.UserName);

            refreshTokenResult.TokenString = refreshTokenGen.TokenString;

            refreshTokenResult.ExpireAt = (DateTime)expiryDate;

            response.refreshToken = refreshTokenResult;


            var UserRefreshToken = new UserRefreshToken
            {
                AddedTime = DateTime.UtcNow,
                ExpiryDate = refreshTokenGen.ExpireAt,
                RefreshToken = refreshTokenGen.TokenString,
                UserId = user.Id,
                IsRevoked = false,
                IsUsed = false,
                JwtId = jwtSecurityToken.Id,
                Token = response.AccessToken
            };

            await _RefreshTokenRepository.AddAsync(UserRefreshToken);

            return response;
        }
        public JwtSecurityToken ReadJWTToken(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentNullException(nameof(accessToken));
            }
            var handler = new JwtSecurityTokenHandler();
            var response = handler.ReadJwtToken(accessToken);
            return response;
        }
        public async Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken jwtToken, string accessToken, string refreshToken)
        {
            if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
            {
                return ("AlgorithmIsWrong", null);
            }

            if (jwtToken.ValidTo > DateTime.UtcNow)
            {
                return ("TokenIsNotExpired", null);
            }

            //Get User

            var userId = jwtToken.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimModel.Id)).Value;

            var userRefreshToken = await _RefreshTokenRepository.GetTableNoTracking()
                                             .FirstOrDefaultAsync(x => x.Token == accessToken &&
                                                                     x.RefreshToken == refreshToken &&
                                                                     x.UserId == int.Parse(userId));
            if (userRefreshToken == null)
            {
                return ("RefreshTokenIsNotFound", null);
            }

            if (userRefreshToken.ExpiryDate < DateTime.UtcNow)
            {
                userRefreshToken.IsRevoked = true;
                userRefreshToken.IsUsed = false;
                await _RefreshTokenRepository.UpdateAsync(userRefreshToken);
                return ("RefreshTokenIsExpired", null);
            }

            var expirydate = userRefreshToken.ExpiryDate;

            return (userId, expirydate);
        }
        private async Task<List<Claim>> GetClaims(User user)
        {
            var Roles = await _UserManager.GetRolesAsync(user);

            var claims = new List<Claim>()
            {
                new Claim(nameof(UserClaimModel.Id), user.Id.ToString()),
                new Claim(nameof(UserClaimModel.UserName), user.UserName),
                new Claim(nameof(UserClaimModel.Email), user.Email),
                new Claim(nameof(UserClaimModel.PhoneNumber), user.PhoneNumber),

            };

            foreach (var role in Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            ;

            var Claims = await _UserManager.GetClaimsAsync(user);
            claims.AddRange(Claims);

            return claims;
        }
        private async Task<(JwtSecurityToken, string)> GenerateJWTToken(User user)
        {
            string issuer = _JwtSettings.Issuer;

            string audience = _JwtSettings.Audience;

            IEnumerable<Claim> claims = await GetClaims(user);

            DateTime? expires = DateTime.UtcNow.AddMinutes(_JwtSettings.AccessTokenExpireDate);

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_JwtSettings.Secret)), SecurityAlgorithms.HmacSha256Signature);

            var jwtSecurityToken = new JwtSecurityToken(issuer, audience, claims, null, expires, signingCredentials);

            var AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return (jwtSecurityToken, AccessToken);
        }
        private RefreshToken GenerateRefreshToken(string username)
        {
            var randomNumber = new byte[32];

            var randomNumberGenerate = RandomNumberGenerator.Create();

            randomNumberGenerate.GetBytes(randomNumber);

            var token = Convert.ToBase64String(randomNumber);

            var refreshToken = new RefreshToken
            {
                TokenString = token,
                ExpireAt = DateTime.UtcNow.AddDays(_JwtSettings.RefreshTokenExpireDate),
                UserName = username
            };

            return refreshToken;

        }

        public async Task<string> ConfirmEmail(int? userId, string? code)
        {

            if (userId == null || code == null)
                return "ErrorWhenConfirmEmail";

            var user = await _UserManager.FindByIdAsync(userId.ToString());

            var confirmEmail = await _UserManager.ConfirmEmailAsync(user, code);

            if (!confirmEmail.Succeeded)
                return "ErrorWhenConfirmEmail";

            return ResultString.Success;
        }

        public async Task<string> SendResetPasswordCode(string Email)
        {
            var trans = await _DbContext.Database.BeginTransactionAsync();
            try
            {
                //find user
                var user = await _UserManager.FindByEmailAsync(Email);
                if (user == null)
                    return "UserNotFound";

                //Generate Random Number
                var chars = "0123456789";
                var random = new Random();
                var randomNumber = new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());

                //update User In Database Code
                user.Code = randomNumber;
                var updateResult = await _UserManager.UpdateAsync(user);

                if (!updateResult.Succeeded)
                    return "ErrorInUpdateUser";

                var message = "Code To Reset Passsword : " + user.Code;
                //Send Code To  Email 
                await _EmailService.SendEmailAsync(user.Email, message, "Reset Password");
                await trans.CommitAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                return "Failed";
            }
        }

        public async Task<string> ConfirmResetPassword(string Code, string Email)
        {


            //Get User
            var user = await _UserManager.FindByEmailAsync(Email);

            if (user == null)
                return "UserNotFound";

            var userCode = user.Code;

            //Equal With Code
            if (userCode == Code)
                return ResultString.Success;

            return ResultString.Failure;
        }

        public async Task<string> ResetPassword(string Email, string Password)
        {
            var trans = await _DbContext.Database.BeginTransactionAsync();
            try
            {
                //Get User
                var user = await _UserManager.FindByEmailAsync(Email);
                //user not Exist => not found
                if (user == null)
                    return "UserNotFound";

                await _UserManager.RemovePasswordAsync(user);

                if (!await _UserManager.HasPasswordAsync(user))
                {
                    await _UserManager.AddPasswordAsync(user, Password);
                }

                await trans.CommitAsync();

                return ResultString.Success;
            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                return ResultString.Failure;
            }
        }

        #endregion
    }
}
