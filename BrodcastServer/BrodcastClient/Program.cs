using System.Net;
using System.Net.Sockets;
using System.Text;

Console.InputEncoding = Encoding.UTF8;
Console.OutputEncoding = Encoding.UTF8;

UdpClient sender = new UdpClient();
//sender.EnableBroadcast = true;

Console.WriteLine("Вкажіть повідомлення:");
string message = Console.ReadLine();

IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Broadcast, 2839);
byte[] data = Encoding.UTF8.GetBytes(message);
sender.Send(data, data.Length, iPEndPoint);

Console.WriteLine("Повідолення надіслано :)");

sender.Close();