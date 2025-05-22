// See https://aka.ms/new-console-template for more information

using System.Net;
using System.Net.Sockets;
using System.Text;

Console.InputEncoding = Encoding.UTF8;
Console.OutputEncoding = Encoding.UTF8;

int port = 2084;
TcpListener listener = new TcpListener(IPAddress.Any, port); //0.0.0.0 - 127.0.0.1
listener.Start();
Console.WriteLine($"Запустили серве {listener.LocalEndpoint}");

while (true)
{
    //TcpClient - клас, який представляє клієнтське з'єднання
    var client = listener.AcceptTcpClient(); //отримуємо запит від клієнта
    Console.WriteLine($"Клієнт приєднався {client.Client.RemoteEndPoint.ToString()}");
    //Це потік через який ми можемо взяємодіяти із клієнтом. Це особливість використання класу TcpClient
    NetworkStream stream = client.GetStream(); //отримуємо потік даних

    byte[] buffer = new byte[1023];
    //Отримуємо кільсть байт від клєінта і читаємо байти в buffer
    int bytesRead = stream.Read(buffer, 0, buffer.Length);
    string text = Encoding.UTF8.GetString(buffer); //переводимо байти у символи.
    Console.WriteLine($"Повідомлення: {text}");
    text = $"Дякую клієте {DateTime.UtcNow.ToString()}. Смакуй.";
    buffer = Encoding.UTF8.GetBytes(text); //повімлення перетворив у байти
    stream.Write(buffer); //відправляю повідомлення на клієнт
    stream.Close();
    client.Close();
    Console.WriteLine("Завершив роботу із клієнтом.");
}
