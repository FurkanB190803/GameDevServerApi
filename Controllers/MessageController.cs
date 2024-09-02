using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerApi.Controllers
{
    
   /* public class KeyPressModel
    {
        public bool IsPressed { get; set; }
        public string Key { get; set; }
        public string Timestamp { get; set; }
    }*/
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
/*
    public class CameraDataModel
    {
        public string px { get; set; }
        public string py { get; set; }
        public string pz { get; set; }
        public string cx { get; set; }
        public string cy { get; set; }
        public string cz { get; set; }
        public string Timestamp { get; set; }
    }
*/

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
        //private static List<KeyPressModel> _keyPresses = new List<KeyPressModel>();

        //private static List<CameraDataModel> _cameraData = new List<CameraDataModel>();

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
            return Ok("KeyPress received!");
        }

        [HttpGet("getKeyInput")]
        public IActionResult GetKeyInput()
        {
            return Ok(_keyPressData);
        }
        [HttpGet("getMouseInput")]
        public IActionResult GetMouseInput()
        {
            return Ok(_mouseMovementData);
        }








        /*

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


      
        [HttpGet("cameras")]
        public IActionResult GetCamera()
        {
            return Ok(_cameraData);
        }

        // POST: api/message/camera

       

        [HttpPost("camera")]
        public IActionResult PostCameraData([FromBody] CameraDataModel cameraData)
        {
            if (cameraData == null)
            {
                return BadRequest("Geçersiz giriþ");
            }

            _cameraData.Add(cameraData);
            return Ok("Kamera verisi alýndý!");
        }*/
    }


}
