using System;
namespace BaseAPI.Core.Interfaces.UnitOfWork
{
	public interface IUnitOfWork
	{
		Task CommitAsync();
		void Commit();
	}
}

