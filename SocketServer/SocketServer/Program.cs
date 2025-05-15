using System.Net;
using System.Net.Sockets;
using System.Text;

Console.InputEncoding = Encoding.UTF8;
Console.OutputEncoding = Encoding.UTF8;

var server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

var endPoint = new IPEndPoint(IPAddress.Any, 2318);

server.Bind(endPoint);

server.Listen(10);


Console.WriteLine($"Запукск сервера {endPoint}");

while (true)
{
    Socket client = server.Accept();

    Console.WriteLine($"Клієнт підключився {client.RemoteEndPoint}");

    var buffer = new byte[1024];
    int sizeBytes = client.Receive(buffer); //Читаємо байти, що прислав сервак

    var text = Encoding.UTF8.GetString(buffer, 0, sizeBytes);
    Console.WriteLine($"Оримали: {text}");

    string resultText = "Дякую козаче!";
    client.Send(Encoding.UTF8.GetBytes(resultText));

    client.Shutdown(SocketShutdown.Both);
    client.Close();
}


server.Close();