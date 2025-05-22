using System.Net;
using System.Net.Sockets;
using System.Text;

Console.InputEncoding = Encoding.UTF8;
Console.OutputEncoding = Encoding.UTF8;

Console.WriteLine("Вкажіть IP сервера");
string ipServer = Console.ReadLine();
if(string.IsNullOrEmpty(ipServer))
    ipServer = IPAddress.Loopback.ToString(); //127.0.0.1

int port = 2084;

TcpClient myClient = new TcpClient();
myClient.Connect(IPAddress.Parse(ipServer), port);

Console.WriteLine($"Підключилися до сервера {myClient.Client.RemoteEndPoint.ToString()}");

NetworkStream myStream = myClient.GetStream(); //Отворюємо потік для роботи із сервером
Console.WriteLine("Вкажіть текст:");
string message = Console.ReadLine();
var bytes = Encoding.UTF8.GetBytes(message);
//аналог Socket.Send
myStream.Write(bytes, 0, bytes.Length); //відправляємо байти на сервер, як повідомлення 

//Очікуємо відповіді від сервера
byte[] listBytes = new byte[10024];
//Отримує дані від сервера аналогічно як Socket.Receive()
int countBytes = myStream.Read(listBytes, 0, listBytes.Length);
string result = Encoding.UTF8.GetString(listBytes);

Console.WriteLine("Відповідь сервера: " + result);

myStream.Close();
myClient.Close();