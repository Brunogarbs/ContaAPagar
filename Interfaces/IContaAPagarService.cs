public interface IContaAPagarService
{
    Task<Contas> CadastrarConta(CadastrarContaAPagarDto contaDto);

    Task<IEnumerable<Contas>> ListarContas();
}