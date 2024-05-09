class Course
{
    public string ClassCode;
    public string ClassName;
    public int Credits;
    public string Color;

    public void Display()
    {
        Console.WriteLine($"{ClassCode} {ClassName} {Credits} {Color}");
    }
}