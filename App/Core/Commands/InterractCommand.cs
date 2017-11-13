using System.Collections.Generic;
using Microsoft.Bot.Connector;
using System.Linq;
using App.Core.Base;
using System.Threading.Tasks;

namespace App.Core.Commands
{
    internal class InterractCommand : CommandBase
    {
        protected override string ExactCommand => null;
        protected override string[] Responses { get => null; }

        protected override string ActivityType => ActivityTypes.ConversationUpdate;

        protected override async Task<IList<string>> ExecuteInternalAsync(Activity activity)
        {
            var botId = activity.Recipient.Id;
            if (activity.MembersAdded != null && activity.MembersAdded.Any(m => m.Id != botId))
            {
                return new List<string> { "Игого!" };
            }
            if (activity.MembersRemoved != null && activity.MembersRemoved.Any(m => m.Id != botId))
            {
                return new List<string> { "Игого... Игого..." };
            }
            return null;
        }
    }
}