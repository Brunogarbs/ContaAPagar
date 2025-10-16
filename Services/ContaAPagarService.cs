using Microsoft.EntityFrameworkCore;

public class ContaAPagarService : IContaAPagarService
{
    private readonly AppDbContext _context;

    public ContaAPagarService(AppDbContext context)
    {
        _context = context;
    }

    // Regra para o cadastro das Contas
    public async Task<Contas> CadastrarConta(CadastrarContaAPagarDto contaDto)
    {
        int diasEmAtraso = 0;
        decimal percentualMulta = 0m;
        decimal percentualJurosAoDia = 0m;
        decimal valorCorrigido = contaDto.ValorOriginal;

        //Verificando dias de atraso
        if (contaDto.DataDePagamento > contaDto.DataDeVencimento)
        {
            // TimeSpan -> resultado da subtração de duas datas
            TimeSpan diferenca = contaDto.DataDePagamento.Date - contaDto.DataDeVencimento.Date;
            diasEmAtraso = diferenca.Days;

            // Regra sobre as multas x dias de atraso 
            if (diasEmAtraso <= 3)
            {
                percentualMulta = 0.02m;
                percentualJurosAoDia = 0.001m;
            }
            else if (diasEmAtraso <= 5)
            {
                percentualMulta = 0.03m;
                percentualJurosAoDia = 0.002m;
            }
            else
            {
                percentualMulta = 0.05m;
                percentualJurosAoDia = 0.003m;
            }

            // Calcular valor Corrigido
            decimal valorMulta = contaDto.ValorOriginal * percentualMulta;
            decimal valorJuros = contaDto.ValorOriginal * (percentualJurosAoDia * diasEmAtraso);

            valorCorrigido = contaDto.ValorOriginal + valorMulta + valorJuros;
        }

        //Criaçao da conta
        var novaConta = new Contas
        {
            Nome = contaDto.Nome,
            ValorOriginal = contaDto.ValorOriginal,
            DataDeVencimento = contaDto.DataDeVencimento,
            DataDePagamento = contaDto.DataDePagamento,
            DiasEmAtraso = diasEmAtraso,
            ValorCorrigido = Math.Round(valorCorrigido, 2),
            MultaAplicadaPercentual = percentualMulta,
            JurosAoDiaAplicadoPercentual = percentualJurosAoDia
        };

        _context.Contas.Add(novaConta);
        await _context.SaveChangesAsync();

        return novaConta;
    }

    // Listagem das Contas
    public async Task<IEnumerable<Contas>> ListarContas()
    {
        return await _context.Contas.ToListAsync();
    }

    // Listar contas por id
    public async Task<Contas?> ListarContasId(int id)
    {
        return await _context.Contas.FindAsync(id);
    }

    // Atualizar Contas de Compra
    public async Task<Contas?> AtualizarContas(int id, CadastrarContaAPagarDto contaDto)
    {
        var ContaExistente = await _context.Contas.FindAsync(id);

        if (ContaExistente == null)
        {
            return null;
        }

        ContaExistente.Nome = contaDto.Nome;
        ContaExistente.ValorOriginal = contaDto.ValorOriginal;
        ContaExistente.DataDeVencimento = contaDto.DataDeVencimento;
        ContaExistente.DataDePagamento = contaDto.DataDePagamento;
        int diasEmAtraso = 0;
        decimal percentualMulta = 0m;
        decimal percentualJurosAoDia = 0m;
        decimal valorCorrigido = ContaExistente.ValorOriginal;

        if (ContaExistente.DataDePagamento > ContaExistente.DataDeVencimento)
        {
            TimeSpan diferenca = contaDto.DataDePagamento.Date - contaDto.DataDeVencimento.Date;
            diasEmAtraso = diferenca.Days;

            if (diasEmAtraso <= 3)
            {
                percentualMulta = 0.02m;
                percentualJurosAoDia = 0.001m;
            }
            else if (diasEmAtraso <= 5)
            {
                percentualMulta = 0.03m;
                percentualJurosAoDia = 0.001m;
            }
            else
            {
                percentualMulta = 0.05m;
                percentualJurosAoDia = 0.003m;
            }
            var valorMulta = contaDto.ValorOriginal * percentualMulta;
            var valorJuros = contaDto.ValorOriginal * percentualJurosAoDia * diasEmAtraso;

            ContaExistente.DiasEmAtraso = diasEmAtraso;
            ContaExistente.MultaAplicadaPercentual = percentualMulta;
            ContaExistente.JurosAoDiaAplicadoPercentual = percentualJurosAoDia;
            ContaExistente.ValorCorrigido = Math.Round(contaDto.ValorOriginal + valorMulta + valorJuros, 2);

        }
        else
        {
            ContaExistente.DiasEmAtraso = 0;
            ContaExistente.JurosAoDiaAplicadoPercentual = 0m;
            ContaExistente.MultaAplicadaPercentual = 0m;
            ContaExistente.ValorCorrigido = contaDto.ValorOriginal;
        }

        await _context.SaveChangesAsync();

        return ContaExistente;
    }

    public async Task<Contas?> ExcluirConta(int id)
    {
        var ContaExcluida = await _context.Contas.FindAsync(id);

        if (ContaExcluida == null)
        {
            return null;
        }

        _context.Contas.Remove(ContaExcluida);

        await _context.SaveChangesAsync();

        return ContaExcluida;

    }
}


