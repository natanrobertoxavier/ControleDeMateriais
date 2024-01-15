namespace ControleDeMateriais.Communication.Responses;
public class ResponseBorrowedMaterialJson
{
    public string BarCode { get; set; }
    public string MaterialName { get; set; }
    public string MaterialDescription { get; set; }
    public string CategoryName { get; set; }
    public string HashId { get; set; }
    public string CollaboratorEnrollment { get; set; }
    public string CollaboratorNickName { get; set; }
    public string CollaboratorTelephone { get; set; }
    public string UserReceivedName { get; set; }
    public DateTime DateReceived { get; set; }
}
