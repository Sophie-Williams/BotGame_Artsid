using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace BotGame
{
    class Program
    {
        static TelegramBotClient botClient;
        
        static void Main(string[] args)
        {
            try
            {
                botClient = new TelegramBotClient("765143820:AAFkn-60-4T-yf_ibmAhAsLvi8PnvJ3db_w");
                var bot = botClient.GetMeAsync().Result;
                Console.WriteLine(bot.Username);
                
                botClient.StartReceiving();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.Read();
        }
    }
}
