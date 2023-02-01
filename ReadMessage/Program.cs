using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using MVCSignalR.Models;
using Microsoft.AspNetCore.Components;
using RabbitMQ.Client.Events;
using System.Net.Mail;
using ReadMessage;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;

HubConnection hubConnection = new HubConnectionBuilder().WithUrl("http://localhost:5088/chatHub").Build();
await hubConnection.StartAsync();

ConnectionFactory factory = new ConnectionFactory();
factory.Uri = new Uri("amqps://bjfirkun:BWUeVguc4p7PObOrwnkPJ9eAV8uDBTx3@cougar.rmq.cloudamqp.com/bjfirkun");
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();
//string serializedData = JsonSerializer.Deserialize<User>();
//byte[] byteData = Encoding.UTF8.GetBytes(serializedData);
channel.QueueDeclare("messagequeue", false, false, true);
EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
channel.BasicConsume("messagequeue", true, consumer);
consumer.Received += async (sender, e) =>
{
   User user= JsonSerializer.Deserialize<User>(Encoding.UTF8.GetString(e.Body.Span));
    //EmailSender.SendEmail("familhemidov71@gmail.com", "Report", user.Message);
    Console.WriteLine($"{user.Email} mesaj gonderildi");
    await hubConnection.InvokeAsync("SendMessage", $"{user.Email} mesaj gonderildi");
};
Console.Read();