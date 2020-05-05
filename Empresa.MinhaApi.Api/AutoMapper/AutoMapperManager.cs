using AutoMapper;
using Empresa.MinhaApi.Api.DTOs;
using Empresa.MinhaApi.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Empresa.MinhaApi.Api.AutoMapper
{
    //Gerenciador do AutoMapper
    public class AutoMapperManager
    {
        //Criação da instância
        private static readonly Lazy<AutoMapperManager> _instance = new Lazy<AutoMapperManager>(() => { //É estático porque garante que terá apenas uma única instância
            return new AutoMapperManager(); //Sempre irá retorna a instância que foi gerada pela primeira vez
        }); //Framework 4.5

        public static AutoMapperManager Instance { //Retorna a mesma instância do autoMapper
            get { return _instance.Value; //retorna o valor do lazy 
        } }

        private MapperConfiguration _config;

        public IMapper Mapper { get { return _config.CreateMapper(); } }

        //ctor tab tab - criar o construtor
        private  AutoMapperManager()
        {
            //Carregar as configurações de conversão do autoMapper
            _config = new MapperConfiguration((ctg) => {
                ctg.CreateMap<Aluno, AlunoDTO>(); //Indo do Aluno para AlunoDTO -- Ensinando o AutoMapper a fazer conversão do domínio Aluno para AlunoDTO
                ctg.CreateMap<AlunoDTO, Aluno>();
            }); //Faz mapeamento entre as entidades envolvidas (domínios/classes e DTOs)
        }
    }
}