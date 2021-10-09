using System;
using System.Threading.Tasks;
using FluentResults;

namespace Application.Configuration.CommandsAndQueries.Commands.Interfaces
{
    public interface ICreateConfigurationCommand
    {
        Task<Result<Guid>> ExecuteAsync();
    }
}