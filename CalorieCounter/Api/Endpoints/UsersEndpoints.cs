using CalorieCounter.Api.Mapping;
using CalorieCounter.Application.Commands.UserCommands.CreateCommand;
using CalorieCounter.Application.Commands.UserCommands.UpdateGeneralDataCommand;
using CalorieCounter.Application.Queries.UserQueries.GetInfoQuery;
using CalorieCounter.Domain.AggregatesModels;
using CalorieCounter.Domain.AggregatesModels.UserAggregate;
using Contracts.Requests;
using Contracts.Responses;
using ErrorOr;
using MediatR;

namespace CalorieCounter.Api.Endpoints
{
    public static class UsersEndpoints
    {
        private const string BaseAddress = "/api/user";

        public static IEndpointRouteBuilder MapUsersEndpoints(this IEndpointRouteBuilder app)
        {
            RouteGroupBuilder user = app.MapGroup(BaseAddress);
            user.MapGet("/{id}", GetUser);
            user.MapPost("/", CreateUser);

            return app;
        }

        private async static Task<IResult> GetUser(Guid id, IMediator mediator)
        {
            GetUserInfoQuery query = new(id);
            ErrorOr<User> commandResult = await mediator.Send(query);

            if (commandResult.IsError)
            {
                if (commandResult.FirstError.Type == ErrorType.NotFound)
                    return Results.NotFound();
                else
                    return Results.Problem(commandResult.FirstError.Description);
            }

            UserResponse result = commandResult.Value.MapToResponse();

            return Results.Ok(result);
        }

        private async static Task<IResult> CreateUser(CreateUserRequest request, IMediator mediator)
        {
            CreateUserCommand command = new(request.Name, request.Age, (Gender)request.Gender, request.Weight, request.Height);
            ErrorOr<User> commandResult = await mediator.Send(command);

            if (commandResult.IsError)
            {
                IEnumerable<string> errorDescriptions = commandResult.Errors.Select(e => e.Description);

                if (commandResult.FirstError.Type == ErrorType.Validation)
                    return Results.BadRequest(errorDescriptions);
                else if (commandResult.FirstError.Type == ErrorType.Conflict)
                    return Results.Conflict(errorDescriptions);
            }

            UserResponse result = commandResult.Value.MapToResponse();

            return Results.Created($"{BaseAddress}/{result.Id}", result);
        }

        private async static Task<IResult> UpdateUserGeneralData(UpdateUserGeneralData request, IMediator mediator)
        {
            UpdateUserGeneralDataCommand command = new(request.Id, request.Name, request.Age, (Gender)request.Gender, request.Weight, request.Height);
            ErrorOr<Updated> commandResult = await mediator.Send(command);

            if (commandResult.IsError)
            {
                IEnumerable<string> errorDescriptions = commandResult.Errors.Select(e => e.Description);

                switch (commandResult.FirstError.Type)
                {
                    case ErrorType.NotFound:
                        return Results.NotFound(errorDescriptions);
                    case ErrorType.Validation:
                        return Results.BadRequest(errorDescriptions);
                    default:
                        return Results.Problem(commandResult.FirstError.Description);
                }
            }

        }
    }
}
