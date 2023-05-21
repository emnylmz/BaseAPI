using System;
using BaseAPI.Core.Model;

namespace BaseAPI.Core.Interfaces
{
	public interface IAuthenticationService
	{
		string Login(User user);	
	}
}

