class Scripture
{
	private List<Word> _words;
	private List<int> _hiddenWords;
	private Reference _reference;
	private Random _random;

	public Scripture(Reference reference, string text)
	{
		_reference = reference;
		_random = new();
		_words = [];

		foreach (string word in text.Split(" "))
		{
			_words.Add(new Word(word));
		}
	}

	public void Display()
	{
		_reference.Display();
		foreach (Word word in _words)
		{
			Console.Write($"{word.Render()} ");
		}
		Console.WriteLine();
	}

	public void HideWords()
	{
		int hiddenWords = 0;

		while (hiddenWords < 3 && !IsAllHidden())
		{
			int index = _random.Next(_words.Count());
			if (!_words[index].IsHidden())
			{
				_words[index].Hide();
				hiddenWords += 1;
			}
		}
	}

	public void UnhideWords()
	{

	}

	public bool IsAllHidden()
	{
		bool result = true;

		foreach (Word word in _words)
		{
			result = result && word.IsHidden();
		}

		return result;
	}
}
