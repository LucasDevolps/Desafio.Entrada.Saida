using Desafio.entrada.saida.Dominio;
using Desafio.entrada.saida.Dominio.DTO;
using Desafio.Entrada.Saida.Dominio.DTO.Request;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.Entrada.Saida.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmbalagemController : ControllerBase
    {
        private readonly IEmbalagemService _embalagemService;

        /// <summary>
        /// Inicializa uma nova instância do controller de embalagem.
        /// </summary>
        /// <param name="embalagemService">O serviço de embalagem injetado.</param>
        public EmbalagemController(IEmbalagemService embalagemService)
        {
            _embalagemService = embalagemService;
        }

        /// <summary>
        /// Processa uma lista de pedidos e retorna a solução de embalagem.
        /// </summary>
        /// <param name="pedidos">A lista de pedidos em formato JSON.</param>
        /// <returns>O resultado do processamento de embalagem.</returns>
        [HttpPost("processar-pedidos")]
        public ActionResult<EmbalagemResultadoResponse> ProcessarPedidos([FromBody] List<PedidoRequest> pedidos)
        {
            if (pedidos == null || pedidos.Count == 0)
            {
                return BadRequest("A lista de pedidos não pode estar vazia.");
            }

            // Converte a lista de pedidos para JSON para utilizar o método do serviço
            var pedidosJson = System.Text.Json.JsonSerializer.Serialize(pedidos);
            var resultado = _embalagemService.ProcessarPedidos(pedidosJson);

            if (!resultado.Sucesso)
            {
                return BadRequest(resultado.Mensagem);
            }

            return Ok(resultado);
        }
    }
}
