using LearnMUSIC.Core.Application.Infrastructure;
using LearnMUSIC.Core.Application._Infrastructure.AutoMapper;
using LearnMUSIC.Core.Application._Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using AFPMBAI.CLAIMS.WebAPI.Auth;
using LearnMUSIC.Interface.WebAPI.Auth;
using LearnMUSIC.Infrastructure;
using LearnMUSIC.Infrastructure.Persistence;
using LearnMUSIC.Common.Common;
using LearnMusic.Interface.WebApi.HostedService;

namespace LearnMUSIC
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
      this.JWT_KEY = Configuration["JWT:Key"].ToString();
    }

    public IConfiguration Configuration { get; }
    private readonly string JWT_KEY;

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddControllers();
      services.AddCors();

      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
        options => Configuration.Bind("JwtSettings", options))
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
        options => Configuration.Bind("CookieSettings", options));

      services.AddEndpointsApiExplorer();
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPIv5", Version = "v1" });
      });
      services.AddDbContext<AppDbContext>(options =>
          options.UseSqlServer(Configuration.GetConnectionString("LearnMUSICDbConnectionString"))
                  .UseLazyLoadingProxies());

      services.AddHostedService<DbUpdateService>();

      // Configure DI
      this.ConfigureDI(services);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPIv5 v1"));
      }

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseCors(policy => policy.AllowAnyHeader()
                                  .AllowAnyMethod()
                                  .WithOrigins("http://localhost:4200", "http://192.168.100.2:4200"));

      app.UseAuthentication();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }

    private void ConfigureDI(IServiceCollection services)
    {
      //DbContext
      //services.AddScoped(typeof(IAppDbContext), typeof(AppDbContext));
      services.AddScoped(typeof(IAppDbContext), typeof(AppDbContext));

      services.AddMediatR(typeof(Startup).GetTypeInfo());

      // AutoMapper
      services.AddAutoMapper(typeof(AutoMapperProfile));

      // Fluent Validation
      services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

      // Add Framework Services
      services.AddTransient<IDateTime, MachineDateTime>();

      // JwtAuthenticationManager
      services.AddScoped<IJwtAuthenticationManager>(manager => new JwtAuthenticationManager(this.JWT_KEY, manager.GetService<IMediator>()));
    }
  }
}
