using Formulario.RepositoryPattern;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulario.Data.Infrastructure
{
    public class UnitOfWorkGeneric : IUnitOfWOrkGeneric<IDatabaseFactory>
    {
        private readonly IDatabaseFactory factory;

        public UnitOfWorkGeneric(IDatabaseFactory factory)
        {
            this.factory = factory;
        }

        public int Commit(string UserID)
        {
            var qtd = (factory.Get() as FormularioContext).SaveChanges();
            return qtd;
        }

        public async Task<int> CommitAsync(string UserID)
        {
            var qtd = await (factory.Get() as FormularioContext).SaveChangesAsync();
            return qtd;
        }

        public void Rollback()
        {
            var entries = (factory.Get() as DbContext).ChangeTracker.Entries();

            foreach (var item in entries)
            {
                switch (item.State)
                {
                    case EntityState.Added:
                        item.State = EntityState.Detached;
                        break;
                    case EntityState.Deleted:
                        item.State = EntityState.Unchanged;
                        break;
                    case EntityState.Modified:
                        item.CurrentValues.SetValues(item.OriginalValues);
                        item.State = EntityState.Unchanged;
                        break;
                }
            }
        }
    }
}
