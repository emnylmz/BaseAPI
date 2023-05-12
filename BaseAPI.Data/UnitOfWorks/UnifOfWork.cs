using System;
using BaseAPI.Core.Interfaces.UnitOfWork;

namespace BaseAPI.Data.UnitOfWorks
{
	public class UnitOfWork:IUnitOfWork
	{
		private readonly AppDbContext context;

        public void Commit()
        {
            context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            context.SaveChangesAsync();
        }
    }
}

