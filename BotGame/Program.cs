using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace BotGame
{
    class Program
    {
        static TelegramBotClient botClient;
        readonly static string connectionString = @"Data Source=10.2.2.212;Initial Catalog=Weather forecast_DB;Persist Security Info=True;User ID=student;Pooling=False";
        static void Main(string[] args)
        {
            try
            {
                botClient = new TelegramBotClient("765143820:AAFkn-60-4T-yf_ibmAhAsLvi8PnvJ3db_w");
                var bot = botClient.GetMeAsync().Result;
                Console.WriteLine(bot.Username);
                botClient.OnMessage += getMessage;
                botClient.OnCallbackQuery += GetQuare;
                botClient.StartReceiving();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.Read();
        }

        private static async Task deletLastMsg(Chat chatId, int msgId)
        {
            try
            {
                await botClient.DeleteMessageAsync(chatId, msgId);
            }
            catch(ApiRequestException e)
            {
                Console.WriteLine("Упс...");
            }
            catch (Exception e)
            {
                Console.WriteLine("Упс...");
            }
        }

        private static async void GetQuare(object sender, CallbackQueryEventArgs e)
        {
            await deletLastMsg(e.CallbackQuery.Message.Chat, e.CallbackQuery.Message.MessageId);

            switch (e.CallbackQuery.Data.ToLower())
            {
                case "/bonfire":
                    {
                        List<InlineKeyboardButton> btns = new List<InlineKeyboardButton>();
                        btns.Add(new InlineKeyboardButton() { CallbackData = "/market", Text = "Рынок" });
                        btns.Add(new InlineKeyboardButton() { CallbackData = "/house", Text = "Дом" });

                        var klava = new InlineKeyboardMarkup(btns);
                        await botClient.SendPhotoAsync(e.CallbackQuery.Message.Chat.Id, "https://cs8.pikabu.ru/post_img/big/2016/06/19/10/146635589611057362.jpg", "Это основное место. Хочешь перейти на рынок или пойти домой?", replyMarkup: klava);

                        //await botClient.SendPhotoAsync(e.CallbackQuery.Message.Chat.Id, "https://cs8.pikabu.ru/post_img/big/2016/06/19/10/146635589611057362.jpg", "Это основное место");
                        //await botClient.SendTextMessageAsync(e.CallbackQuery.Message.Chat.Id, "Хочешь перейти на рынок или пойти домой?");

                        //await GetRinokPage(e.CallbackQuery.Message.Chat);
                        //await GetDomPage(e.CallbackQuery.Message.Chat);
                        break;
                    }

                case "/market":
                    {
                        await botClient.SendPhotoAsync(e.CallbackQuery.Message.Chat.Id, "https://cs4.pikabu.ru/post_img/2016/06/19/10/1466355878118082698.jpg", "Это рынок");
                        //await botClient.SendPhotoAsync(e.CallbackQuery.Message.Chat.Id, "https://cs4.pikabu.ru/post_img/2016/06/19/10/1466355878118082698.jpg", "Это рынок");
                        break;
                    }

                case "/house":
                    {
                        await botClient.SendPhotoAsync(e.CallbackQuery.Message.Chat.Id, "http://www.gamer.ru/system/attached_images/images/000/343/227/original/snapshot20101128130437_20101128_1197020013.jpg");
                        await botClient.SendPhotoAsync(e.CallbackQuery.Message.Chat.Id, "http://www.silenthillmemories.net/allison_road/arts/pics/allison_road_art_2015.10.09_02_lakeside_cabin_exterior.jpg", "Да это твой дом))) XD");


                        //await botClient.SendPhotoAsync(e.CallbackQuery.Message.Chat.Id, "http://www.gamer.ru/system/attached_images/images/000/343/227/original/snapshot20101128130437_20101128_1197020013.jpg");
                        //await botClient.SendPhotoAsync(e.CallbackQuery.Message.Chat.Id, "http://www.silenthillmemories.net/allison_road/arts/pics/allison_road_art_2015.10.09_02_lakeside_cabin_exterior.jpg", "Да это твой дом))) XD");
                        break;
                    }
            }
        }

        private static async Task GetMainGamePage(Message userMsg)
        {
            await deletLastMsg(userMsg.Chat, userMsg.MessageId);

            InlineKeyboardButton b = new InlineKeyboardButton();
            b.Text = "Костер";
            b.CallbackData = "/bonfire";

            var keybords = new InlineKeyboardMarkup(b);
            await botClient.SendPhotoAsync(userMsg.Chat.Id, "https://cs8.pikabu.ru/post_img/big/2016/06/19/10/146635589611057362.jpg", replyMarkup: keybords);
        }

        private static async Task GetRinokPage(Chat userChat)
        {
            

            InlineKeyboardButton b = new InlineKeyboardButton();
            b.Text = "Рынок";
            b.CallbackData = "/market";

            var keybords = new InlineKeyboardMarkup(b);
            await botClient.SendTextMessageAsync(userChat.Id, "-", replyMarkup: keybords);
        }

        private static async Task GetDomPage(Chat userChat)
        {
            

            InlineKeyboardButton b = new InlineKeyboardButton();
            b.Text = "Дом";
            b.CallbackData = "/house";

            var keybords = new InlineKeyboardMarkup(b);
            await botClient.SendTextMessageAsync(userChat.Id, "-", replyMarkup: keybords);
        }

        private static async void getMessage(object sender, MessageEventArgs e)
        {
            if (e.Message.Text != null)
            {
                switch (e.Message.Text.ToLower())
                {
                    case "/start":
                        {
                            await botClient.SendTextMessageAsync(e.Message.Chat.Id, "Привет. Это игра дешевая версия игры которую мы заслужили)");
                            await botClient.SendTextMessageAsync(e.Message.Chat.Id, "Я предложил бы выбрать персонажа, но он один) Сейчас покажу");
                            await botClient.SendTextMessageAsync(e.Message.Chat.Id, "Его имя Хаус");
                            await botClient.SendPhotoAsync(e.Message.Chat.Id, "https://demiart.ru/forum/uploads11/post-2484384-1353421971.jpg");
                            await GetMainGamePage(e.Message);
                            break;
                        }
                    default:
                        await botClient.SendTextMessageAsync(e.Message.Chat.Id, e.Message.Text);
                        break;
                }
            }
            else
                await botClient.SendPhotoAsync(e.Message.Chat.Id, "https://images.techhive.com/images/article/2016/12/error-100700406-large.jpg", $"??Type '{e.Message.Type.ToString()}' is not supporting!");
        }

    }
}
