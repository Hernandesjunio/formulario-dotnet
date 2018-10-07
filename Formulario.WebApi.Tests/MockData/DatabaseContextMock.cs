using Formulario.Business.RepositoryPattern;
using System;
using System.Collections.Generic;

namespace Formulario.WebApi.Tests.MockData
{
    public class DatabaseContextMock : IDatabaseContext
    {
        List<Action> lst = new List<Action>();

        public void Dispose()
        {
            lst.ForEach(c => c());
            lst.Clear();
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            var repo = new RepositoryMock<T>();
            lst.Add(() => RepositoryMock<T>.database.Clear());
            return repo;
        }
    }
}
