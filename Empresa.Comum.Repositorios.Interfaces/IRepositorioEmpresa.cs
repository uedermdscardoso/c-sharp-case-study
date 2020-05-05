using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Empresa.Comum.Repositorios.Interfaces
{
    public interface IRepositorioEmpresa<TDominio, TChave>
        where TDominio : class //Que o domínio seja uma classe
    {
        List<TDominio> Selecionar(Expression<Func<TDominio, bool>> where = null);

        TDominio SelecionarPorId(TChave id);

        void Inserir(TDominio dominio);

        void Atualizar(TDominio dominio);

        void Excluir(TDominio dominio);

        void ExcluirPorId(TChave id);

    }
}
