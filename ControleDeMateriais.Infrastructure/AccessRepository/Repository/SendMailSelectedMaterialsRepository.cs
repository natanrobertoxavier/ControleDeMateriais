using ControleDeMateriais.Communication.Requests;
using ControleDeMateriais.Communication.Responses;
using ControleDeMateriais.Domain.Entities;
using ControleDeMateriais.Domain.Repositories.Email;
using ControleDeMateriais.Domain.Repositories.Material;

namespace ControleDeMateriais.Infrastructure.AccessRepository.Repository;
public class SendMailSelectedMaterials : ISelectedMaterialsSendMailOnlyRepository
{
    private readonly IEmailSendOnlyRepository _emailSendOnlyRepository;

    public SendMailSelectedMaterials(
        IEmailSendOnlyRepository emailSendOnlyRepository)
    {
        _emailSendOnlyRepository = emailSendOnlyRepository;
    }

    public async Task SendMail(
        RequestSelectedMaterialsContentEmailJson selectedMaterials,
        ResponseCollaboratorJson collaborator)
    {
        var content = InsertContent(selectedMaterials, collaborator);
        var subject = "Lista de materiais tomados de empréstimo";
        var recipient = collaborator.Email;

        await _emailSendOnlyRepository.SendMail(content, subject, recipient);
    }

    private static string InsertContent(RequestSelectedMaterialsContentEmailJson selectedMaterials, ResponseCollaboratorJson collaborator)
    {
        var tableContent = SelectedMaterials(selectedMaterials);

        var htmlResult = $@"    
        <head>
            <meta charset=""UTF-8"">
            <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
            <title>Tabela de Materiais</title>
            <style>
	            * {{
                    font: 400 18px Roboto, sans-serif;
                    margin-top: 20px;
	                margin-left: 20px;
                    color: black;
	            }}
                table {{
                    border-collapse: collapse;
                    width: 100%;
                    margin-top: 20px;
                }}
                th, td {{
                    border: 1px solid #dddddd;
                    text-align: left;
                    padding: 8px;
                }}
                th {{
                    background-color: #f2f2f2;
                }}
		        strong {{
                    font-weight: bold;
                }}
            </style>
        </head>
        <body>
	        Olá <strong>{collaborator.Nickname}</strong>, os materiais abaixo foram cadastrados como cedidos para você por <strong>{selectedMaterials.UserName}</strong>.
        	<table>
                <thead>
                    <tr>
                        <th>Código de Barras</th>
                        <th>Nome</th>
                        <th>Descrição</th>
                    </tr>
                </thead>
                <tbody>
                    {tableContent}
                </tbody>
            </table>
	        <br>
	       <spam>Confirme com a senha para validar a concessão.</spam>
        </body>";

        return htmlResult;
    }

    private static object SelectedMaterials(RequestSelectedMaterialsContentEmailJson selectedMaterials)
    {
        var result = selectedMaterials.BarCode
            .Zip(selectedMaterials.MaterialName, (barcode, name) =>
                (barcode, name))
            .Zip(selectedMaterials.MaterialDescription, ((string barcode, string name) tuple, string description) =>
                $@"
                <tr>
                    <td>{tuple.barcode}</td>
                    <td>{tuple.name}</td>
                    <td>{description}</td>
                </tr>")
            .Aggregate((current, next) => current + next);

        return result;

    }
}
