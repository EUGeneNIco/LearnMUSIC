
using LearnMUSIC.Core.Application.DbUpdaters.Command.RunDbUpdate;
using MediatR;

namespace LearnMusic.Interface.WebApi.HostedService
{
  public class DbUpdateService : BackgroundService, IDisposable
  {
    private readonly IServiceScopeFactory serviceScopeFactory;

    public DbUpdateService(IServiceScopeFactory serviceScopeFactory)
    {
      this.serviceScopeFactory = serviceScopeFactory;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
      Task.Run(() => DoWork());

      return Task.CompletedTask;
    }
    public override Task StopAsync(CancellationToken cancellationToken)
    {
      return base.StopAsync(cancellationToken);
    }
    private async void DoWork()
    {
      try
      {
        using var scope = this.serviceScopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        await mediator.Send(new RunDbUpdateCommand());

      }
      catch(Exception ex)
      {
        return;
      }
    }
  }
}
