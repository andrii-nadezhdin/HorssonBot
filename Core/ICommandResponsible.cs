using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Commands;
using Microsoft.Bot.Connector;

namespace Core
{
    public interface ICommandResponsible
    {
        CommandBase RegisterNext<T>() where T : CommandBase, new();
        Task<IList<string>> ExecuteAsync(Activity activity);
    }
}