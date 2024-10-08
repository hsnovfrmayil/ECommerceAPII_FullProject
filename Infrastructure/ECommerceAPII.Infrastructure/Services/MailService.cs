﻿using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using ECommerceAPII.Application.Abstractions.Services;
using Microsoft.Extensions.Configuration;

namespace ECommerceAPII.Infrastructure.Services;

public class MailService : IMailService
{
    readonly IConfiguration _configuration;

    public MailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true)
    {

        await SendMailAsync(new[] { to},subject,body,isBodyHtml);
    }

    public async Task SendMailAsync(string[] tos, string subject, string body, bool isBodyHtml = true)
    {
        MailMessage mail = new();
        mail.IsBodyHtml = isBodyHtml;
        foreach(var to in tos) 
            mail.To.Add(to);
        mail.Subject = subject;
        mail.Body = body;
        mail.From = new(_configuration["Mail:Username"], "BraNDT Holding",System.Text.Encoding.UTF8);

        SmtpClient smtp = new();
        smtp.Credentials=new NetworkCredential(_configuration["Mail:Username"], _configuration["Mail:Password"]);
        smtp.Port = 587;
        smtp.EnableSsl = true;
        smtp.Host = _configuration["Mail:Host"];
        await smtp.SendMailAsync(mail);
    }

    public async Task SendPasswordResetMailAsync(string to,string userId,string resetToken)
    {
        StringBuilder mail = new();
        mail.AppendLine("Salam<br>Passwordu yenilemek ucun asagidaki linki ziyaret edin...<br><strong><a target=\"_blank\" href=\"" );
        mail.AppendLine(_configuration["FrontUrl"]);
        mail.AppendLine("/update-password/");
        mail.AppendLine(userId);
        mail.AppendLine("/");
        mail.AppendLine(resetToken);
        mail.AppendLine("\">Yeni sifre ucun bura klik edin</a></strong><br><br><span style=\"font-size:12px;\">eger istemirsense bunu ciddiye alma agilli bala</span><br>Hormete<br><br><br>ECommerceAPII iii");

        await SendMailAsync(to,"Reset Password",mail.ToString());
    }
}

