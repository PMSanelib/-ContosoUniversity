using System;

namespace Core.Commands
{
	public interface ICommand
    {
        Guid? GetAggregateId(); 
	}
}