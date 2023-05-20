using System;
using BaseAPI.Core.Model;

namespace BaseAPI.Core.Interfaces.Authentication
{
	public interface IAuthenticationService
	{
		string Login(User user);	
	}
}

