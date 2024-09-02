using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace ServerApi.Controllers
{
    public class KeyPressData
    {
        public string keyCode;
        public List<string> pressTimes = new List<string>();
        public List<string> releaseTimes = new List<string>();
    }

    public class MouseMovementData
    {
        public string timestamp;
        public float deltaX;
        public float deltaY;
    }

    [ApiController]
    [Route("ping/[controller]")]
    public class PingController : ControllerBase
    {
        [HttpGet]
        public IActionResult Ping()
        {
            // Ping isteði alýndý, herhangi bir iþlem yapmadan hýzlýca yanýt ver
            return Ok(new { message = "Pong", timestamp = DateTime.UtcNow });
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private static List<KeyPressData> _keyPressData = new List<KeyPressData>();
        private static List<MouseMovementData> _mouseMovementData = new List<MouseMovementData>();

        [HttpPost("KeyInput")]
        public IActionResult PostKeyInput([FromBody] KeyPressData keyPressData)
        {
            if (keyPressData == null)
            {
                return BadRequest("Invalid input");
            }

            _keyPressData.Add(keyPressData);

            // Log the received data
            Console.WriteLine("KeyInput received:");
            Console.WriteLine($"KeyCode: {keyPressData.keyCode}");
            Console.WriteLine($"Press Times: {string.Join(", ", keyPressData.pressTimes)}");
            Console.WriteLine($"Release Times: {string.Join(", ", keyPressData.releaseTimes)}");

            return Ok("KeyPress received!");
        }

        [HttpPost("MouseInput")]
        public IActionResult PostMouseInput([FromBody] MouseMovementData mouseMovementData)
        {
            if (mouseMovementData == null)
            {
                return BadRequest("Invalid input");
            }

            _mouseMovementData.Add(mouseMovementData);

            // Log the received data
            Console.WriteLine("MouseInput received:");
            Console.WriteLine($"Timestamp: {mouseMovementData.timestamp}");
            Console.WriteLine($"DeltaX: {mouseMovementData.deltaX}");
            Console.WriteLine($"DeltaY: {mouseMovementData.deltaY}");

            return Ok("MouseInput received!");
        }

        [HttpGet("getKeyInput")]
        public IActionResult GetKeyInput()
        {
            // Log the data being sent
            Console.WriteLine("Getting KeyInput:");
            foreach (var data in _keyPressData)
            {
                Console.WriteLine($"KeyCode: {data.keyCode}");
                Console.WriteLine($"Press Times: {string.Join(", ", data.pressTimes)}");
                Console.WriteLine($"Release Times: {string.Join(", ", data.releaseTimes)}");
            }

            return Ok(_keyPressData);
        }

        [HttpGet("getMouseInput")]
        public IActionResult GetMouseInput()
        {
            // Log the data being sent
            Console.WriteLine("Getting MouseInput:");
            foreach (var data in _mouseMovementData)
            {
                Console.WriteLine($"Timestamp: {data.timestamp}");
                Console.WriteLine($"DeltaX: {data.deltaX}");
                Console.WriteLine($"DeltaY: {data.deltaY}");
            }

            return Ok(_mouseMovementData);
        }
    }
}
