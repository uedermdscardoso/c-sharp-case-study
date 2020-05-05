using Empresa.Comum.Repositorios.Interfaces;
using Empresa.MinhaApi.Api.AutoMapper;
using Empresa.MinhaApi.Api.DTOs;
using Empresa.MinhaApi.Api.filters;
using Empresa.MinhaApi.Dominio;
using Empresa.MinhaApi.Repositorios.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Empresa.MinhaApi.Api.Controllers
{
    [Authorize] //Só será acessível se tiver autenticado
    [RoutePrefix("api/alunos")]
    public class AlunosController : ApiController
    {
        private IRepositorioEmpresa<Aluno, int> _repositorioAlunos = new RepositorioAluno(new AcessoDados.Entity.Context.MinhaApiDbContext());

        /*public IEnumerable<Aluno> Get() {
            return _repositorioAlunos.Selecionar();
        }

        public HttpResponseMessage Get(int? id) { //Precisa fazer match com o formato da rota
            if (!id.HasValue)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            Aluno aluno = _repositorioAlunos.SelecionarPorId(id.Value);

            if (aluno == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            return Request.CreateResponse(HttpStatusCode.Found, aluno);
        }

        public HttpResponseMessage Post([FromBody]Aluno aluno) {

            try
            {
                _repositorioAlunos.Inserir(aluno);
                return Request.CreateResponse(HttpStatusCode.Created);
            }
            catch (Exception ex) {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }*/

        //Sem automapper
        /*public IHttpActionResult Get()
        {
            return Ok(_repositorioAlunos.Selecionar());
        }

        public IHttpActionResult Get(int? id) // IHttpActionResult - Mais recomendado
        { //Precisa fazer match com o formato da rota
            if (!id.HasValue) return BadRequest();

            Aluno aluno = _repositorioAlunos.SelecionarPorId(id.Value);

            if (aluno == null) return NotFound();

            return Content(HttpStatusCode.Found, aluno);
        }

        public IHttpActionResult Post([FromBody]Aluno aluno) //Mais recomendado com IHttpActionResult
        {

            try
            {
                _repositorioAlunos.Inserir(aluno);
                return Created($"{Request.RequestUri}/{aluno.Id}", aluno);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult Put(int? id, [FromBody] Aluno aluno) {
            try
            {
                if (!id.HasValue)
                    return BadRequest();

                aluno.Id = id.Value;
                _repositorioAlunos.Atualizar(aluno);
                return Ok();
            }
            catch (Exception ex) {
                return InternalServerError();
            }
        }

        public IHttpActionResult Delete(int? id)
        {
            try {
                if (!id.HasValue)
                    return BadRequest();

                Aluno aluno = _repositorioAlunos.SelecionarPorId(id.Value);

                if (aluno == null)
                    return NotFound();

                _repositorioAlunos.ExcluirPorId(id.Value);
                return Ok();

            } catch (Exception ex) {
                return InternalServerError();
            }
        }*/

        //Com automapper
        public IHttpActionResult Get()
        {
            List<Aluno> alunos = _repositorioAlunos.Selecionar();
            List<AlunoDTO> alunosDTO = AutoMapperManager.Instance.Mapper.Map<List<Aluno>,List<AlunoDTO>>(alunos); //Já está carregando com opções de mapeamento
            return Ok(alunosDTO);
        }

        public IHttpActionResult Get(int? id) // IHttpActionResult - Mais recomendado
        { //Precisa fazer match com o formato da rota
            if (!id.HasValue) return BadRequest();

            Aluno aluno = _repositorioAlunos.SelecionarPorId(id.Value);

            if (aluno == null) return NotFound();

            AlunoDTO alunoDTO = AutoMapperManager.Instance.Mapper.Map<Aluno, AlunoDTO>(aluno);

            return Content(HttpStatusCode.OK, alunoDTO); //Found é de erro e não de sucesso
        }

        [Route("por-nome/{nomeAluno}")]
        public IHttpActionResult Get(string nomeAluno)
        {
            List<Aluno> alunos = _repositorioAlunos.Selecionar(s => s.Nome.ToLower().Contains(nomeAluno.ToLower()));

            List<AlunoDTO> alunosDTO = AutoMapperManager.Instance.Mapper.Map<List<Aluno>, List<AlunoDTO>>(alunos);

            return Ok(alunosDTO);
        }

        //Com if/else
        /*public IHttpActionResult Post([FromBody]AlunoDTO alunoDTO) //Mais recomendado com IHttpActionResult
        {
            if (ModelState.IsValid)
            {

                try
                {
                    Aluno aluno = AutoMapperManager.Instance.Mapper.Map<AlunoDTO, Aluno>(alunoDTO); //Mapear do AlunoDTO para Aluno

                    _repositorioAlunos.Inserir(aluno);
                    return Created($"{Request.RequestUri}/{aluno.Id}", aluno);
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }
            }
            else {
                return BadRequest(ModelState); //Retorna as informações de erros que ocorreram
            }
        }*/

        [ApplyModelValidation]  //Decorator filtro
        public IHttpActionResult Post([FromBody]AlunoDTO alunoDTO) //Mais recomendado com IHttpActionResult
        {
            try
            {
                Aluno aluno = AutoMapperManager.Instance.Mapper.Map<AlunoDTO, Aluno>(alunoDTO); //Mapear do AlunoDTO para Aluno

                _repositorioAlunos.Inserir(aluno);
                return Created($"{Request.RequestUri}/{aluno.Id}", aluno);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [ApplyModelValidation] //Decorator filtro
        public IHttpActionResult Put(int? id, [FromBody] AlunoDTO alunoDTO)
        {
            try
            {
                if (!id.HasValue)
                    return BadRequest();

                Aluno aluno = AutoMapperManager.Instance.Mapper.Map<AlunoDTO, Aluno>(alunoDTO); //Mapear AlunoDTO para Aluno

                aluno.Id = id.Value;
                _repositorioAlunos.Atualizar(aluno);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

        public IHttpActionResult Delete(int? id)
        {
            try
            {
                if (!id.HasValue)
                    return BadRequest();

                Aluno aluno = _repositorioAlunos.SelecionarPorId(id.Value);

                if (aluno == null)
                    return NotFound();

                _repositorioAlunos.ExcluirPorId(id.Value);
                return Ok();

            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

    }
}
