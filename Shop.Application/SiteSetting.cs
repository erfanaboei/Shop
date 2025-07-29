using System;

namespace Shop.Application
{
    public class SiteSetting
    {
        public JwtSetting JwtSetting { get; set; }
        public IdentitySetting IdentitySetting { get; set; }
    }

    public class JwtSetting
    {
        public string SecretKey { get; set; }
        public string EncryptKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpirationDay { get; set; }
        public int NotBeforeMinute { get; set; }
    }

    public class IdentitySetting
    {
        public bool PasswordRequireDigit { get; set; }
        public bool PasswordRequireLowercase { get; set; }
        public bool PasswordRequireUppercase { get; set; }
        public int PasswordRequiredLength { get; set; }
        public bool PasswordRequireNonAlphanumeric { get; set; }
        public bool UserRequireUniqueEmail { get; set; }
    }
}