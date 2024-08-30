using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerApi.Controllers
{
    public class Vector3
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }

    public class KeyPressModel
    {
        public bool IsPressed { get; set; }
        public string Key { get; set; }
        public string Timestamp { get; set; }
    }

    public class MouseDataModel
    {
        public float MouseX { get; set; }
        public float MouseY { get; set; }
        public string Timestamp { get; set; }
    }

    public class CameraDataModel
    {
        public Vector3 CameraPosition { get; set; }
        public Vector3 CameraRotation { get; set; }
        public string Timestamp { get; set; }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private static List<KeyPressModel> _keyPresses = new List<KeyPressModel>();
        private static List<MouseDataModel> _mouseData = new List<MouseDataModel>();
        private static List<CameraDataModel> _cameraData = new List<CameraDataModel>();

        // POST: api/message/keypress
      
        [HttpPost("keypress")]
        public IActionResult PostKeyPress([FromBody] KeyPressModel keyPress)
        {
            if (keyPress == null)
            {
                return BadRequest("Invalid input");
            }

            _keyPresses.Add(keyPress);
            return Ok("KeyPress received!");
        }

        // GET: api/message/keypresses
        [HttpGet("keypresses")]
        public IActionResult GetKeyPresses()
        {
            return Ok(_keyPresses);
        }

        // POST: api/message/mouse
        /*[HttpPost("mouse")]
        public IActionResult PostMouseData([FromBody] MouseDataModel mouseData)
        {
            if (mouseData == null)
            {
                return BadRequest("Invalid input");
            }

            _mouseData.Add(mouseData);
            return Ok("Mouse data received!");
        }
        
        // POST: api/message/camera
        [HttpPost("camera")]
        public IActionResult PostCameraData([FromBody] CameraDataModel cameraData)
        {
            if (cameraData == null)
            {
                return BadRequest("Invalid input");
            }

            _cameraData.Add(cameraData);
            return Ok("Camera data received!");
        }*/
    }


}
