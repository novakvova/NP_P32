using System.Net.Sockets;
using System.Text;

Console.InputEncoding = Encoding.UTF8; // Встановлюємо кодування для консолі
Console.OutputEncoding = Encoding.UTF8; // Встановлюємо кодування для виводу в консоль

UdpClient udpClient = new UdpClient();

Console.WriteLine("Вкажіть повідомлення:");
string message  = Console.ReadLine();
var bytes = Encoding.UTF8.GetBytes(message);
string ip = "127.0.0.1";
int port = 1028;

await udpClient.SendAsync(bytes, ip, port);

Console.WriteLine("Повідомлення надіслано! :)");
