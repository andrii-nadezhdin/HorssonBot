using App.Core.Base;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Core.Commands
{
    internal abstract class CommandBase
    {
        public IRandomizer Randomizer { get; set; }

        protected abstract string ExactCommand { get; }
        protected abstract string[] Responses { get; }

        protected virtual int ChanseToResponseInPercent { get; } = Constants.HundredPercent;

        protected virtual string ActivityType => ActivityTypes.Message;
        protected virtual string[] StartWithCommands { get; }
        protected virtual string[] ContainsCommands { get; }

        private CommandBase _nextCommand;

        protected abstract Task<IList<string>> ExecuteInternalAsync(Activity activity);

        public CommandBase RegisterNext<T>() where T: CommandBase, new()
        {
            _nextCommand = new T();
            _nextCommand.Randomizer = Randomizer;
            return _nextCommand;
        }

        public async Task<IList<string>> ExecuteAsync(Activity activity)
        {
            var messageActivity = activity.AsMessageActivity();
            if (messageActivity == null)
                return await ProcessActivityByType(activity);
            var command = messageActivity.Text;
            if (VerifyCommandStrict(command) || VerifyCommandStartsWith(command) || VerifyCommandContains(command))
                return await PrepareExecutingAsync(activity);
            if (_nextCommand != null)
                return await _nextCommand.ExecuteAsync(activity);
            return null;
        }

        private async Task<IList<string>> ProcessActivityByType(Activity activity)
        {
            if (activity.Type == ActivityType)
                return await PrepareExecutingAsync(activity);
            if (_nextCommand != null)
                return await _nextCommand.ExecuteAsync(activity);
            return null;
        }

        private async Task<IList<string>> PrepareExecutingAsync(Activity activity)
        {
            var responses = new List<string>();
            if (Responses != null && Responses.Any() && Randomizer.CheckIsLucky(ChanseToResponseInPercent))
                responses.Add(Responses[Randomizer.GetRandom(Responses.Length)]);
            var internalResponse = await ExecuteInternalAsync(activity);
            if (internalResponse != null)
                responses.AddRange(internalResponse);
            return responses;
        }

        private bool VerifyCommandStrict(string command)
        {
            return command.Equals(ExactCommand, StringComparison.InvariantCultureIgnoreCase) ||
                command.Equals($"@{Constants.BotName} {ExactCommand}", StringComparison.InvariantCultureIgnoreCase);
        }

        private bool VerifyCommandStartsWith(string command)
        {
            return StartWithCommands != null && StartWithCommands.Any(c =>
                 command.StartsWith(c, StringComparison.InvariantCultureIgnoreCase) ||
                 command.StartsWith($"@{Constants.BotName} {c}", StringComparison.InvariantCultureIgnoreCase));
        }

        private bool VerifyCommandContains(string command)
        {
            return ContainsCommands != null && ContainsCommands.Any(c => command.IndexOf(c, StringComparison.OrdinalIgnoreCase) >= 0);
        }
    }
}