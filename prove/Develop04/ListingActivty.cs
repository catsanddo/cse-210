class ListingActivity : Activity
{
	private string[] _prompts = [
		"Who are people you appreciate?",
		"What are personal strengths of yours?",
		"Who are people that you have helped this week?",
		"When have you felt the Holy Ghost this month?",
		"Who are some of your personal heroes?",
	];
	private int _responses = 0;

	public ListingActivity() : base("Listing Activity",
	"This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.") {}

	public void PerformActivity()
	{
		base.StartActivity();
		Console.WriteLine("\nPrompt:");
		Console.WriteLine($"\t{PickItem(_prompts)}");
		Console.WriteLine("\nThe activity will begin in a moment.");
		base.Wait(15);

		base.StartTimer();
		while (!base.IsTimerExpired())
		{
			Console.Write("> ");
			Console.ReadLine();
			_responses += 1;
		}

		Console.WriteLine();
		Console.WriteLine($"You recorded {_responses} responses.");
		base.EndActivity();
	}
}
