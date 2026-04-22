namespace CalorieCounter.Api.Endpoints
{
    public static class UserController
    {
        public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/users", GetUsers);

            return app;
        }

        private static Task<IResult> GetUsers()
        {
            // Implementation to get users
            return Task.FromResult(Results.Ok(new[] { new { Id = 1, Name = "John Doe" } }));
        }

        private static Task<IResult> CreateUser()
        {
            // Implementation to create a user
            return Task.FromResult(Results.Created("/users/1", new { Id = 1, Name = "John Doe" }));
        }
    }
}
