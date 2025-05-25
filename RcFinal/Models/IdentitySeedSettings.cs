namespace RcFinal.Models
{
    public class IdentitySeedSettings
    {
        public List<string> Roles { get; set; } = new();
        public AdminUserSettings AdminUser { get; set; } = new();
    }
}
