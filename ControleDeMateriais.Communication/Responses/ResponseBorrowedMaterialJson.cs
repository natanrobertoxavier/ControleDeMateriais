namespace ControleDeMateriais.Communication.Responses;
public class ResponseBorrowedMaterialJson
{
    public string CollaboratorEnrollment { get; set; }
    public string CollaboratorNickname { get; set; }
    public string CollaboratorTelephone { get; set; }
    public string UserNameRegisterLoan { get; set; }
    public DateTime LoanDateTime { get; set; }
    public bool LoanConfirmed { get; set; }
    public string ColaboratorNicknameConfirmed { get; set; }
    public List<ResponseBorrowedListMaterialJson> ListMaterialBorrowed { get; set; }
}
