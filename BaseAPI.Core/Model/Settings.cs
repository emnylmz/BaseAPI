using System;
namespace BaseAPI.Core.Model
{
	public class Settings
	{
		public string SecretKeyBase64 { get; set; }

		public string MsSQLConnection { get; set; }

		public int ExpireMinutes { get; set; }
	}
}

