using System.Collections.Specialized;

namespace ShopRoles
{
    public static class RolesSh
    {
        public static string AdminRole = "Admin";
        public static string CustomerRole = "Customer";
        public static string EmpRole = "Editor";

        public const string Pending = "Pending";
        public const string Approve = "Approved";
        public const string Processing = "Processing";
        public const string Cancelled = "Cancelled";
        public const string Shipped = "Shipped";
        public const string Refund = "Refund";
        public const string Rejected = "Rejected";

        public const string SessionKey = "ShoppingCartSession";

    }
}
