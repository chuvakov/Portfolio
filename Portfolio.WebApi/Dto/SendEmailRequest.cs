namespace Portfolio.WebApi.Dto;

public class SendEmailRequest
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Title { get; set; }
    public string Message { get; set; }
}