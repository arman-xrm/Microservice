using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParameterService.Models.RequestModels;
using ParameterService.Services.Interface;

namespace ParameterService.Controllers
{
    public class InputController : BaseController
    {
        private readonly IInputService _inputService;

        public InputController(IInputService inputService)
        {
            _inputService = inputService;
        }

        [HttpPost("send")]
        [Authorize]
        public IActionResult Send([FromBody] InputRequestModel input)
        {
            _inputService.SendMessage(input);
            return Ok();
        }
    }
}
