using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Bot.Schema;

namespace Core.Commands
{
    internal class HelpCommand : CommandBase
    {
        protected override string ExactCommand => "HELP";
        protected override string[] StartWithCommands => new[] { "/?", "-?" };
        protected override string[] ContainsCommands => new[] { "HELP", "ПОМОЩ", "ПОМОГ", "ЧТО ТУТ ДЕЛАТЬ", "ЧЯВО", "ЧЕ КАК" };
        protected override string[] Responses { get => null; }

        protected override Task<List<string>> ExecuteInternalAsync(IMessageActivity activity)
        {
            var responses = new List<string>
            {
                "Бот лошадка V:",
                "**HELP** -- справка",
                "**POST** -- картинки от Невседома",
                "**PROFESSION** -- фото от профессионалов",
                "**ЧТО УГОДНО** -- картинки от Коржика"
            };
            return Task.FromResult(responses);
        }
    }
}