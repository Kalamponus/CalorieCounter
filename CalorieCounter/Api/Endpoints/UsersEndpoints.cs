using CalorieCounter.Api.Mapping;
using CalorieCounter.Application.Commands.UserCommands;
using CalorieCounter.Application.Queries.UserQueries.GetInfoQuery;
using CalorieCounter.Domain.AggregatesModels;
using CalorieCounter.Domain.AggregatesModels.UserAggregate;
using Contracts.Requests;
using Contracts.Responses;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

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
            user.MapPut("/{id}", UpdateUserGeneralData);
            user.MapPut("/{id}/name", ChangeUserName);

            return app;
        }

        private async static Task<Results<Ok<UserResponse>, NotFound, ProblemHttpResult>> GetUser(Guid id, IMediator mediator)
        {
            GetUserInfoQuery query = new(id);
            ErrorOr<User> commandResult = await mediator.Send(query);

            if (commandResult.IsError)
            {
                if (commandResult.FirstError.Type == ErrorType.NotFound)
                    return TypedResults.NotFound();
                else
                    return TypedResults.Problem(commandResult.FirstError.Description);
            }

            UserResponse response = commandResult.Value.MapToResponse();

            return TypedResults.Ok(response);
        }

        private async static Task<Results<Created<UserResponse>, BadRequest<IEnumerable<string>>, Conflict<IEnumerable<string>>>> CreateUser(CreateUserRequest request, IMediator mediator)
        {
            CreateUserCommand command = new(request.Name, request.Age, (Gender)request.Gender, request.Weight, request.Height);
            ErrorOr<User> commandResult = await mediator.Send(command);

            if (commandResult.IsError)
            {
                IEnumerable<string> errorDescriptions = commandResult.Errors.Select(e => e.Description);

                if (commandResult.FirstError.Type == ErrorType.Validation)
                    return TypedResults.BadRequest(errorDescriptions);
                else if (commandResult.FirstError.Type == ErrorType.Conflict)
                    return TypedResults.Conflict(errorDescriptions);
            }

            UserResponse response = commandResult.Value.MapToResponse();

            return TypedResults.Created($"{BaseAddress}/{response.Id}", response);
        }

        private async static Task<Results<Ok<UserResponse>, NotFound<IEnumerable<string>>, BadRequest<IEnumerable<string>>, ProblemHttpResult>> ChangeUserName(ChangeUserNameRequest request, IMediator mediator)
        {
            ChangeUserNameCommand command = new(request.Id, request.NewName);
            ErrorOr<User> commandResult = await mediator.Send(command);

            if (commandResult.IsError)
            {
                IEnumerable<string> errorDescriptions = commandResult.Errors.Select(e => e.Description);

                switch (commandResult.FirstError.Type)
                {
                    case ErrorType.NotFound:
                        return TypedResults.NotFound(errorDescriptions);
                    case ErrorType.Validation:
                        return TypedResults.BadRequest(errorDescriptions);
                    default:
                        return TypedResults.Problem(commandResult.FirstError.Description);
                }
            }

            UserResponse response = commandResult.Value.MapToResponse();

            return TypedResults.Ok(response);
        }

        private async static Task<Results<Ok<UserResponse>, NotFound<IEnumerable<string>>, BadRequest<IEnumerable<string>>, ProblemHttpResult>> UpdateUserGeneralData(UpdateUserGeneralData request, IMediator mediator)
        {
            UpdateUserGeneralDataCommand command = new(request.Id, request.Name, request.Age, (Gender)request.Gender, request.Weight, request.Height);
            ErrorOr<User> commandResult = await mediator.Send(command);

            if (commandResult.IsError)
            {
                IEnumerable<string> errorDescriptions = commandResult.Errors.Select(e => e.Description);

                switch (commandResult.FirstError.Type)
                {
                    case ErrorType.NotFound:
                        return TypedResults.NotFound(errorDescriptions);
                    case ErrorType.Validation:
                        return TypedResults.BadRequest(errorDescriptions);
                    default:
                        return TypedResults.Problem(commandResult.FirstError.Description);
                }
            }

            UserResponse response = commandResult.Value.MapToResponse();

            return TypedResults.Ok(response);
        }
    }
}
