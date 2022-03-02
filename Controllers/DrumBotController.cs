using System;
using System.Net.Http.Json;
using System.Threading.Tasks;
using DrumBot.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace DrumBot.Controllers
{
    [ApiController]
    [Route("api/message/update")]
    public class DrumBotController : ControllerBase
    {
        private readonly ICommandExecutor _commandExecutor;

        public DrumBotController(ICommandExecutor commandExecutor)
        {
            _commandExecutor = commandExecutor;
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody]object update)
        {
            // /start => register user

            var upd = JsonConvert.DeserializeObject<Update>(update.ToString());

            if (upd?.Message?.Chat == null && upd?.CallbackQuery == null)
            {
                return Ok();
            }

            try
            {
                await _commandExecutor.Execute(upd);
            }
            catch (Exception e)
            {
                return Ok();
            }
            
            return Ok();
        }
    }
}