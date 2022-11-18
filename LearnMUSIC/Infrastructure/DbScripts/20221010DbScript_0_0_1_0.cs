
using LearnMusic.Core.Domain.Enumerations;
using LearnMUSIC.Common.Helper;
using LearnMUSIC.Core.Domain.Entities;
using LearnMUSIC.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFPMBAI.CLAIMS.DbUpdate.DbScripts
{
  internal class DbScript_0_0_1_0
  {
    private static AppDbContext _dbContext = null;

    private static List<CodeListValue> genres;
    private static List<CodeListValue> keys;
    private static List<Module> modules;

    internal static void Run(AppDbContext dbContext, CodeListValue lastDbVersionEntity)
    {
      var thisVersion = new Version("0.0.1.0");
      var lastDbVersion = new Version(lastDbVersionEntity.Name);

      if (lastDbVersion < thisVersion)
      {
        _dbContext = dbContext;

        keys = new List<CodeListValue>
        {
          new CodeListValue { Name = "C", Type = CodeListType.KeySignature },
          new CodeListValue { Name = "C#", Type = CodeListType.KeySignature },
          new CodeListValue { Name = "D", Type = CodeListType.KeySignature },
          new CodeListValue { Name = "Eb", Type = CodeListType.KeySignature },
          new CodeListValue { Name = "E", Type = CodeListType.KeySignature },
          new CodeListValue { Name = "F", Type = CodeListType.KeySignature },
          new CodeListValue { Name = "F#", Type = CodeListType.KeySignature },
          new CodeListValue { Name = "G", Type = CodeListType.KeySignature },
          new CodeListValue { Name = "G#", Type = CodeListType.KeySignature },
          new CodeListValue { Name = "A", Type = CodeListType.KeySignature },
          new CodeListValue { Name = "Bb", Type = CodeListType.KeySignature },

        };
        
        genres = new List<CodeListValue>
        {
          new CodeListValue { Name = "Pop", Type = CodeListType.Genre },
          new CodeListValue { Name = "Rock", Type = CodeListType.Genre },
          new CodeListValue { Name = "Jazz", Type = CodeListType.Genre },
          new CodeListValue { Name = "RNB", Type = CodeListType.Genre },
          new CodeListValue { Name = "Hip Hop", Type = CodeListType.Genre },
          new CodeListValue { Name = "Blues", Type = CodeListType.Genre },
          new CodeListValue { Name = "Soul", Type = CodeListType.Genre },
          new CodeListValue { Name = "K-POP", Type = CodeListType.Genre },
          new CodeListValue { Name = "Hard Rock", Type = CodeListType.Genre },
          new CodeListValue { Name = "Heavy Metal", Type = CodeListType.Genre },
          new CodeListValue { Name = "OPM", Type = CodeListType.Genre },
          new CodeListValue { Name = "Ballad", Type = CodeListType.Genre },
          new CodeListValue { Name = "Country Music", Type = CodeListType.Genre },
          new CodeListValue { Name = "Musical Theatre", Type = CodeListType.Genre },
          new CodeListValue { Name = "Fol Music", Type = CodeListType.Genre },
          new CodeListValue { Name = "Techno", Type = CodeListType.Genre },
        };

        
        modules = new List<Module>
        {
          new Module { Name = "SongSheet", Category = "Usual" },
          new Module { Name = "Feedback", Category = "Usual" },
          new Module { Name = "Home", Category = "Usual" },
          new Module { Name = "Post", Category = "Usual" },
          new Module { Name = "Profile", Category = "Usual" },
          new Module { Name = "Introduction", Category = "Usual" },

          new Module { Name = "Admin", Category = "Management" },
        };

        try
        {
          SeedUsers();

          SeedGenres();

          SeedKeys();

          SeedModules();

          SeedAdminAccess();

          lastDbVersionEntity.Name = thisVersion.ToString();

          _dbContext.SaveChanges();
        }
        catch (Exception ex)
        {
          Console.Write($"\n\nSeeding Error: {ex.InnerException.Message}");
        }
      }
    }

    private static void SeedUsers()
    {
      if (!_dbContext.Users.Any())
      {
        _dbContext.Users.Add(new User
        {
          UserName = "eugene",
          Email = "test@test.com",
          PasswordHash = PasswordHelper.Hash("pogi"),
          FirstName = "Eugene Nico",
          LastName = "Hermano",
          AccountStatus = true,
          CreatedOn = DateTime.Now
        });

        _dbContext.SaveChanges();
      }
    }

    private static void SeedGenres()
    {
      if (!_dbContext.CodeListValues.Any(p => p.Type == CodeListType.Genre))
      {
        foreach (var item in genres)
        {
          _dbContext.CodeListValues.Add(item);
        }

        _dbContext.SaveChanges();
      }
    }

    private static void SeedKeys()
    {
      if (!_dbContext.CodeListValues.Any(p => p.Type == CodeListType.KeySignature))
      {
        foreach (var item in keys)
        {
          _dbContext.CodeListValues.Add(item);
        }

        _dbContext.SaveChanges();
      }
    }

    private static void SeedAdminAccess()
    {
      var admin = _dbContext.Users
          .Include(p => p.ModuleAccesses)
              .ThenInclude(p => p.Module)
          .First
          (x => x.UserName == "eugene");


      var modules = _dbContext.Modules.ToList();
      modules.ForEach(module =>
      {
        if (!admin.ModuleAccesses.Any(p => p.Module != null && p.Module.Name == module.Name))
        {
          admin.ModuleAccesses.Add(new UserModuleAccess
          {
            ModuleId = module.Id,
            HasAccess = true
          });
        }
      });

      _dbContext.SaveChanges();
    }

    private static void SeedModules()
    {
      var dbModules = _dbContext.Modules.ToList();

      foreach (var item in modules)
      {
        if (!dbModules.Any(p => p.Name == item.Name))
        {
          _dbContext.Modules.Add(new Module
          {
            Name = item.Name,
            Category = item.Category
          });
        }
      }

      _dbContext.SaveChanges();

    }
  }
}
