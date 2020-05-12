﻿using CSharpFunctionalExtensions;
using Escola.Dominio.Alunos;
using Escola.Dominio.Turmas;
using Escola.Infra.EF;
using Escola.API.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Escola.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public sealed class TurmasController : ControllerBase
    {
        private readonly ILogger<TurmasController> _logger;
        private readonly ITurmasRepositorio _turmasRepositorio;
        private readonly EscolaContextoEF _unitOfWork;

        public TurmasController(
            ILogger<TurmasController> logger,
            ITurmasRepositorio turmasRepositorio,
            EscolaContextoEF unitOfWork)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _turmasRepositorio = turmasRepositorio;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(TurmaViewModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<TurmaViewModel>> TurmaPorId(int id)
        {
            if (id <= 0)
                return BadRequest();
            if(await _turmasRepositorio.RecuperarAsync(id) is var turma && turma.HasNoValue)
                return NotFound();

            return Ok(new TurmaViewModel());
        }

        //[Route("")]
        //[HttpPost]
        //[ProducesResponseType((int)HttpStatusCode.Created)]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest)]
        //public async Task<ActionResult> CriarAsync([FromBody]NovoAlunoInputModel novoAlunoRequest)
        //{
        //    var aluno = Aluno.Criar(
        //        novoAlunoRequest.PrimeiroNome, 
        //        novoAlunoRequest.Sobrenome, 
        //        novoAlunoRequest.Email, 
        //        novoAlunoRequest.DataNascimento,
        //        novoAlunoRequest.Sexo);
        //    if (aluno.IsFailure)
        //        return BadRequest(aluno.Error);

        //    if(await _turmasRepositorio.RecuperarPorEmailAsync(aluno.Value.Email) is var alunoExistente && alunoExistente.HasValue )
        //        return BadRequest("Já existe um aluno cadastrado com este e-mail");

        //    await _turmasRepositorio.AdicionarAsync(aluno.Value);
        //    await _unitOfWork.CommitAsync();

        //    return CreatedAtAction(nameof(AlunoPorId), new { id = aluno.Value.Id }, null);
        //}

        //[Route("{id:int}")]
        //[HttpPut]
        //[ProducesResponseType((int)HttpStatusCode.NotFound)]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest)]
        //[ProducesResponseType((int)HttpStatusCode.OK)]
        //public async Task<ActionResult> AtualizarAsync(
        //    [FromBody]EditarAlunoInputModel alunoEditadoInputModel,
        //    int id)
        //{
        //    if (await _turmasRepositorio.RecuperarAsync(id) is var aluno && aluno.HasNoValue)
        //        return NotFound();

        //    var resultadoNome = aluno.Value.AtualizarNome(alunoEditadoInputModel.PrimeiroNome, alunoEditadoInputModel.Sobrenome);
        //    var resultadoNascimento = aluno.Value.AtualizarDataNascimento(alunoEditadoInputModel.DataNascimento);

        //    if (Result.Combine(resultadoNome, resultadoNascimento) is var resultado && resultado.IsFailure)
        //        return BadRequest(resultado.Error);

        //    await _unitOfWork.CommitAsync();

        //    return Ok();
        //}
    }
}
