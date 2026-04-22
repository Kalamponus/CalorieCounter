using CalorieCounter.Domain;
using CalorieCounter.Domain.AggregatesModels;

namespace CalorieCounter.Api.Endpoints
{
    public static class UserEndpoints
    {
        public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder app)
        {
            RouteGroupBuilder user = app.MapGroup("/user");
            user.MapGet("/", GetUser);

            return app;
        }

        private static Task<IResult> GetUser()
        {
            User dummyUser = new ("Aaa", 18, Gender.Female, 185, 67);
            dummyUser.SetTargetWeight(dummyUser.Weight);

            // Implementation to get users
            return Task.FromResult(Results.Ok(dummyUser));
        }

        private static Task<IResult> CreateUser()
        {
            // Implementation to create a user
            return Task.FromResult(Results.Created("/users/1", new { Id = 1, Name = "John Doe" }));
        }
    }
}
