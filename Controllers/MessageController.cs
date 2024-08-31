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

   

    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private static List<KeyPressModel> _keyPresses = new List<KeyPressModel>();

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
                return BadRequest("Ge�ersiz giri�");
            }

            _cameraData.Add(cameraData);
            return Ok("Kamera verisi al�nd�!");
        }
    }


}
