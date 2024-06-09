using System.Net.Http.Headers;
using System.Text;
using IoTEcoWatt.Entities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

// Без цього ми не побачимо українськи символи
Console.OutputEncoding = Encoding.UTF8;

// Завантаження конфігурації з файлу appsettings.json
var builder = new ConfigurationBuilder();
builder.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false);
IConfiguration configBuilder = builder.Build();
var configuration = new Configuration();
configBuilder.Bind(configuration);

Console.WriteLine("Ласкаво просимо до системи EcoWatt!");

// Аутентифікація та отримання JWT


using var client = new HttpClient();
var loginData = new
{
    email = configuration.Credentials.Email,
    password = configuration.Credentials.Password,
};

var loginContent = new StringContent(JsonConvert.SerializeObject(loginData), Encoding.UTF8, "application/json");
var loginResponse = await client.PostAsync($"{configuration.ApiUrl}/User/Login", loginContent);

if (!loginResponse.IsSuccessStatusCode)
{
    Console.WriteLine($"Помилка: {loginResponse.StatusCode} - Неможливо увійти.");
    return;
}

var jwt = await loginResponse.Content.ReadAsStringAsync();
client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

Console.WriteLine("Перевірка даних користувача");
for (int i = 0; i < 3; i++)
{
    Console.WriteLine("...");
    await Task.Delay(1000);
}

try
{
    // Отримання даних користувача
    var userResponse = await client.GetAsync($"{configuration.ApiUrl}/User/{1}");

    // Перевірка чи користувача не знайдено
    if (!userResponse.IsSuccessStatusCode)
    {
        Console.WriteLine($"Помилка: {userResponse.StatusCode} - Неможливо знайти користувача.");
        return;
    }
    
    var userResponseBody = await userResponse.Content.ReadAsStringAsync();
    var user = JsonConvert.DeserializeObject<User>(userResponseBody);

    Console.WriteLine($"Привіт, {user.Name}!");

    Console.WriteLine("Отримання даних вашого номеру...");
    for (int i = 0; i < 2; i++)
    {
        Console.WriteLine("...");
        await Task.Delay(1000);
    }
    
    var response = await client.GetAsync($"{configuration.ApiUrl}/Apartments/{1}");
    
    if (!response.IsSuccessStatusCode)
    {
        Console.WriteLine($"Помилка: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
        return;
    }
    
    Console.WriteLine("Отримання даних з датчиків вашого номеру...");
    for (int i = 0; i < 2; i++)
    {
        Console.WriteLine("...");
        await Task.Delay(1000);
    }
    
    var response_iot = await client.GetAsync($"{configuration.ApiUrl}/Measurement");
    
    if (!response_iot.IsSuccessStatusCode)
    {
        Console.WriteLine($"Помилка: {response_iot.StatusCode} - {await response_iot.Content.ReadAsStringAsync()}");
        return;
    }

    var responseBody = await response_iot.Content.ReadAsStringAsync();
    var measurements = JsonConvert.DeserializeObject<Measurement[]>(responseBody)!;
    
    Console.WriteLine("Дані успішно отримано:");
    
    foreach (var measurement in measurements)
    {
        Console.WriteLine($"Показник датчику: {measurement.Metrics}");
        Console.WriteLine($"Значення виміру: {measurement.Value}");
        Console.WriteLine($"Дата виміру: {measurement.TimeSpan}");
        Console.WriteLine("-------------------");
    }
    Console.WriteLine("Хай щастить!");
}
catch (Exception ex)
{
    // Відображення будь-якої виняткової ситуації, яка може виникнути
    Console.WriteLine($"Виникла несподівана помилка: {ex.Message}");
}