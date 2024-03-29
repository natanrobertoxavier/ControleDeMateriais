﻿using ControleDeMateriais.Domain.Entities;

namespace ControleDeMateriais.Domain.Repositories.Loan.Borrowed;
public interface IBorrowedMaterialReadOnly
{
    Task<List<BorrowedMaterial>> RecoverAll();
    Task<List<BorrowedMaterial>> RecoverByStatus(bool status);
    Task<List<BorrowedMaterial>> RecoverByStatusReceived(bool status, bool received);
    Task<List<BorrowedMaterial>> RecoverByHashId(string hashId);
    Task<List<BorrowedMaterial>> RecoverByBarCode(string barCode);
    Task<List<BorrowedMaterial>> RecoverByHashIdAndStatus(string hashId, bool status);
    Task<List<string>> RecoverBorrowedMaterial(List<string> codeBar);
}
