using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;
using TelegramBot.Models.Telegram;
using TelegramBot.Service.Telegram;

namespace TelegramBot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelegramController : ControllerBase
    {
        //نمونه سازی از کلای =>ITelegramService و ساخت api همه ی متدهاش
        private readonly TelegramService _telegramService;
        public TelegramController(TelegramService telegramService)
        {
            _telegramService = telegramService;
        }
        //ارسال پیام 
        [HttpPost("sendMessage")]
        public async Task<IActionResult> SendMessage([FromBody] SendMessageRequest request )
        {
            var result = await _telegramService.SendMessage(request.Message);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        // ارسال فایل 
        [HttpPost("sendFile")]
        public async Task<IActionResult> SendFile([FromForm] SendFileRequest request )
        {
            var result = await _telegramService.SendFile(request.File);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        //ویرایش پیام 
        [HttpPost("editMessage")]
        public async Task<IActionResult> EditMessage([FromBody] EditMessageRequest request)
        {
            var result = await _telegramService.Edit(request.MessageId, request.Text);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        //حذف پیام 
        [HttpPost("removeMessage")]
        public async Task<IActionResult> RemoveMessage([FromBody] RemoveMessageRequest request) 
        {
            var result = await _telegramService.Remove(request.PostId);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
        
       
        
