using System.Net.Sockets;
using System.Net;
using System.Text;

namespace ServerAsyncSocket;

// Клас асинхронного TCP-сервера
class AsyncTcpServer
{
    // Статичний метод для запуску сервера
    public static async Task StartServerAsync()
    {
        // Створюємо сокет, який використовує IPv4, потоковий тип (TCP), і протокол TCP
        var listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        // Прив'язуємо сокет до всіх доступних IP-адрес машини і порту 8080
        listener.Bind(new IPEndPoint(IPAddress.Any, 1345));

        // Починаємо слухати підключення з чергою на 100 клієнтів
        listener.Listen(100);

        Console.WriteLine("Сервер слухає на порту 1345...");

        // Нескінченний цикл: постійно очікуємо нові підключення
        while (true)
        {
            // Асинхронно приймаємо підключення клієнта (не блокує потік)
            Socket clientSocket = await listener.AcceptAsync();

            // Обробка клієнта запускається у фоновому режимі (без очікування завершення)
            _ = HandleClientAsync(clientSocket);

        }
        
    }

    // Метод для обробки кожного окремого клієнта
    private static async Task HandleClientAsync(Socket clientSocket)
    {
        Console.WriteLine("Клієнт підключився.");

        // Виділяємо буфер для прийому даних (до 1024 байт)
        byte[] buffer = new byte[1024];

        // Асинхронно отримуємо дані від клієнта
        int received = await clientSocket.ReceiveAsync(buffer, SocketFlags.None);

        // Перетворюємо отримані байти в текст за допомогою UTF8-кодування
        string request = Encoding.UTF8.GetString(buffer, 0, received);
        Console.WriteLine($"Запит: {request}");

        // Формуємо відповідь у вигляді тексту
        string response = "Привіт від сервера!";
        // Перетворюємо відповідь у байти
        byte[] responseBytes = Encoding.UTF8.GetBytes(response);

        // Асинхронно надсилаємо відповідь клієнту
        await clientSocket.SendAsync(responseBytes, SocketFlags.None);

        // Завершуємо з'єднання (з обох сторін: надсилання і прийом)
        clientSocket.Shutdown(SocketShutdown.Both);

        // Закриваємо сокет
        clientSocket.Close();
    }
}
