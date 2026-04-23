using CalorieCounter.Domain;
using CalorieCounter.Domain.AggregatesModels;

namespace CalorieCounter.Api.Endpoints
{
    public static class UsersEndpoints
    {
        public static IEndpointRouteBuilder MapUsersEndpoints(this IEndpointRouteBuilder app)
        {
            RouteGroupBuilder user = app.MapGroup("/users");
            user.MapGet("/{id}", GetUser);

            return app;
        }

        private static Task<IResult> GetUser(long id)
        {
            User dummyUser = new ("Aaa", 18, Gender.Female, 185, 67);
            dummyUser.SetTargetWeight(dummyUser.Weight);

            // Implementation to get users
            return Task.FromResult(Results.Ok(dummyUser));
        }

        private static Task<IResult> CreateUser(User user)
        {
            // Implementation to create a user
            return Task.FromResult(Results.Created("/users/1", new { Id = 1, Name = "John Doe" }));
        }
    }
}
