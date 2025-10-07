using System;
using System.ComponentModel.DataAnnotations;
public class Contas
{
    public int Id { get; set; }
    [Required]
    public string? Nome { get; set; }
    [Required]
    public decimal ValorOriginal { get; set; }
    [Required]
    public DateTime DataDeVencimento { get; set; }
    [Required]
    public DateTime DataDePagamento { get; set; }
    public decimal ValorCorrigido { get; set; }
    public int DiasEmAtraso { get; set; }
    public decimal MultaAplicadaPercentual { get; set; }
    public decimal JurosAoDiaAplicadoPercentual { get; set; }
    
}
