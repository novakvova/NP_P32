using System.Net;
using System.Net.Sockets;
using System.Text;

var ip = "239.0.0.222";
var port = 5350;

UdpClient sender = new UdpClient();
var endPoint = new IPEndPoint(IPAddress.Parse(ip), port);

string message = "Привіт multicast-мережа";
var data = Encoding.UTF8.GetBytes(message);

sender.Send(data, data.Length, endPoint);

Console.WriteLine("Повідомлення надіслано.");

sender.Close();
