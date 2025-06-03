// See https://aka.ms/new-console-template for more information
using System.Net;
using Newtonsoft.Json;
using WebRequestConsole.Models;

Console.InputEncoding = System.Text.Encoding.UTF8;
Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.WriteLine("Привіт. Класна погода. Треба вміти плввати і лазити по деревах. :)!");

//ViewAll();
ViewById(134);

static void ViewById(int id)
{
    string url = $"https://lohika.itstep.click/api/Users/get/{id}";

    var request = (HttpWebRequest)WebRequest.Create(url);
    request.Method = "GET";

    var resp = (HttpWebResponse)request.GetResponse();
    var reader = new StreamReader(resp.GetResponseStream());
    string data = reader.ReadToEnd();
    var user = JsonConvert.DeserializeObject<UserItemModel>(data);
    Console.WriteLine(user);
}


static void ViewAll()
{
    string url = "https://lohika.itstep.click/api/Users/all";

    var request = (HttpWebRequest)WebRequest.Create(url);
    request.Method = "GET";

    var resp = (HttpWebResponse)request.GetResponse();
    var reader = new StreamReader(resp.GetResponseStream());
    string data = reader.ReadToEnd();
    var list = JsonConvert.DeserializeObject<List<UserItemModel>>(data);
    foreach (var item in list)
    {
        Console.WriteLine(item);
    }
}


//Console.WriteLine("Результат від сервера: {0}", data);
