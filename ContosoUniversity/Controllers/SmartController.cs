using System;
using System.Web.Mvc;
using ContosoUniversity.App_Start;
using Core.Commands;
using Core.Dtos;
using Core.Infrastructure;
using Core.Infrastructure.Processors;

namespace ContosoUniversity.Controllers
{
    public abstract class SmartController : Controller
    {
        protected ValidationResult ExecuteCommand<TCommand>(TCommand command) where TCommand : ICommand
        {
            var validationResult = new ValidationResult();
            try
            {
                validationResult = GetService<ICommandProcessor<TCommand>>().Process(command);
                GetService<ApplicationDbContext>().SaveChanges();
            }
            catch (Exception e)
            {
                validationResult.AddError("Internal", e.GetBaseException().Message);
            }

            return validationResult;
        }

        protected T GetService<T>()
        {
            var container = StructuremapMvc.StructureMapDependencyScope.Container;
            return container.GetInstance<T>();
        }
    }
}