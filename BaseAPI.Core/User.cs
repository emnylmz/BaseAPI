using System;
namespace BaseAPI.Core
{
	public class User:BaseEntity
	{
		public string Username { get; set; }
		public string Password { get; set; }
		public string Firstname { get; set; }
		public string Lastname { get; set; }
		public string Email { get; set; }
		public DateTime BirthDate { get; set; }
		public string Gender { get; set; }
		public DateTime? LastLoginDate { get; set; }
		public string? LastLoginIP { get; set; }
		public bool IsActive { get; set; }

	}
}

