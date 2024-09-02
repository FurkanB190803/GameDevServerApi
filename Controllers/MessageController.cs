using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ServerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        // In-memory storage for demonstration
        private static List<KeyPressData> keyPressDataStorage = new List<KeyPressData>();
        private static List<MouseMovementData> mouseMovementDataStorage = new List<MouseMovementData>();

        // POST: api/message/KeyInput
        [HttpPost("KeyInput")]
        public IActionResult ReceiveKeyInput([FromBody] KeyPressDataWrapper dataWrapper)
        {
            if (dataWrapper == null || dataWrapper.KeyPressDataList == null)
                return BadRequest("Invalid data");

            keyPressDataStorage.AddRange(dataWrapper.KeyPressDataList);
            return Ok(new { Message = "Key input data received successfully" });
        }

        // POST: api/message/MouseInput
        [HttpPost("MouseInput")]
        public IActionResult ReceiveMouseInput([FromBody] MouseMovementDataWrapper dataWrapper)
        {
            if (dataWrapper == null || dataWrapper.MouseMovementDataList == null)
                return BadRequest("Invalid data");

            mouseMovementDataStorage.AddRange(dataWrapper.MouseMovementDataList);
            return Ok(new { Message = "Mouse input data received successfully" });
        }

        // GET: api/message/KeyInput
        [HttpGet("KeyInput")]
        public IActionResult GetKeyInput()
        {
            return Ok(keyPressDataStorage);
        }

        // GET: api/message/MouseInput
        [HttpGet("MouseInput")]
        public IActionResult GetMouseInput()
        {
            return Ok(mouseMovementDataStorage);
        }
    }
}
