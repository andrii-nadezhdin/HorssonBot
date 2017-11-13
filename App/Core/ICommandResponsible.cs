using App.Core.Commands;
using Microsoft.Bot.Connector;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Core
{
    internal interface ICommandResponsible
    {
        CommandBase RegisterNext<T>() where T : CommandBase, new();
        Task<IList<string>> ExecuteAsync(Activity activity);
    }
}