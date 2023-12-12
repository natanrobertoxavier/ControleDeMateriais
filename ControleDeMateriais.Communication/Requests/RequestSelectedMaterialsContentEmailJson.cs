namespace ControleDeMateriais.Communication.Requests;
public class RequestSelectedMaterialsContentEmailJson
{
    public string UserName { get; set; }
    public List<string> BarCode {  get; set; }
    public List<string> MaterialName {  get; set; }
    public List<string> MaterialDescription { get; set; }
}
