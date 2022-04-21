namespace Portfolio.WebApi.Models;

public class EmailConfig
{
    public string From { get; set; }
    public string To { get; set; }
    public string SmtpPassword { get; set; }
    public string SmtpAddress { get; set; }
    public int SmtpPort { get; set; }
}