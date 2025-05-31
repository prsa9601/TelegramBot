using System.Net;
using System;
using TelegramBot.Infrastructure;
using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types.Enums;
namespace TelegramBot.Service.Telegram
{
    public interface ITelegramService
    {
        Task<ApiResult> SendMessage(string message);
        Task<ApiResult> SendFile(InputFile file);
        Task<ApiResult> Edit(int messageId, string text);
        Task<ApiResult> Remove(int postId);
    }
    public class TelegramService : ITelegramService
    {
        public async Task<ApiResult> Edit(int messageId, string text)
        {
            try
            {
                var chatId = -123456789; // شناسه چت
                                         //var messageId = "MESSAGE_ID"; // شناسه پیام
                var bot = new TelegramBotClient("7503920971:AAEssbgfdhedRM");
                await bot.EditMessageTextAsync(chatId, messageId, text);
                return new ApiResult
                {
                    IsSuccess = true,
                    MetaData = new MetaData
                    {
                        Message = ApiResult.SuccessMessage,
                        statusCode = AppStatusCode.Success
                    }
                };
            }
            catch (Exception e)
            {
                return new ApiResult
                {
                    IsSuccess = true,
                    MetaData = new MetaData
                    {
                        Message = ex.Message,
                        statusCode = AppStatusCode.ServerError
                    }
                };
            }
        }

        public async Task<ApiResult> Remove(int messageId)
        {
            try
            {
                var chatId = -123456789; // شناسه چت
                //var messageId = "MESSAGE_ID"; // شناسه پیام
                var bot = new TelegramBotClient("7503920971:AAEssbgfdhedRM");
                await bot.DeleteMessageAsync(chatId, messageId);
                return new ApiResult
                {
                    IsSuccess = true,
                    MetaData = new MetaData
                    {
                        Message = ApiResult.SuccessMessage,
                        statusCode = AppStatusCode.Success
                    }
                };
            }
            catch (Exception e)
            {
                return new ApiResult
                {
                    IsSuccess = true,
                    MetaData = new MetaData
                    {
                        Message = ex.Message,
                        statusCode = AppStatusCode.ServerError
                    }
                };
            }
        }

        public async Task<ApiResult> SendFile(InputFile file)
        {
            try
            {
                var chatId = -123456789; // شناسه چت
                //var messageId = "MESSAGE_ID"; // شناسه پیام
                var bot = new TelegramBotClient("7503920971:AAEssbgfdhedRM");
                await bot.SendPhotoAsync(chatId, file);
                return new ApiResult
                {
                    IsSuccess = true,
                    MetaData = new MetaData
                    {
                        Message = ApiResult.SuccessMessage,
                        statusCode = AppStatusCode.Success
                    }
                };
            }
            catch (Exception e)
            {
                return new ApiResult
                {
                    IsSuccess = true,
                    MetaData = new MetaData
                    {
                        Message = ex.Message,
                        statusCode = AppStatusCode.ServerError
                    }
                };
            }
        }

        public async Task<ApiResult> SendMessage(string message)
        {
            try
            {
                //https://api.telegram.org/bot123456:ABC-DEF1234ghIkl-zyx57W2v1u123ew11/getMe
                var botClient = new TelegramBotClient
                    ("7503920971:AAEssyhfsjr7564wlodRM");
                var result = await botClient.SendTextMessageAsync(chatId: "-123456789", text: message);

                // ارسال پیام به کانال
                //var sentMessage = await botClient.SendTextMessageAsync(
                //    chatId: -1002675250569, 
                //    text: message
                //);
                return new ApiResult
                {
                    IsSuccess = true,
                    MetaData = new MetaData
                    {
                        Message = ApiResult.SuccessMessage,
                        statusCode = AppStatusCode.Success
                    }
                };
            }
            catch (ApiRequestException ex) when (ex.Message.Contains("Forbidden"))
            {
                //Console.WriteLine($"Error: Bot is not admin in channel {channelName}");
                return new ApiResult
                {
                    IsSuccess = true,
                    MetaData = new MetaData
                    {
                        Message = ex.Message,
                        statusCode = AppStatusCode.BadRequest
                    }
                }; // 403
            }
            catch (ApiRequestException ex)
            {
                Console.WriteLine($"Telegram API Error: {ex.ErrorCode} - {ex.Message}");
                return new ApiResult
                {
                    IsSuccess = true,
                    MetaData = new MetaData
                    {
                        Message = ex.Message,
                        statusCode = AppStatusCode.NotFound
                    }
                }; // 400
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Error: {ex.Message}");
                return new ApiResult
                {
                    IsSuccess = true,
                    MetaData = new MetaData
                    {
                        Message = ex.Message,
                        statusCode = AppStatusCode.ServerError
                    }
                }; // 500
            }
        }
    }
}
