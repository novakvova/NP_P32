using System.Net.Sockets;
using System.Text;

Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

//client.Connect("127.0.0.1", 2318);
client.Connect("91.238.103.107", 2318);

string message = "Привіт, мій хост! То я Вова :)";

client.Send(Encoding.UTF8.GetBytes(message));

byte[] buffer = new byte[1024];
int bytes = client.Receive(buffer);
Console.WriteLine($"Відповідь від сервера {Encoding.UTF8.GetString(buffer)}");

client.Shutdown(SocketShutdown.Both);
client.Close();
