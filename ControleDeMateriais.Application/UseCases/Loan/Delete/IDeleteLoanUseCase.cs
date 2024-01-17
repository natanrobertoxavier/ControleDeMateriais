namespace ControleDeMateriais.Application.UseCases.Loan.Delete;
public interface IDeleteLoanUseCase
{
    Task Execute(string hashId);
}
