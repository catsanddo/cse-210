/* Travis Scoville (c) 2024
 * Journal program main
 */

class Entry
{
    public string _prompt;
    public string _response;
    public string _date;

    public void TakeResponse(string prompt)
    {
        Console.WriteLine(prompt);
        Console.WriteLine("(Enter your response here. Type a \".\" on\na line by itself to finish.)");

        _response = "";
        bool finished = false;
        while (!finished)
        {
            Console.Write("> ");
            string inputLine = Console.ReadLine();

            if (inputLine == ".")
            {
                _response = _response.Trim();
                finished = true;
            }
            else
            {
                _response += inputLine + "\n";
            }
        }

        _prompt = prompt;
        _date = DateTime.Now.ToString();
    }
}