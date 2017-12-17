using System;
using System.Threading.Tasks;
using Core;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Core.Base;

namespace Host.Dialogs
{
	[Serializable]
	public class RootDialog : IDialog<object>
	{
		public Task StartAsync(IDialogContext context)
		{
			context.Wait(MessageReceivedAsync);

			return Task.CompletedTask;
		}

		private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
		{
			try
			{
				var activity = (Activity)await result;
				var responses = await Resolver.Resolve<ICommandResponsible>().ExecuteAsync(activity);
				if (responses != null)
				{
					foreach (var response in responses)
					{
						await context.PostAsync(response);
					}
				} else
				{
					await context.PostAsync("Мне нечего ответить");
				}

			}
			catch
			{
				await context.PostAsync("Увы и ах, что-то пошло не так...");
                throw;
			}
			context.Wait(MessageReceivedAsync);
		}
	}
}