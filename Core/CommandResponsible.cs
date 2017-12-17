using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Base;
using Core.Commands;
using Microsoft.Bot.Connector;

namespace Core
{
    internal class CommandResponsible : ICommandResponsible
    {
        private CommandBase _firstCommand;
        protected IRandomizer Randomizer { get; set; }

        public CommandResponsible(IRandomizer randomizer)
        {
            Randomizer = randomizer;
        }

        public async Task<IList<string>> ExecuteAsync(Activity activity)
        {
            if (_firstCommand == null)
            {
                return null;
            }
            return await _firstCommand.ExecuteAsync(activity);
        }

        public CommandBase RegisterNext<T>() where T : CommandBase, new()
        {
            _firstCommand = new T();
            _firstCommand.Randomizer = Randomizer;
            return _firstCommand;
        }
    }
}