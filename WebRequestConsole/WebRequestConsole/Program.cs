// See https://aka.ms/new-console-template for more information
using System.Net;
using Newtonsoft.Json;
using WebRequestConsole.Models;

Console.InputEncoding = System.Text.Encoding.UTF8;
Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.WriteLine("Привіт. Класна погода. Треба вміти плввати і лазити по деревах. :)!");

ViewAll();
//ViewById(134);

//var user = new UserItemModel
//{
//    FirstName = "Іван",
//    SecondName = "Іванов",
//    Photo = "https://example.com/photo.jpg",
//    Phone = "+380123456789",
//    Email="wwww"
//};
//Create(user);


static void Create(UserItemModel user)
{
    var data = JsonConvert.SerializeObject(user);
    string url = $"https://lohika.itstep.click/api/Users/create";
    var request = (HttpWebRequest)WebRequest.Create(url);
    request.Method = "POST";
    request.ContentType = "application/json";
    request.Accept = "application/json";

    using (var streamWriter = new StreamWriter(request.GetRequestStream()))
    {
        streamWriter.Write(data);
    }

    try
    {
        var response = (HttpWebResponse)request.GetResponse();

        using (var streamReader = new StreamReader(response.GetResponseStream()))
        {
            string result = streamReader.ReadToEnd();
            Console.WriteLine(result);
        }
    }
    catch (WebException ex)
    {
        if (ex.Response is HttpWebResponse errorResponse)
        {
            using (var reader = new StreamReader(errorResponse.GetResponseStream()))
            {
                string error = reader.ReadToEnd();
                Console.WriteLine($"Error {(int)errorResponse.StatusCode}: {error}");
            }
        }
        else
        {
            Console.WriteLine($"WebException: {ex.Message}");
        }
    }
}


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
