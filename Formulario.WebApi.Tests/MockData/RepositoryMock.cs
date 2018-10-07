using Formulario.Business;
using Formulario.Business.Leiaute;
using Formulario.Business.Perguntas;
using Formulario.Business.RepositoryPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Formulario.WebApi.Tests.MockData
{
    public class RepositoryMock<T> : IRepository<T>, IDisposable where T : class
    {
        public static HashSet<T> database = new HashSet<T>();

        public void Dispose()
        {

        }

        public T Create()
        {
            return Activator.CreateInstance<T>();
        }

        public void Delete(T obj)
        {
            if (database.Contains(obj))
            {
                database.Remove(obj);

                if (typeof(T).Equals(typeof(Pergunta)))
                {
                    foreach (var m in RepositoryMock<ModeloDeFormulario>.database)
                    {
                        foreach (var p in m.Perguntas.ToList())
                        {
                            if (p.Equals(obj))
                                m.Perguntas.Remove(p);
                        }
                    }
                }
            }
        }

        public void Detach(T obj)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetQuery(bool TrackingChanges = true)
        {
            return database.AsQueryable();
        }

        public T Insert(T obj)
        {
            database.Add(obj);

            if (typeof(T).Equals(typeof(Pergunta)))
            {

            }

            if (typeof(T).Equals(typeof(ModeloDeFormulario)))
            {
                var maxID = RepositoryMock<ModeloDeFormulario>.database.Select(c => c.ModeloFormularioID).DefaultIfEmpty(0).Max();
                RepositoryMock<ModeloDeFormulario>.database.Where(c => c.ModeloFormularioID < 1).ToList().ForEach(c => c.ModeloFormularioID = ++maxID);
            }

            if (typeof(T).Equals(typeof(RespostaModeloDeFormulario)))
            {
                var maxID = RepositoryMock<RespostaModeloDeFormulario>.database.Select(c => c.RespostaModeloFormularioID).DefaultIfEmpty(0).Max();
                RepositoryMock<RespostaModeloDeFormulario>.database.Where(c => c.RespostaModeloFormularioID < 1).ToList().ForEach(c => c.RespostaModeloFormularioID = ++maxID);
            }

            if (typeof(T).Equals(typeof(LeiautePergunta)))
            {
                var maxID = RepositoryMock<LeiautePergunta>.database.Select(c => c.LeiautePerguntaID).DefaultIfEmpty(0).Max();
                RepositoryMock<LeiautePergunta>.database.Where(c => c.LeiautePerguntaID < 1).ToList().ForEach(c => c.LeiautePerguntaID = ++maxID);
                RepositoryMock<LeiautePergunta>.database.ToList().ForEach(c => c.LeiauteItem.ToList().ForEach(d => d.LeiautePerguntaID = c.LeiautePerguntaID));
            }

            if (typeof(T).Equals(typeof(LeiautePerguntaItem)))
            {
                var maxID = RepositoryMock<LeiautePerguntaItem>.database.Select(d => d.LeiautePerguntaItemID).DefaultIfEmpty(0).Max();
                RepositoryMock<LeiautePerguntaItem>.database.Where(c => c.LeiautePerguntaItemID < 1).ToList().ForEach(c => c.LeiautePerguntaItemID = ++maxID);
            }

            return obj;
        }

        public void Update(T obj)
        {
            database.Remove(obj);
            database.Add(obj);
        }
    }
}
