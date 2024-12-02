namespace ReplicantDemo.Core.Configuration;

public class ExternalApiSettings
{
    public string BaseUrl { get; set; } = "https://api.example.com";
    public int Timeout { get; set; } = 30;
}