namespace PrimeStore.Data.AppMetaData
{
    public static class Router
    {
        public const string SignleRoute = "/{id}";

        public const string root = "Api";
        public const string version = "V1";
        public const string Rule = root + "/" + version + "/";

        public static class ProductRouting
        {
            public const string Prefix = Rule + "Product";
            public const string List = Prefix + "/List";
            public const string GetByID = Prefix + "/GetById";
            public const string Create = Prefix + "/Create";
            public const string Edit = Prefix + "/Edit";
            public const string Delete = Prefix + "/Delete";
            public const string Paginated = Prefix + "/Paginated";
            public const string ActivePaginated = Prefix + "/ActivePaginated";
            public const string PaginatedByBrandId = Paginated + "/Brand";
            public const string PaginatedByCategoryId = Paginated + "/Category";

        }
        public static class BrandRouting
        {
            public const string Prefix = Rule + "Brand";
            public const string List = Prefix + "/List";
            public const string ActiveList = Prefix + "/ActiveList";
            public const string Paginated = Prefix + "/Paginated";
            public const string Create = Prefix + "/Create";
            public const string Edit = Prefix + "/Edit";
            public const string Delete = Prefix + "/Delete";
            public const string GetByID = Prefix + "/Get";
        }
        public static class CategoryRouting
        {
            public const string Prefix = Rule + "Category";
            public const string List = Prefix + "/List";
            public const string Create = Prefix + "/Create";
            public const string Edit = Prefix + "/Edit";
            public const string Delete = Prefix + "/Delete";
        }
        public static class UserRouting
        {
            public const string Prefix = Rule + "User";
            public const string Paginated = Prefix + "/Paginated";
            public const string GetById = Prefix + "/GetById";
            public const string Create = Prefix + "/Create";
            public const string Edit = Prefix + "/Edit";
            public const string EditRole = Prefix + "/EditRole";
            public const string EditStatus = Prefix + "/EditStatus";
            public const string Delete = Prefix + "/Delete";
            public const string UserRoles = Prefix + "/UserRoles";
            public const string UserClaims = Prefix + "/UserClaims";
            public const string EditUserRoles = Prefix + "/EditUserRoles";
            public const string EditUserClaims = Prefix + "/EditUserClaims";
            public const string EditUserStatus = Prefix + "/EditUserStatus";

        }
        public static class ReviewRouting
        {
            public const string Prefix = Rule + "Review";
            public const string UserReviewsPaginated = Prefix + "/UserReviewsPaginated";
            public const string ProductReviewsPaginated = Prefix + "/ProductReviewsPaginated";
            public const string Create = Prefix + "/Create";
            public const string Edit = Prefix + "/Edit";
            public const string Delete = Prefix + "/Delete";
        }

        public static class WishlistRouting
        {
            public const string Prefix = Rule + "Wishlist";
            public const string UserWishlistPaginated = Prefix + "/UserWishlistPaginated";
            public const string Create = Prefix + "/Create";
            public const string Delete = Prefix + "/Delete";
            public const string IsProductInUserWishlist = Prefix + "/IsProductInUserWishlist";
        }
        public static class AuthenticationRouting
        {
            public const string Prefix = Rule + "Authentication";
            public const string SignIn = Prefix + "/SignIn";
            public const string RefreshToken = Prefix + "/Refresh-Token";
            public const string ValidateToken = Prefix + "/Validate-Token";
            public const string ConfirmEmail = Prefix + "/ConfirmEmail";
            public const string SendResetPasswordCode = Prefix + "/SendResetPasswordCode";
            public const string ConfirmResetPasswordCode = Prefix + "/ConfirmResetPasswordCode";
            public const string ResetPassword = Prefix + "/ResetPassword";

        }

        public static class AuthorizationRouting
        {
            public const string Prefix = Rule + "AuthorizationRouting";
            public const string Roles = Prefix + "/Roles";
            public const string Claims = Prefix + "/Claims";
            public const string Create = Roles + "/Create";
            public const string Edit = Roles + "/Edit";
            public const string Delete = Roles + "/Delete/{id}";
            public const string RoleList = Roles + "/Role-List";
            public const string GetRoleById = Roles + "/Role-By-Id/{id}";
            public const string ManageUserRoles = Roles + "/Manage-User-Roles/{userId}";
            public const string ManageUserClaims = Claims + "/Manage-User-Claims/{userId}";
            public const string UpdateUserRoles = Roles + "/Update-User-Roles";
            public const string UpdateUserClaims = Claims + "/Update-User-Claims";
        }

        public static class EmailsRoute
        {
            public const string Prefix = Rule + "EmailsRoute";
            public const string SendEmail = Prefix + "/SendEmail";
        }
        public static class ImagesRouting
        {
            public const string Prefix = Rule + "Image";
            public const string AddImage = Prefix + "/upload";
            public const string DeleteImage = Prefix + "/Delete";
            public const string Paginated = Prefix + "/Paginated";
        }

        public static class CartRouting
        {
            public const string Prefix = Rule + "CartItem";
            public const string AddCartItem = Prefix + "/Add";
            public const string EditCartItem = Prefix + "/Edit";
            public const string DeleteCartItem = Prefix + "/Delete";
            public const string GetUserCartItems = Prefix + "/GetUserCartItems";
        }

        public static class PaymentRouting
        {
            public const string Prefix = Rule + "Payment";
            public const string AddPayment = Prefix + "/Add";
            public const string EditPaymentStatus = Prefix + "/EditPaymentStatus";
            public const string GetOrderPayment = Prefix + "/GetOrderPayment";
        }

        public static class OrderRouting
        {
            public const string Prefix = Rule + "Order";
            public const string GetUserOrders = Prefix + "/GetUserOrders";
            public const string GetOrderItems = Prefix + "/GetOrderItems";
            public const string EditUserOrderStatus = Prefix + "/EditUserOrderStatus";
        }

    }
}
