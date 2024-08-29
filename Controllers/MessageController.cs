using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ServerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        // Basit bir liste ile mesajlar� depola
        private static List<string> _messages = new List<string>();

        // GET iste�i ile t�m mesajlar� d�nd�r
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_messages);
        }

        // POST iste�i ile yeni mesaj� al ve listeye ekle
        [HttpPost]
        public IActionResult Post([FromBody] string message)
        {
            _messages.Add(message);
            Console.WriteLine(message); // Konsola yazd�r
            return Ok("Message received!");
        }
    }
}
