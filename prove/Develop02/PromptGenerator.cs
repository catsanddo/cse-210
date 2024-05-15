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
    ];

    public Random _rng;

    public string PickPrompt()
    {
        int choice = _rng.Next(_prompts.Count);
        return _prompts[choice];
    }
}