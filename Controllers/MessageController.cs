using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ServerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        // Basit bir liste ile mesajlarý depola
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
            Console.WriteLine(message); // Konsola yazdýr
            return Ok("Message received!");
        }
    }
}
