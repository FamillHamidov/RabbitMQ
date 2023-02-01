using Microsoft.AspNetCore.Mvc;
using MVCSignalR.Models;
using RabbitMQ.Client;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace MVCSignalR.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(User user)
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.Uri = new Uri("amqps://bjfirkun:BWUeVguc4p7PObOrwnkPJ9eAV8uDBTx3@cougar.rmq.cloudamqp.com/bjfirkun");
            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();
            string serializedData=JsonSerializer.Serialize(user);   
            byte[] byteData = Encoding.UTF8.GetBytes(serializedData);
            channel.QueueDeclare("messagequeue", false, false, true);
            channel.BasicPublish("", routingKey: "messagequeue",body:byteData);
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}