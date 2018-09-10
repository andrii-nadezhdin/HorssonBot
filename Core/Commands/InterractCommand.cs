using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;

namespace Core.Commands
{
    internal class InterractCommand : CommandBase
    {
        protected override string ExactCommand => null;
        protected override string[] Responses { get => null; }

        protected override string ActivityType => ActivityTypes.ConversationUpdate;

        protected override Task<List<string>> ExecuteInternalAsync(Activity activity)
        {
            var botId = activity.Recipient.Id;
            if (activity.MembersAdded != null && activity.MembersAdded.Any(m => m.Id != botId))
            {
                return Task.FromResult(new List<string> { "Игого!" });
            }
            if (activity.MembersRemoved != null && activity.MembersRemoved.Any(m => m.Id != botId))
            {
                return Task.FromResult(new List<string> { "Игого... Игого..." });
            }
            return Task.FromResult<List<string>>(null);
        }
    }
}