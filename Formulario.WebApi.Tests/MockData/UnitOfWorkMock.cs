using Formulario.Business.Leiaute;
using Formulario.Business.RepositoryPattern;
using System.Linq;
using System.Threading.Tasks;

namespace Formulario.WebApi.Tests.MockData
{
    public class UnitOfWorkMock<T> : IUnitOfWOrkGeneric<T> where T : IDatabaseFactory
    {
        private readonly T factory;

        public UnitOfWorkMock(T factory)
        {
            this.factory = factory;
        }

        public int Commit(string UserID)
        {            
            return 1;
        }

        public async Task<int> CommitAsync(string UserID)
        {
            return await Task.Run(() => { return 1; });
        }

        public void Rollback()
        {
        }
    }
}
