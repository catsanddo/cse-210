class Reference
{
	private string _book;
	private int _chapter;
	private int _startVerse;
	private int _endVerse;

	public Reference(string b, int c, int sv, int ev)
	{
		_book = b;
		_chapter = c;
		_startVerse = sv;
		_endVerse = ev;
	}

	public Reference(string b, int c, int sv)
	{
		_book = b;
		_chapter = c;
		_startVerse = sv;
		_endVerse = sv;
	}

	public void Display()
	{
		Console.WriteLine($"{_book} {_chapter}:{_startVerse}");
		if (_endVerse > _startVerse)
		{
			Console.Write($"-{_endVerse}");
		}
	}
}
