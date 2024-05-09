using System;
using System.Security.Cryptography.X509Certificates;

class Program
{
    static void Main(string[] args)
    {
        Course course1 = new();
        course1.ClassCode = "CSE210";
        course1.ClassName = "Programming with Classes";
        course1.Credits = 2;
        course1.Color = "green";

        course1.Display();
    }
}