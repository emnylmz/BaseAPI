using System;
namespace BaseAPI.Core.Model
{
	public class Role
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public bool IsActive { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }

    }
}

