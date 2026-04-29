using CalorieCounter.Application.Commands.UserCommands.CreateCommand;
using CalorieCounter.Application.Queries.UserQueries.GetInfoQuery;
using CalorieCounter.Domain.AggregatesModels;
using MediatR;

namespace CalorieCounter.Api.Endpoints
{
    public static class UsersEndpoints
    {
        public static IEndpointRouteBuilder MapUsersEndpoints(this IEndpointRouteBuilder app)
        {
            RouteGroupBuilder user = app.MapGroup("/user");
            user.MapGet("/{id}", GetUser);
            user.MapPost("/", CreateUser);

            return app;
        }

        private async static Task<IResult> GetUser(long id, IMediator mediator)
        {
            GetUserInfoQuery query = new(id);
            User result = await mediator.Send(query);

            // Implementation to get users
            return Results.Ok(result);
        }

        private async static Task<IResult> CreateUser(User user, IMediator mediator)
        {
            CreateUserCommand command = new (user.Name, user.Age, user.Gender, user.Weight, user.Height);
            User result = await mediator.Send(command);

            return Results.Created("/users/1", result);
        }
    }
}
