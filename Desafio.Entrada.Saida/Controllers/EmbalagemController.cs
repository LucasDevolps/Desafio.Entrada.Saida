using Desafio.entrada.saida.Dominio.DTO;
using Desafio.entrada.saida.Dominio;
using Desafio.Entrada.Saida.Dominio.DTO.Request;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Desafio.Entrada.Saida.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmbalagemController : ControllerBase
    {
        private readonly IEmbalagemService _embalagemService;
        private readonly Serilog.ILogger _logger;

        /// <summary>
        /// Inicializa uma nova instância do controller de embalagem.
        /// </summary>
        /// <param name="embalagemService">O serviço de embalagem injetado.</param>
        public EmbalagemController(IEmbalagemService embalagemService)
        {
            _embalagemService = embalagemService;
            _logger = Log.ForContext<EmbalagemController>();
        }

        /// <summary>
        /// Processa uma lista de pedidos e retorna a solução de embalagem.
        /// </summary>
        /// <param name="pedidos">A lista de pedidos em formato JSON.</param>
        /// <returns>O resultado do processamento de embalagem.</returns>
        [HttpPost("processar-pedidos")]
        public ActionResult<EmbalagemResultadoResponse> ProcessarPedidos([FromBody] List<PedidoRequest> pedidos)
        {
            _logger.Information("Recebendo requisição para processar {Count} pedidos.", pedidos?.Count);

            if (pedidos == null || pedidos.Count == 0)
            {
                _logger.Warning("A lista de pedidos está vazia ou é nula.");
                return BadRequest("A lista de pedidos não pode estar vazia.");
            }

            try
            {
                // Converte a lista de pedidos para JSON para utilizar o método do serviço
                var pedidosJson = System.Text.Json.JsonSerializer.Serialize(pedidos);
                var resultado = _embalagemService.ProcessarPedidos(pedidosJson);

                if (!resultado.Sucesso)
                {
                    _logger.Error("Erro ao processar os pedidos: {Mensagem}", resultado.Mensagem);
                    return BadRequest(resultado.Mensagem);
                }

                _logger.Information("Pedidos processados com sucesso.");
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex, "Ocorreu um erro ao processar os pedidos.");
                return StatusCode(500, "Erro interno ao processar os pedidos.");
            }
        }
    }
}
