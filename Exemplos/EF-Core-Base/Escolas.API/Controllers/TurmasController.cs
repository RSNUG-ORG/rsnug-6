using CSharpFunctionalExtensions;
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

        [Route("")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> CriarAsync([FromBody]NovaTurmaInputModel novaTurmaInputModel)
        {
            Result<TurmaBase> turma;
            if (novaTurmaInputModel.ComDuracao)
                turma = TurmaBase.CriarComDuracao(
                            novaTurmaInputModel.Descricao,
                            novaTurmaInputModel.LimiteIdade,
                            novaTurmaInputModel.QuantidadeMinimaAlunos,
                            novaTurmaInputModel.QuantidadeMaximaAlunos,
                            novaTurmaInputModel.DuracaoTipo,
                            novaTurmaInputModel.DuracaoValor);
            else
                turma = TurmaBase.CriarComDuracaoIlimitada(
                            novaTurmaInputModel.Descricao,
                            novaTurmaInputModel.LimiteIdade,
                            novaTurmaInputModel.QuantidadeMinimaAlunos,
                            novaTurmaInputModel.QuantidadeMaximaAlunos);

            if (turma.IsFailure)
                return BadRequest(turma.Error);

            await _turmasRepositorio.AdicionarAsync(turma.Value);
            await _unitOfWork.CommitAsync();

            return CreatedAtAction(nameof(TurmaPorId), new { id = turma.Value.Id }, null);
        }

        [Route("{id:int}")]
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> ExcluirAsync(int id)
        {
            if (await _turmasRepositorio.RecuperarAsync(id) is var turma && turma.HasNoValue)
                return NotFound();

            turma.Value.Excluir();

            await _unitOfWork.CommitAsync();

            return Ok();
        }
    }
}
