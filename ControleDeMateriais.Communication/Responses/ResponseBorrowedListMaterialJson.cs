namespace ControleDeMateriais.Communication.Responses;
public class ResponseBorrowedListMaterialJson
{
    public string BarCode { get; set; }
    public string MaterialName { get; set; }
    public string MaterialDescription { get; set; }
    public string CategoryName { get; set; }
    public string UserReceivedName { get; set; }
    public DateTime DateReceived { get; set; }
}
