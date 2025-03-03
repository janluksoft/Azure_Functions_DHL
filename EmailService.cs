using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices.JavaScript;


namespace ConsoleAPI1;

// Use test this class:
// EmailService cEmail = new();
// cEmail.InitMyMailSender();
// await cEmail.SendEmailTestAsync();

//--- file: EmailService.cs -----------

class EmailService
{
    public string email_sender { set; get; }
    string email_pass { set; get; }
    string host { set; get; }
    int port { set; get; }

    public string email_recipient { set; get; } //destination: cel, miejsce przeznaczenia

    public string _HtmlBody { set; get; }
    public string _Subject { set; get; }

    SecretData cSecret;


    public EmailService(string xemail_sender, string xemail_pass, string xhost, int xport)
    {
        cSecret = new();
        InitMailSender(xemail_sender, xemail_pass, xhost, xport);
    }

    public EmailService()
    {
        cSecret = new();
    }

    private void InitMailSender(string email_sender, string email_pass, string host, int port)
    {
        this.email_sender = email_sender;
        this.email_pass = email_pass;
        this.host = host;
        this.port = port;

        this.email_recipient = "";
        this._HtmlBody = "";
        this._Subject = "";
    }

    public void InitMyMailSender()
    {
        InitMailSender(
            cSecret.email_sender,
            cSecret.email_pass,
            cSecret.host,
            cSecret.port
        );
    }

    public async Task SendEmailTestAsync(string xHtmlBody = "", string xSubject = "")
    {
        string sMailRecipient = cSecret.email_recipient;

        string sSubject = "Test NET";
        if (xSubject != "")
            sSubject = xSubject;

        string sHtmlBody = "It is <b>only Test</b> e-mail from Azure Function<br>" +
            "This e-mail was sent at: <b>" +DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + "</b>";
        if (xHtmlBody != "")
            sHtmlBody = xHtmlBody;

        this._Subject = sSubject;
        this._HtmlBody = sHtmlBody;

        var tResult = await SendEmailAsync(sMailRecipient, sSubject, sHtmlBody);
    }

    /// <summary>
    /// The function sends e-mail
    /// </summary>
    /// <param name="xMailRecipient"></param>
    /// <param name="xSubject"></param>
    /// <param name="xHtmlBody"></param>
    /// <returns>Tupples((or success), "sending information")</returns>
    public async Task<(bool bOk, string message)> SendEmailAsync(string xMailRecipient, string xSubject, string xHtmlBody)
    {
        string messEnd = "e-mail has been sent.";
        bool bEnd = false;

        var message = new MailMessage();
        message.From = new MailAddress(email_sender);  //("your@email.com");

        message.To.Add(xMailRecipient); 
        message.Subject = xSubject;     
        message.Body = xHtmlBody;
        message.IsBodyHtml = true;

        using var smtp = new SmtpClient(host, port)
        {   
            Credentials = new NetworkCredential(email_sender, email_pass),
            EnableSsl = true
        };

        try
        {
            //smtp.Send(message);
            await smtp.SendMailAsync(message);
            bEnd = true;
        }
        catch (Exception e)
        {
            messEnd = e.Message;
            bEnd = false;
        }

        int i = 0;
        return (bEnd, messEnd);
    }
}
