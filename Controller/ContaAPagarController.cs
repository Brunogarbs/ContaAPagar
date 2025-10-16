
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

    [HttpGet("{id}")]
    public async Task<IActionResult> ListarContasId(int id)
    {
        var conta = await _contaAPagarService.ListarContasId(id);

        if (conta == null)
        {
            return NotFound();
        }
        else
            return Ok(conta);

    }
    [HttpPut("{id}")]
    //[FromBody] pega os dados e tenta transformar em um objeto CadastrarContaAPagarDto
    public async Task<IActionResult> AtualizarContas(int id, [FromBody] CadastrarContaAPagarDto contaAPagarDto)
    {
        try
        {
            var ContaExistente = await _contaAPagarService.AtualizarContas(id, contaAPagarDto);
            if (ContaExistente == null)
            {
                return NotFound();
            }
            return Ok(ContaExistente);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Ocorreu um erro ao atualizar a conta.", error = ex.Message });
        }
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> ExcluirConta(int id)
    {
        try
        {
            var ContaExcluida = await _contaAPagarService.ExcluirConta(id);
            if (ContaExcluida == null)
            {
                return NotFound();
            }
            return Ok(ContaExcluida);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Ocorreu um erro ao excluir a conta.", error = ex.Message });
        }
    }
}