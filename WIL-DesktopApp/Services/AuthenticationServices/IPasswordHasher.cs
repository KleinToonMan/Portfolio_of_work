namespace WIL_DesktopApp.Services.AuthenticationServices
{
    internal interface IPasswordHasher
    {
        public string HashPassword(string password);
        public bool VerifyHash(string password, string hashedPassword);
    }
}
