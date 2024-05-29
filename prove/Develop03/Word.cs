class Word
{
	private string _word;

	public Word(string text)
	{
		_word = text;
	}

	public string Render(bool isHidden)
	{
		string result = "";

		if (!isHidden)
		{
			return _word;
		}

		for (int i = 0; i < _word.Count(); i++)
		{
			result += "_";
		}

		return result;
	}
}