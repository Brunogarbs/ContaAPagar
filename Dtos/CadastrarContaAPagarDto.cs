using System.ComponentModel.DataAnnotations;

public class CadastrarContaAPagarDto
{
    [Required(ErrorMessage = "O campo nome precisa ser informado!")]
    public string? Nome { get; set; }
    [Required(ErrorMessage = "O campo Valor Original precisa ser informado!")]
    public decimal ValorOriginal { get; set; }
    [Required(ErrorMessage = "O compo Data de Vencimento precisa ser informado!")]
    public DateTime DataDeVencimento { get; set; }
    [Required(ErrorMessage = "O compo Data de Pagamento precisa ser informado!")]
    public DateTime DataDePagamento { get; set; }

}