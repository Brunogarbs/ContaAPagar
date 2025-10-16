public interface IContaAPagarService
{
    Task<Contas> CadastrarConta(CadastrarContaAPagarDto contaDto);
    Task<IEnumerable<Contas>> ListarContas();
    Task<Contas?> ListarContasId(int id);
    Task<Contas?> AtualizarContas(int id, CadastrarContaAPagarDto contaDto);
    Task<Contas?> ExcluirConta(int id);
}