using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Commands;
using Microsoft.Bot.Schema;

namespace Core
{
    public interface ICommandResponsible
    {
        CommandBase RegisterNext<T>() where T : CommandBase, new();
        Task<IList<string>> ExecuteAsync(IMessageActivity activity);
    }
}