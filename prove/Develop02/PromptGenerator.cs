/* Travis Scoville (c) 2024
 * Journal program main
 */

class PromptGenerator
{
    public List<string> _prompts = [
        "Did you see anything pretty today?",
        "How are you feeling?",
        "What are you looking forward to?",
        "What are you not looking forward to?",
        "What are you grateful for recently?",
        "Who was the most interesting person I interacted with today?",
        "What was the best part of the day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?",
    ];

    public Random _rng;

    public string PickPrompt()
    {
        int choice = _rng.Next(_prompts.Count);
        return _prompts[choice];
    }
}