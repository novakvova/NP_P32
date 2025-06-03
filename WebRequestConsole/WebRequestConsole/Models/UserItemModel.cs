namespace WebRequestConsole.Models;

public class UserItemModel
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    public string Photo { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public override string ToString()
    {
        return $"{Id}\t{SecondName} {FirstName}\t{Photo}\t{Phone}\t{Email}";
    }
}
