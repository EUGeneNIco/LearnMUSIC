
using LearnMUSIC.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace AFPMBAI.CLAIMS.DbUpdate
{
  public class DbUpdater
  {
    private readonly ConsoleIO _consoleIO;

    public DbUpdater()
    {
      _consoleIO = new ConsoleIO();
    }

    private string GetConnectionString()
    {
      var configBuilder = new ConfigurationBuilder()
          .AddJsonFile("appsettings.json", false, true)
          .Build();

      var connectionString = configBuilder.GetConnectionString("LearnMUSICDbConnectionString");

      return connectionString;
    }

    public void Start()
    {
      var connectionString = this.GetConnectionString();

      var response = _consoleIO.Confirm($"This tool will attempt to update the REAMS database using this connection configuration:\n{connectionString}\n\nProceed? [Y/N] ");
      if (response == ConsoleKey.Y)
      {
        Console.Write("\n\nStarted...");

        try
        {
          var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
          optionsBuilder.UseSqlServer(connectionString);

          using (var dbContext = new AppDbContext(optionsBuilder.Options))
          {
            dbContext.Database.Migrate();

            Console.WriteLine("\n\nUpdate finished.");

            response = _consoleIO.Confirm("\nWould you like to populate the database with seed data? [Y/N] ");
            if (response == ConsoleKey.Y)
            {
              Console.Write("\n\nSeeding...");

              var seeder = new Seeder(dbContext);
              seeder.Seed();

              Console.Write("\n\nSeed finished.");
            }

            Console.WriteLine();
          }
        }
        catch (Exception ex)
        {
          Console.WriteLine("\n\nError: " + ex.Message);
        }
      }
      else if (response == ConsoleKey.N)
      {
        //Console.Clear();
        Console.WriteLine();
      }

      Console.ReadKey();
    }
  }
}
