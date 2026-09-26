namespace PizzaZiegWebApp.Models;

public class AppConfig
{
    public Database Database { get; set; }
    public Company Company { get; set; }
}


public class Database
{
    public string ConnectionString { get; set; }
}

public class Company
{
    public string CompanyName { get; set; }
    public string CompanyPhone { get; set; }
    public string CompanyPhoneShort { get; set; }
    public string CompanyEmail { get; set; }
}

