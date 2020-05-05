using Empresa.Comum.Repositorios.Entity;
using Empresa.MinhaApi.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Empresa.MinhaApi.AcessoDados.Entity.Context;

namespace Empresa.MinhaApi.Repositorios.Entity
{
    public class RepositorioAluno : RepositorioEmpresa<Aluno, int>
    {
        public RepositorioAluno(MinhaApiDbContext context) : base(context) { } //Invocando o construtor da classe base (RepositorioEmpresa)

    }
}
