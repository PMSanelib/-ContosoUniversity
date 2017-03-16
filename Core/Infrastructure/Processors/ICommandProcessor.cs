using Core.Commands;
using Core.Dtos;

namespace Core.Infrastructure.Processors
{
    public interface ICommandProcessor<in TCommand> where TCommand : ICommand
    {
        ValidationResult Process(TCommand command);
    }
}
