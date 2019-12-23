using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;

namespace NewHost.Bots
{
    public class HorssonBot : ActivityHandler
    {
        private readonly ICommandResponsible _commandResponsible;

        public HorssonBot(ICommandResponsible commandResponsible)
        {
            _commandResponsible = commandResponsible;
        }

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            if (membersAdded.Any(m => m.Id != turnContext.Activity.Recipient.Id))
            {
                await turnContext.SendActivityAsync(MessageFactory.Text("Игого!"), cancellationToken);
            }
        }

        protected override async Task OnMembersRemovedAsync(IList<ChannelAccount> membersRemoved, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            if (membersRemoved.Any(m => m.Id != turnContext.Activity.Recipient.Id))
            {
                await turnContext.SendActivityAsync(MessageFactory.Text("Игого... Игого..."), cancellationToken);
            }
        }

        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            var responses = await _commandResponsible.ExecuteAsync(turnContext.Activity);
            foreach (var response in responses ?? Enumerable.Empty<string>())
            {
                await turnContext.SendActivityAsync(
                    Uri.IsWellFormedUriString(response, UriKind.Absolute) ? 
                        MessageFactory.ContentUrl(response, MediaTypeNames.Image.Jpeg) :
                        MessageFactory.Text(response), cancellationToken);
            }
        }
    }
}
