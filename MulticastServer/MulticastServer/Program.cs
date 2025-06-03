
using System.Net;
using System.Net.Sockets;
using System.Text;

Console.InputEncoding = Encoding.UTF8;
Console.OutputEncoding = Encoding.UTF8;

string ipMulticast = "239.0.0.222";

int port = 5350;

UdpClient udpClient = new UdpClient(port);

//Наша група для multicast
udpClient.JoinMulticastGroup(IPAddress.Parse(ipMulticast));

IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, port);

Console.WriteLine("Очікуємо multicast ...");

while (true)
{
    var data = udpClient.Receive(ref remoteEP);
    string message = Encoding.UTF8.GetString(data);
    Console.WriteLine($"Отримали {remoteEP}: {message}");
}