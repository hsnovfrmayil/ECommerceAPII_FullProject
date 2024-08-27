using System;
using Microsoft.Extensions.Configuration;

namespace ECommerceAPI.Persistence;

static class Configuration
{
    static public string ConnectionString()
    {
        //get
        //{
        ConfigurationManager configurationManager = new();
        configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/ECommerceAPII.API"));
        configurationManager.AddJsonFile("appsettings.json");

        return configurationManager.GetConnectionString("PostgreSQL");
        //}
    }
}