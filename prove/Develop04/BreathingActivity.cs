using System.Threading;

class BreathingActivity : Activity
{
	public BreathingActivity() : base("Breathing Activty",
	"This activity will help you relax by walking your through breathing in and out slowly. Clear your mind and focus on your breathing.") {}

	public void PerformActivity()
	{
		base.StartActivity();
		Console.WriteLine("The activity will begin in a moment.");
		base.Wait(5);

		base.StartTimer();
		while (!base.IsTimerExpired())
		{
			Console.Write("Breathe in... ");
			CountDown(6);
			Console.WriteLine();
			Console.Write("Breathe out... ");
			CountDown(4);
			Console.WriteLine();
		}

		Console.WriteLine();
		base.EndActivity();
	}

	private void CountDown(int duration)
	{
		while (duration > 0)
		{
			Console.Write($"{duration}");
			Thread.Sleep(1000);
			Console.Write("\b");
			duration -= 1;
		}
		Console.Write(" ");
	}
}
