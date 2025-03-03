using System.Text.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using ConsoleAPI1;
using System.Runtime.InteropServices.JavaScript;

namespace AzureFTimer;

//--- file: DailyPickupFunction.cs -----------
public class DailyPickupFunction
{
    private readonly ILogger<DailyPickupFunction> _logger;

    public DailyPickupFunction(ILogger<DailyPickupFunction> logger)
    {
        _logger = logger;
    }

    [Function("pickupfun8765")]
    public async Task Run([TimerTrigger("0 0 15 * * *")] TimerInfo timer) //"0 */1 * * * *" every 1 min
    {
        //TimerTrigger("0 0 15 * * *") - Every day at exactly 3:00 PM the action starts.
        _logger.LogInformation($"Function started at: {DateTime.UtcNow}");

      #region --- Main action in Azure Function --

        CShipmentsDhl cShipments = new ();

        await cShipments.Run();

        string sSubject = "Automatic EasyPost Mail from Azure";
        string sHtmlBody = cShipments.GetMessageToPost();
        sHtmlBody += cShipments._htmlTable;

        EmailService cEmail = new();
        cEmail.InitMyMailSender();
        await cEmail.SendEmailTestAsync(sHtmlBody, sSubject);

        _logger.LogInformation($"e-mail subiect: {cEmail._Subject}"); //{cEmail._HtmlBody}
        _logger.LogInformation($"Shipments service: {cShipments._htmlMessage}");

      #endregion -----

    }
}
