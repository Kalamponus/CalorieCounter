using CalorieCounter.Api.Mapping;
using CalorieCounter.Application.Commands.UserCommands.CreateCommand;
using CalorieCounter.Application.Queries.UserQueries.GetInfoQuery;
using CalorieCounter.Domain.AggregatesModels;
using Contracts.Requests;
using Contracts.Responses;
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
            User? user = await mediator.Send(query);

            if (user is null)
                return Results.NotFound();

            UserResponse result = user.MapToResponse();

            return Results.Ok(result);
        }

        private async static Task<IResult> CreateUser(CreateUserRequest request, IMediator mediator)
        {
            User user = request.MapToUser();
            CreateUserCommand command = new (user.Name, user.Age, user.Gender, user.Weight, user.Height);
            User? resultUser = await mediator.Send(command);

            if (resultUser is null)
                return Results.Conflict();

            UserResponse result = resultUser.MapToResponse();

            return Results.Created($"{BaseAddress}/{result.Id}", result);
        }
    }
}
