// See https://aka.ms/new-console-template for more information
using System.Net;
using System.Net.Sockets;

Console.InputEncoding = System.Text.Encoding.UTF8;
Console.OutputEncoding = System.Text.Encoding.UTF8;

var listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
var ipEndPoint = new IPEndPoint(IPAddress.Any, 8888);
listener.Bind(ipEndPoint); //Привязуємо слухача до IP-адреси та порту
listener.Listen(100);   //Встановлюємо максимальну кількість підключень

Console.WriteLine($"Сервер запущено {ipEndPoint}");

while (true)
{
    Socket client = await listener.AcceptAsync(); //Чекаємо на підключення клієнта
    
    _ = Task.Run(() => HandleClientAsync(client)); //Запускаємо обробник клієнта в окремому потоці
}
//Метод, який буде обробляти запити клієнта
async Task HandleClientAsync(Socket client)
{
    Console.WriteLine($"Клієнт {client.RemoteEndPoint} підлкючений");

    byte[] buffer = new byte[10024];
    int bytesRead = await client.ReceiveAsync(buffer); //Читаємо дані з сокета
    string message = System.Text.Encoding.UTF8.GetString(buffer, 0, bytesRead); //Перетворюємо байти в рядок
    Console.WriteLine($"Клієнт пислав повідомлення {message}");
    //Відправляємо відповідь клієнту - сервер відповідає на запит клієнта
    string response = $"Сервер отримав ваше повідомлення: {DateTime.Now} - {client.RemoteEndPoint}";
    byte[] responseBytes = System.Text.Encoding.UTF8.GetBytes(response); //Перетворюємо рядок в байти
    await client.SendAsync(responseBytes); //Відправляємо байти клієнту
    client.Shutdown(SocketShutdown.Both); //Закриваємо сокет
    client.Close(); //Закриваємо сокет
}