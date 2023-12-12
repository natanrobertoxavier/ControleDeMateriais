using ControleDeMateriais.Domain.Entities;
using ControleDeMateriais.Domain.Repositories.Email;
using ControleDeMateriais.Domain.Repositories.User.ForgotPassword.Forgot;

namespace ControleDeMateriais.Infrastructure.AccessRepository.Repository;
public class ForgotPasswordRepository : IForgotPasswordSendMailOnlyRepository
{
    private readonly IEmailSendOnlyRepository _emailSendOnlyRepository;

    public ForgotPasswordRepository(
        IEmailSendOnlyRepository emailSendOnlyRepository)
    {
        _emailSendOnlyRepository = emailSendOnlyRepository;
    }

    public async Task SendMailRecoveryCode(User user, string recoveryCode)
    {
        var content = InsertContentRecoveryCode(user, recoveryCode);
        var subject = "Código para recuperação de senha";
        var recipient = user.Email;

        await _emailSendOnlyRepository.SendMail(content, subject, recipient);
    }

    private static string InsertContentRecoveryCode(User user, string recoveryCode)
    {
        var content = $@"<div class="""">
							<div class=""aHl"">
							</div>
							<div id="":181"" tabindex=""-1"">
							</div>
							<div id="":18b"" class=""ii gt"" jslog=""20277; u014N:xr6bB; 1:WyIjdGhyZWFkLWY6MTc4NDcxNDQ5Nzk0Mzc3MDI4MyJd; 4:WyIjbXNnLWY6MTc4NDcxNDQ5Nzk0Mzc3MDI4MyJd"">
								<div id="":18c"" class=""a3s aiL msg979101199036221872"">
									<u></u>       
									<div marginheight=""0"" marginwidth=""0"" style=""margin:0px;background-color:#f2f3f8""> 
										<table cellspacing=""0"" border=""0"" cellpadding=""0"" width=""100%"" bgcolor=""#f2f3f8"" style=""font-family:sans-serif""> 
											<tbody>
												<tr> 
													<td> 
														<table style=""background-color:#f2f3f8;max-width:670px;margin:0 auto"" width=""100%"" border=""0"" align=""center"" cellpadding=""0"" cellspacing=""0""> 
															<tbody>
																<tr>
																	<td style=""height:80px"">&nbsp;</td>
																</tr>
																<tr> 
																	<td style=""text-align:center""> 
																		<a href=""https://controle-de-materiais.onrender.com/"" title=""logo"" target=""_blank"" > 
																			<u></u> 
																			<u></u> 
																			<u></u> 
																		</a> 
																	</td>
																</tr>
																<tr> 
																	<td style=""height:20px"">&nbsp;</td>
																</tr>
																<tr> 
																	<td> 
																		<table width=""95%"" border=""0"" align=""center"" cellpadding=""0"" cellspacing=""0"" style=""max-width:670px;background:#fff;border-radius:3px;text-align:center""> 
																			<tbody>
																				<tr> 
																					<td style=""height:40px"">&nbsp;</td>
																				</tr>
																				<tr> 
																					<td style=""padding:0 35px""> 
																						<h1 style=""color:#071c21;font-weight:700;margin:0;font-size:24px;line-height:28px"">Olá {user.Name}, seu token para recuperação de senha chegou! </h1> 
																						<span style=""display:inline-block;vertical-align:middle;margin:29px 0 26px;border-bottom:1px solid #707070;width:148px""></span> 
																						<p style=""color:#071c21;font-size:14px;line-height:20px;margin:0""> Para criar uma nova senha basta informar o código abaixo</p>
																						<p style=""font-weight:700;color:#071c21;font-size:36px"">{recoveryCode}</p>
																						<p style=""color:#071c21;font-size:14px;line-height:61px;margin:0""> Observe que ele vence em 30 minutos. </p>
																						<a href=""https://controle-de-materiais.onrender.com/user/newpassword"" style=""background:#166889;text-decoration:none!important;font-weight:400;margin-top:23px;color:#ffffff;font-size:24px;padding:10px 36px;display:inline-block"" rel=""noopener noreferrer"" target=""_blank"" >Acessar o Portal</a> 
																					</td>
																				</tr>
																				<tr> 
																					<td style=""height:40px"">&nbsp;</td>
																				</tr>
																			</tbody>
																		</table> 
																	</td>
																</tr>
																<tr> 
																	<td style=""height:20px"">&nbsp;</td>
																</tr>
																<tr> 
																	<td style=""text-align:center""> 
																		<p style=""font-size:14px;color:rgba(69,80,86,0.7411764705882353);line-height:18px;margin:0 0 0""> 
																			<a href=""https://controle-de-materiais.onrender.com/"" rel=""noopener noreferrer"" target=""_blank"">
																				<strong>controle-de-materiais.onrender.com</strong>
																			</a> 
																		</p>
																	</td>
																</tr>
																<tr> 
																	<td style=""height:80px"">&nbsp;</td>
																</tr>
															</tbody>
														</table> 
													</td>
												</tr>
											</tbody>
										</table> 
										<img src=""https://ci3.googleusercontent.com/meips/ADKq_NbjeJG3z7fQaPgq_UOOy-fAqipAAMlO4ud-IuelbwWyE0pSt6wN7zgv59mbB-32KvRkuZw76PQKLdXZcugsULGcMb5IzGYuj_bV8xdJF8Qtesatn5sdkv1NbChQTnEe6fCedU9plt2oSoRa49-U2X2McWeWUxCgWfsbKv6aaejRy6oQ5Cg83Ru4KrAyqtGp2LC04QmCpY85hcPqVDhLabg7TwKu7wiTRC6Le5Bgv-oYKgl5YhJduvmFs55Y8pg9M640tB-eCzReTr5T9Um_8aVEO7HRGWGfHP0mpdKGP5YwZxIzS38mX0qjjw9V3Ztc3c5GHWEqVLkX_43PmQatke_P_c5ouyynVV5KITwnfH5--4FkhjjLfEgFx0NF6w8ZkEf99TZ3VgiZ5V15A9cyDhtT19SMINOO3KHHobhXlWyIdfxg5w=s0-d-e1-ft#https://u2145994.ct.sendgrid.net/wf/open?upn=ZHJ-2BglCeAbVlE82QUaYjvL4g-2FDS1g6sabVURZLVPFNN69qRvUOyAiaZmL1ayjKQzt-2BpNStAlyxMtGsXBBBYz7ECEpE7GwIuYdKt1B-2FDrofX42V6NbBWT1GTwy1HDXp6T6IBDI-2FYqYLtRW-2BT9jKt-2BQ-2FtPKVrkP2GEDr4thd6rjjohtS1E5m8xhRjPnUDYn4YPdHGMDfv-2FiITyVINQ9TsNFIbwdsNh9RaGY9HuC5Ac458-3D"" alt="""" width=""1"" height=""1"" border=""0"" style=""height:1px!important;width:1px!important;border-width:0!important;margin-top:0!important;margin-bottom:0!important;margin-right:0!important;margin-left:0!important;padding-top:0!important;padding-bottom:0!important;padding-right:0!important;padding-left:0!important"" class=""CToWUd"" data-bit=""iit"">
									</div>
								</div>
							</div>
						</div>";

		return content;
       }
}
