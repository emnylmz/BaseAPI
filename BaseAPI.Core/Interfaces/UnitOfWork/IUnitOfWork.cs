using System;
namespace BaseAPI.Core.Interfaces
{
	public interface IUnitOfWork
	{
		Task CommitAsync();
		void Commit();
	}
}

