namespace CronTrigger
{
	using System;
	using System.Threading;
	using System.Threading.Tasks;
	using Microsoft.Extensions.Hosting;
	using Quartz;

	public abstract class TriggerService : BackgroundService
	{
		protected CronExpression _cron;

		public TriggerService(string? cronExpression)
		{
			if (cronExpression is null)
			{
				throw new ArgumentNullException(nameof(cronExpression));
			}

			_cron = new CronExpression(cronExpression);
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			var nextrun = _cron.GetNextValidTimeAfter(DateTime.Now);
			while (!stoppingToken.IsCancellationRequested)
			{
				var now = DateTime.Now;
				if (now > nextrun)
				{
					await DoWork();
					nextrun = _cron.GetNextValidTimeAfter(now);
				}

				var wait = (now - nextrun.Value).Duration();
				await Task.Delay(wait);
			}
		}

		protected abstract Task DoWork();
	}
}
