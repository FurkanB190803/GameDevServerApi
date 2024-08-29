using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerApi.Controllers
{
    public class KeyPressModel
    {
        public bool IsPressed { get; set; }
        public string Key { get; set; }
        public string Timestamp { get; set; }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private static List<KeyPressModel> _messages = new List<KeyPressModel>();

        // GET isteði ile tüm mesajlarý döndür
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_messages);
        }

        // POST isteði ile yeni mesajý al ve listeye ekle
        [HttpPost]
        public IActionResult Post([FromBody] KeyPressModel keyPress)
        {
            if (keyPress == null)
            {
                return BadRequest("Invalid input");
            }

            _messages.Add(keyPress);
            Console.WriteLine($"{keyPress.IsPressed}, {keyPress.Key}, {keyPress.Timestamp}");
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

                var messages = _messages.Select(m => $"{m.IsPressed}, {m.Key}, {m.Timestamp}");
                var message = string.Join("\n", messages);
                await Response.WriteAsync($"data: {message}\n\n");
                await Response.Body.FlushAsync();

                await Task.Delay(50); // 50 ms gecikme
            }
        }
    }
}
