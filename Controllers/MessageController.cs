using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private static List<string> _messages = new List<string>();

        // GET isteði ile tüm mesajlarý döndür
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_messages);
        }

        // POST isteði ile yeni mesajý al ve listeye ekle
        [HttpPost]
        public IActionResult Post([FromBody] string message)
        {
            _messages.Add(message);
            Console.WriteLine(message);
            return Ok("Message received!");
        }

        // SSE endpoint'i ile mesajlarý stream et
        [HttpGet("stream")]
        public async Task Stream()
        {
            Response.ContentType = "text/event-stream";
            Response.Headers.Add("Cache-Control", "no-cache");
            Response.Headers.Add("Connection", "keep-alive");

            while (true)
            {
                if (Response.HttpContext.RequestAborted.IsCancellationRequested)
                    break;

                var message = string.Join("\n", _messages);
                await Response.WriteAsync($"data: {message}\n\n");
                await Response.Body.FlushAsync();

                await Task.Delay(50); // 50 ms gecikme
            }
        }
    }
}
