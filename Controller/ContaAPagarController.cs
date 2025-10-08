
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ContaAPagarController : ControllerBase
{
    private readonly IContaAPagarService _contaAPagarService;

    public ContaAPagarController(IContaAPagarService contaAPagarService)
    {
        _contaAPagarService = contaAPagarService;
    }

    [HttpPost]
    //[FromBody] pega os dados e tenta transformar em um objeto CadastrarContaAPagarDto
    public async Task<IActionResult> CriarConta([FromBody] CadastrarContaAPagarDto contaAPagarDto)
    {
        try
        {
            var novaConta = await _contaAPagarService.CadastrarConta(contaAPagarDto);
            return CreatedAtAction(null, new { id = novaConta.Id }, novaConta);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Ocorreu um erro ao criar a conta.", error = ex.Message });
        }
    }
    [HttpGet]
    public async Task<IActionResult> ListarContas()
    {
        var contas = await _contaAPagarService.ListarContas();
        return Ok(contas);
    }
}