using Microsoft.AspNetCore.Mvc;
using VkBot.Abstractions;
using VkNet.Abstractions;
using VkNet.Enums.StringEnums;
using VkNet.Model;

namespace VkBot.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CallbackController : ControllerBase
    {
        private readonly ILogger _logger;

        private readonly IVkApi _vkApi;

        private readonly IUpdateRepository _updateRepository;

        public CallbackController(ILogger<CallbackController> logger, 
            IVkApi vkApi,
            IUpdateRepository updateRepository)
        {
            _logger = logger;
            _vkApi = vkApi;
            _updateRepository = updateRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Callback([FromBody] GroupUpdate update)
        {
            switch (update.Type.Value)
            {
                case GroupUpdateType.Confirmation:
                    {
                        var groupId = (ulong)update.GroupId.Value;
                        var code = await _vkApi.Groups.GetCallbackConfirmationCodeAsync(groupId);
                        return Ok(code);
                    }
                default:
                    {
                        await _updateRepository.HandleUpdate(update);
                        break;
                    }
            }
            return Ok("ok");
        }
    }
}
