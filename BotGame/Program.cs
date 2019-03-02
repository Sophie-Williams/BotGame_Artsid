using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;

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
                botClient.StartReceiving();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.Read();
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
                            await botClient.SendPhotoAsync(e.Message.Chat.Id, "https://demiart.ru/forum/uploads11/post-2484384-1353421971.jpg");
                            await botClient.SendTextMessageAsync(e.Message.Chat.Id, "Его имя Хаус");
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
