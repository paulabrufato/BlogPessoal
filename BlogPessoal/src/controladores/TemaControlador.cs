using System.Threading.Tasks;
using BlogPessoal.src.dtos;
using BlogPessoal.src.repositorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogPessoal.src.controladores
{
    [ApiController]
    [Route("api/Temas")]
    [Produces("application/json")]
    public class TemaControlador : ControllerBase
    {
        #region Atributos
        
        private readonly ITema _repositorio;

        #endregion


        #region Construtores

        public TemaControlador(ITema repositorio)
        {
            _repositorio = repositorio;
        }

        #endregion


        #region Métodos
        /// <summary>
        /// Resumo: Pegar todos os temas
        /// </summary>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retornar todos os temas</response>
        /// <response code="204">Sem conteúdo</response>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> PegarTodosTemasAsync()
        {
            var lista = await _repositorio.PegarTodosTemasAsync();

            if (lista.Count < 1) return NoContent();
            
            return Ok(lista);
        }

        /// <summary>
        /// Resumo: Pegar tema pelo Id
        /// </summary>
        /// <param name="idTema">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retorna o tema</response>
        /// <response code="404">Tema não existente</response>
        [HttpGet("id/{idTema}")]
        [Authorize]
        public async Task<ActionResult> PegarTemaPeloIdAsync([FromRoute] int idTema)
        {
            var tema = await _repositorio.PegarTemaPeloIdAsync(idTema);

            if (tema == null) return NotFound();
            
            return Ok(tema);
        }

        /// <summary>
        /// Resumo: Pegar temas pela descrição
        /// </summary>
        /// <param name="descricaoTema">string</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retornar tema</response>
        /// <response code="204">Tema sem conteúdo</response>
        [HttpGet("pesquisa")]
        [Authorize]
        public async Task<ActionResult> PegarTemasPelaDescricaoAsync([FromQuery] string descricaoTema)
        {
            var temas = await _repositorio.PegarTemasPelaDescricaoAsync(descricaoTema);

            if (temas.Count < 1) return NoContent();
            
            return Ok(temas);
        }

        /// <summary>
        /// Resumo: Criar novo Tema
        /// </summary>
        /// <param name="tema">NovoTemaDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /api/Tema
        ///     {
        ///        "descricao": "C#"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Retorna tema criado</response>
        /// <response code="400">Erro na requisição</response>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> NovoTemaAsync([FromBody] NovoTemaDTO tema)
        {
            if(!ModelState.IsValid) return BadRequest();

            await _repositorio.NovoTemaAsync(tema);
            
            return Created($"api/Temas", tema);
        }

        /// <summary>
        /// Resumo: Atualizar Tema
        /// </summary>
        /// <param name="tema">AtualizarTemaDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     PUT /api/Tema
        ///     {
        ///        "id": "1",   
        ///        "descricao": "Python"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Retorna tema atualizado</response>
        /// <response code="400">Erro na requisição</response>
        [HttpPut]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult> AtualizarTema([FromBody] AtualizarTemaDTO tema)
        {
            if(!ModelState.IsValid) return BadRequest();

            await _repositorio.AtualizarTemaAsync(tema);
            
            return Ok(tema);
        }

        /// <summary>
        /// Resumo: Deletar tema pelo Id
        /// </summary>
        /// <param name="idTema">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="204">Tema deletado</response>
        [HttpDelete("deletar/{idTema}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult> DeletarTema([FromRoute] int idTema)
        {
            await _repositorio.DeletarTemaAsync(idTema);
            return NoContent();
        }
        #endregion
    }
}