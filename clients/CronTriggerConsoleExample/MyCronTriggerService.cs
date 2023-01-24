using CronTrigger;

public class MyCronTriggerService : TriggerService
{
	public MyCronTriggerService()
		: base("0/10 * * * * ?")
	{
	}

	protected override Task DoWork()
	{
		Console.WriteLine($"{DateTime.Now} - Doing work...");

		return Task.CompletedTask;
	}
}