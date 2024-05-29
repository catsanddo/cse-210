class Word
{
	private string _word;
	private bool _isHidden;

	public Word(string text)
	{
		_word = text;
		_isHidden = false;
	}

	public void Hide()
	{
		_isHidden = true;
	}

	public bool IsHidden()
	{
		return _isHidden;
	}

	public string Render()
	{
		string result = "";

		if (!_isHidden)
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