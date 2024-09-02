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

            // Veriyi atama yaparak güncelle
            keyPressDataStorage = new List<KeyPressData>(dataWrapper.KeyPressDataList);

            return Ok(new { Message = "Key input data received and updated successfully" });
        }

        // POST: api/message/MouseInput
        [HttpPost("MouseInput")]
        public IActionResult ReceiveMouseInput([FromBody] MouseMovementDataWrapper dataWrapper)
        {
            if (dataWrapper == null || dataWrapper.MouseMovementDataList == null)
                return BadRequest("Invalid data");

            // Veriyi atama yaparak güncelle
            mouseMovementDataStorage = new List<MouseMovementData>(dataWrapper.MouseMovementDataList);

            return Ok(new { Message = "Mouse input data received and updated successfully" });
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
