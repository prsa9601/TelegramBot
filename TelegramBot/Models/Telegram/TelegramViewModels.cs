using Telegram.Bot.Types;
namespace TelegramBot.Models.Telegram
{
    public class TelegramViewModels
    {
        //Generate Send , Edit, Remove Models
    }
    public class SendMessageRequest
    {
        public required string Message { get; set; }
    }
    public class SendFileRequest
    {
        public required InputFile File { get; set; }
    }
    public class EditMessageRequest
    {
        public int MessageId { get; set; }
        public required string Text { get; set; }
    }
    public class RemoveMessageRequest
    {
        public int PostId { get; set; }
    }
}
