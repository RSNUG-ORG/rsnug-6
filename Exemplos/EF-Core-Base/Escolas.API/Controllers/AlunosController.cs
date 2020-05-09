﻿using Escola.Dominio.Alunos;
using Escola.Infra.EF;
using Escolas.API.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Escolas.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public sealed class AlunosController : ControllerBase
    {
        private readonly ILogger<AlunosController> _logger;
        private readonly IAlunosRepositorio _alunosRepositorio;
        private readonly EscolaContextoEF _unitOfWork;

        public AlunosController(
            ILogger<AlunosController> logger,
            IAlunosRepositorio alunosRepositorio,
            EscolaContextoEF unitOfWork)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _alunosRepositorio = alunosRepositorio;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("alunos/{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(AlunoViewModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<AlunoViewModel>> AlunoPorId(int id)
        {
            //if (id <= 0)
            //    return BadRequest();
            return NotFound();
        }

        [Route("")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> CriarAsync([FromBody]NovoAlunoInputModel novoAlunoRequest)
        {
            var aluno = Aluno.Criar(
                novoAlunoRequest.PrimeiroNome, 
                novoAlunoRequest.Sobrenome, 
                novoAlunoRequest.Email, 
                novoAlunoRequest.DataNascimento,
                novoAlunoRequest.Sexo);
            if (aluno.IsFailure)
                return BadRequest(aluno.Error);

            if(await _alunosRepositorio.RecuperarPorEmailAsync(aluno.Value.Email) is var alunoExistente && alunoExistente.HasValue )
                return BadRequest("Já existe um aluno cadastrado com este e-mail");

            await _alunosRepositorio.AdicionarAsync(aluno.Value);
            await _unitOfWork.CommitAsync();

            return CreatedAtAction(nameof(AlunoPorId), new { id = aluno.Value.Id }, null);
        }
    }
}