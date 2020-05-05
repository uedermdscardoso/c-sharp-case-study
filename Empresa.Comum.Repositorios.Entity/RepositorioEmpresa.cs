using Empresa.Comum.Repositorios.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Repositório Genérico
namespace Empresa.Comum.Repositorios.Entity
{
    public abstract class RepositorioEmpresa<TDominio, TChave> : IRepositorioEmpresa<TDominio, TChave>
        where TDominio : class
    {
        protected DbContext _context;

        public RepositorioEmpresa(DbContext context) {
            _context = context;
        }

        public void Atualizar(TDominio dominio)
        {
            _context.Entry(dominio).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Excluir(TDominio dominio)
        {
            _context.Entry(dominio).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public void ExcluirPorId(TChave id)
        {
            TDominio dominio = SelecionarPorId(id);
            Excluir(dominio);
        }

        public void Inserir(TDominio dominio)
        {
            _context.Set<TDominio>().Add(dominio);
            _context.SaveChanges();
        }

        public List<TDominio> Selecionar(System.Linq.Expressions.Expression<Func<TDominio, bool>> where = null)
        {
            DbSet<TDominio> dbSet = _context.Set<TDominio>();
            if (where == null)
                return dbSet.ToList();
            else
                return dbSet.Where(where).ToList();
        }

        public TDominio SelecionarPorId(TChave id)
        {
            return _context.Set<TDominio>().Find(id);
        }
    }
}
