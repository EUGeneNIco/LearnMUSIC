
using LearnMUSIC.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AFPMBAI.CLAIMS.DbUpdate
{
  public class Seeder
  {
    private readonly AppDbContext _dbContext;

    public Seeder(AppDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public void Seed()
    {
      var scriptManager = new DbScriptManager(_dbContext);
      scriptManager.RunDbScripts();
    }
  }
}
