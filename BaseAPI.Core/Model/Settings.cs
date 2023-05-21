using System;
namespace BaseAPI.Core.Model
{
	public class Settings
	{
		public string SecretKeyBase64 { get; set; }

		public string ProjectName { get; set; }

		public string ProjectVersion { get; set; }

		public string MsSQLConnection { get; set; }

		public JWTSettings JWTSettings { get; set; }
	}

	public class JWTSettings
    {
		public string SecretKey { get; set; }

        public int ExpireMinutes { get; set; }

		public string Issuer { get; set; }

		public string Audience { get; set; }
	}
}

