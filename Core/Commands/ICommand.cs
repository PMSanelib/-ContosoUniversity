using System;

namespace Core.Commands
{
	public interface ICommand
    {
        Guid Id { get; set; }
        Guid? GetAggregateId(); 
        void Validate();
	}
}