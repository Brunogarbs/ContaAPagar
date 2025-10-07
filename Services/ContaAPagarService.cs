public class ContaAPagarService : IContaAPagarService
{
    private readonly AppDbContext _context;

    public ContaAPagarService(AppDbContext context)
    {
        _context = context;
    }

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
                percentualJurosAoDia = 0.0001m;
            }
            else if (diasEmAtraso <= 5)
            {
                percentualMulta = 0.03m;
                percentualJurosAoDia = 0.0002m;
            }
            else
            {
                percentualMulta = 0.05m;
                percentualJurosAoDia = 0.0003m;
            }

            // Calcular valor Corrigido
            decimal valorMulta = contaDto.ValorOriginal * percentualMulta;
            decimal valorJuros = contaDto.ValorOriginal * percentualJurosAoDia;

            valorCorrigido = contaDto.ValorOriginal + valorMulta + valorJuros;
        }

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
}

