
using AFPMBAI.CLAIMS.DbUpdate.DbScripts;
using LearnMUSIC.Core.Domain.Entities;
using LearnMUSIC.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AFPMBAI.CLAIMS.DbUpdate
{
  public class DbScriptManager
  {
    private CodeListValue lastDbVersionEntity;
    private readonly AppDbContext context;

    public DbScriptManager(AppDbContext context)
    {
      this.context = context;
      lastDbVersionEntity = this.GetLastDbVersion();
    }

    public void RunDbScripts()
    {
      DbScript_0_0_1_0.Run(context, lastDbVersionEntity);
    }

    private CodeListValue GetLastDbVersion()
    {
      var lastDbVersion =
          context.CodeListValues.SingleOrDefault(p => p.Type == "DB Version");

      if (lastDbVersion == null)
      {
        lastDbVersion = new CodeListValue
        {
          Name = "0.0.0.0",
          Type = "DB Version",
        };

        context.CodeListValues.Add(lastDbVersion);
        context.SaveChanges();
      }

      return lastDbVersion;
    }
  }
}
