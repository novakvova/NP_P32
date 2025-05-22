using System.Text;
using System.Text.Json;
using HomeWork1Example;
using Newtonsoft.Json;

class Program
{
    static async Task Main()
    {
        string folder = @"D:\girl";
        string url = "https://myp22.itstep.click/api/Galleries/upload";

        if (!Directory.Exists(folder))
        {
            Console.WriteLine("Папка не знайдена");
            return;
        }

        var imageFiles = Directory.GetFiles(folder, "*.jpg");
        int images = imageFiles.Length;
        int count = 0;
        int secondcount = 0;

        foreach (var imagePath in imageFiles)
        {
            if (File.Exists(imagePath))
            {
                try
                {
                    byte[] bytes = File.ReadAllBytes(imagePath);
                    string imageBase64 = Convert.ToBase64String(bytes);
                    //string base64 = $"data:image/png;base64,{imageBase64}";

                    var json = System.Text.Json.JsonSerializer.Serialize(new { Photo = imageBase64 });

                    using var client = new HttpClient();
                    //body - запиту
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    //отримує результат
                    var response = await client.PostAsync(url, content);
                    //декодуємо результат сервера - читаємо тіло відповіді
                    var result = await response.Content.ReadAsStringAsync();

                    var resultObject = JsonConvert.DeserializeObject<ServerResultImage>(result);
                    Console.WriteLine($"Server image  https://myp22.itstep.click/images/{resultObject.Image}");

                    //Перевіряємо статус відповіді - якщо 200 - то усе супер.  - Сервер фото зберіг
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"Програма успішно виконалась: {Path.GetFileName(imagePath)}");
                        count++;
                    }
                    else //Щось пішло не так
                    {
                        Console.WriteLine($"Виникла помилка: {Path.GetFileName(imagePath)} - {result}");
                        secondcount++;
                    }
                }
                catch (Exception ex) //якась глобальна помилка - наприклад сервер потух, місце кончилося
                {
                    Console.WriteLine($"Помилка при обробці файлу: {ex.Message}");
                    secondcount++;
                }
            }
        }

        Console.WriteLine($"Успішно надіслано: {count} з {images}");
        Console.WriteLine($"Кількість не надісланих файлів: {secondcount}");
    }
}