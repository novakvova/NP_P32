using System.Net;
using System.Net.Sockets;
using System.Text;

Console.InputEncoding = Encoding.UTF8;
Console.OutputEncoding = Encoding.UTF8;

UdpClient server = new UdpClient(2839);
//Це endPoint - клієнта, який прислає запит на сервер
//IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);
IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("192.168.50.93"), 0);

Console.WriteLine("Очікуємо повідомлення ...");

while (true)
{
    var data = server.Receive(ref endPoint);
    string message = Encoding.UTF8.GetString(data);
    Console.WriteLine($"Отримали від клієнта {endPoint}: {message}");
}
