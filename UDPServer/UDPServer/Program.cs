
using System.Net.Sockets;
using System.Text;

Console.InputEncoding = Encoding.UTF8; // Встановлюємо кодування для консолі
Console.OutputEncoding = Encoding.UTF8; // Встановлюємо кодування для виводу в консоль

var udpClient = new UdpClient(1028); //0 - 65535 - 2 байти
Console.WriteLine("UDP Server is running on port 1028...");

while(true)
{
    //Очікуємо повідомлення від клієнта
    var result = await udpClient.ReceiveAsync();
    string text = Encoding.UTF8.GetString(result.Buffer); // отримуємо байти
    Console.WriteLine($"Отримали повідомлення: {text} від {result.RemoteEndPoint.Address}:{result.RemoteEndPoint.Port}");
}



