class ReflectionActivity : Activity
{
    private string[] _prompts = [
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless.",
    ];
    private string[] _questions = [
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times when you were not as successful?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?",
    ];
    private int _nextQuestion = -1;

    public ReflectionActivity() : base("Reflection Activity",
    "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.") {}

    public void PerformActivity()
    {
        base.StartActivity();
        Console.WriteLine("Prompt:");
        Console.WriteLine($"\t{PickItem(_prompts)}");
        Console.WriteLine("\nThe activity will begin in a moment.");
        base.Wait(15);

        base.StartTimer();
		while (!base.IsTimerExpired())
		{
            Console.WriteLine($"{GetQuestion()} ");
            base.Wait(5);
		}

        Console.WriteLine();
		base.EndActivity();
    }

    public string GetQuestion()
    {
        if (_nextQuestion < 0)
        {
            _rand.Shuffle(_questions);
            _nextQuestion = 0;
        }

        string result = _questions[_nextQuestion];
        _nextQuestion += 1;
        if (_nextQuestion >= _questions.Count())
        {
            _nextQuestion = -1;
        }

        return result;
    }
}