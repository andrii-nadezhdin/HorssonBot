using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;

namespace Core.Commands
{
    internal class SetPostCountCommand : CommandBase
    {
        protected override string ExactCommand => null;
        protected override string[] StartWithCommands => new[] { "SET POST COUNT" };
        protected override string[] Responses { get => null; }

        protected override Task<List<string>> ExecuteInternalAsync(Activity activity)
        {
            var match = Regex.Match(activity.Text, @"SET POST COUNT (\d+)", RegexOptions.IgnoreCase);
            if (!match.Success)
            {
                return Task.FromResult(new List<string> { @"Неправильно, вот паттерн -- SET POST COUNT (\d+)" });
            }
            var value = match.Groups[1]?.Value;
            int.TryParse(value, out int postCount);
            if (postCount < -500 || postCount > 500)
            {
                return Task.FromResult(new List<string>
                {
                    { "No" },
                    { "░░░░░░░░░░░░░▄▄▄█▀▀▀▀▀▀▀█▄▄░░░░░░░░░░░░" },
                    { "░░░░░░░░▄▄█▀▀░░░░░░░░░░░░░░▀▀▄░░░░░░░░░" },
                    { "░░░░░░▄█▀░░░░░░░▄▄▄▄░▀░░░░░░░░▀▄░░░░░░░" },
                    { "░░░░░██░░░░░░▀▀▀▀░░░░░░░░░░░░░░░▀▄░░░░░" },
                    { "░░░▄███▄▄░░░░░▀▀▀░░░░░░░░░░░░░░░░░█▄░░░" },
                    { "░░██▀▀░▄░░░░░░░░░░░░░░░░░░░░░░░░░░░▀▄░░" },
                    { "░▄█▀░░░░░░░░░░░░░░░░░░░░░░▄░▄░░░░░░░█▄░" },
                    { "▄█▀▄░░░░░▄█░░░░░░░░░░░░░░███░░░░░░░░░█░" },
                    { "███░░░░░░▀█░░░░░░░░░░░░░▄█░▀▄░░░░░░░░▀▄" },
                    { "██▀██░░▄░░█░░▄▄▄▄▄▄████▀▀░░░░░░░░░░░░░█" },
                    { "██▄▀█▄█████░░█████▀░░▀█░░░░░░░░░░░░░░░█" },
                    { "███░▀▀████░░██▀██▄░▀░▄▄▄▀▀▀░░░░░░░░░░█░" },
                    { "▀███▄░░███░░░▀████████▀░░░░░░░░░░░░░░█░" },
                    { "░▀████▄█▀█░░█▄█████▄░░░░░░░░░░░░░░░░█░░" },
                    { "░░██████▀█▄█▀░▄▄░▀▄▀▄░▄▄█▀░░░░░░░░░█▀░░" },
                    { "░░░▀█████░▀█░░░░░░█▄▀░▀░░░░░░░░░░░█▀░░░" },
                    { "░░░░░▀██▄█▄░▄░░░▄░░▀░░▀░░░░░░░░░▄█░░░░░" },
                    { "░░░░░░░▀█▄▀░█░░░░█▄█░░░░░░░░░░▄█▀█░░░░░" },
                    { "░░░░░░░░░▀▀██░░░░░░▀░░░░░░▄▄▀▀░░░█░░░░░" }
                });
            }
            if (postCount < 1 || postCount > 5)
            {
                return Task.FromResult(new List<string> { { "Это перебор :)" } });
            }
            else
            {
                BotState.Instance.Set(activity.Conversation?.Id, Constants.PostCountParameter, postCount);
                return Task.FromResult(new List<string> { "Ок" });
            }
        }
    }
}