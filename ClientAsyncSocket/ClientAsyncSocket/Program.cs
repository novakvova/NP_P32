using System.Net;
using System.Net.Sockets;

Console.InputEncoding = System.Text.Encoding.UTF8;
Console.OutputEncoding = System.Text.Encoding.UTF8;

Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

//var endPoint = new IPEndPoint(IPAddress.Loopback, 8888);
var endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8888);

Console.WriteLine($"Search Server {endPoint}");

//Асинхронно підлкючаємо до сервера
await clientSocket.ConnectAsync(endPoint);
string text = "Сервер, що ти сьогодні їв?";
byte[] data = System.Text.Encoding.UTF8.GetBytes(text);
await clientSocket.SendAsync(data); //Надсилаємо асинхронно запит
byte[] buffer = new byte[10024];
//Байти які нам присилає сервер
int bytes = await clientSocket.ReceiveAsync(buffer); //Отримуємо асинхронно відповідь
string textServerResp = System.Text.Encoding.UTF8.GetString(buffer, 0, bytes);
Console.WriteLine($"Server response: {textServerResp}");

clientSocket.Shutdown(SocketShutdown.Both); //Закриваємо сокет
clientSocket.Close(); //Закриваємо сокет