namespace ControleDeMateriais.Communication.Requests;
public class RequestNewPasswordJson
{
    public string Email { get; set; }
    public string RecoveryCode { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmPassword { get; set; }
}
