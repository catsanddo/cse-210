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
		_hiddenWords = [];

		foreach (string word in text.Split(" "))
		{
			_words.Add(new Word(word));
		}
	}

	public void Display()
	{
		_reference.Display();
		for (int i = 0; i < _words.Count(); i++)
		{
			Console.Write($"{_words[i].Render(IsHidden(i))} ");
		}
		Console.WriteLine();
	}

	public void HideWords()
	{
		int hiddenWords = 0;

		while (hiddenWords < 3 && !IsAllHidden())
		{
			int index = _random.Next(_words.Count());
			if (!IsHidden(index))
			{
				_hiddenWords.Add(index);
				hiddenWords += 1;
			}
		}
	}

	public void UnhideWords()
	{
		for (int i = 0; i < 3 && _hiddenWords.Count() > 0; i++) {
			_hiddenWords.RemoveAt(_hiddenWords.Count()-1);
		}
	}

	public bool IsAllHidden()
	{
		return _words.Count() == _hiddenWords.Count();
	}

	private bool IsHidden(int wordIndex)
	{
		return _hiddenWords.Contains(wordIndex);
	}
}
