public interface IContaAPagarService
{
    Task<Contas> CadastrarConta(CadastrarContaAPagarDto contaDto);
    
}