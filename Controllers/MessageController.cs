using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;

namespace ServerApi.Controllers
{
    public class KeyPressData
    {
        public string keyCode { get; set; }
        public List<string> pressTimes { get; set; } = new List<string>();
        public List<string> releaseTimes { get; set; } = new List<string>();
    }

    public class MouseMovementData
    {
        public string timestamp { get; set; }
        public float deltaX { get; set; }
        public float deltaY { get; set; }
    }

    [ApiController]
    [Route("ping/[controller]")]
    public class PingController : ControllerBase
    {
        [HttpGet]
        public IActionResult Ping()
        {
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

            // Log the received data
            Console.WriteLine($"keyPressData: {keyPressData}");

            _keyPressData.Add(keyPressData);
            return Ok("KeyPress received!");
        }

        [HttpPost("MouseInput")]
        public IActionResult PostMouseInput([FromBody] MouseMovementData mouseMovementData)
        {
            if (mouseMovementData == null)
            {
                return BadRequest("Invalid input");
            }

            // Log the received data
            Console.WriteLine($"MouseInput: {mouseMovementData}");

            _mouseMovementData.Add(mouseMovementData);
            return Ok("MouseInput received!");
        }

        [HttpGet("getKeyInput")]
        public IActionResult GetKeyInput()
        {
            return Ok("ddd");
        }

        [HttpGet("getMouseInput")]
        public IActionResult GetMouseInput()
        {
            return Ok(_mouseMovementData);
        }
    }
}
