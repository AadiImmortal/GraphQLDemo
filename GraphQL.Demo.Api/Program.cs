using GraphQL.Demo.Api.DataLoaders;
using GraphQL.Demo.Api.Schema.Mutations;
using GraphQL.Demo.Api.Schema.Queries;
using GraphQL.Demo.Api.Schema.Subscriptions;
using GraphQL.Demo.Api.Services;
using GraphQL.Demo.Api.Services.Courses;
using GraphQL.Demo.Api.Services.Instructors;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


#region[GraphQL Configure]
builder.Services.AddGraphQLServer()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>()
                .AddSubscriptionType<Subscription>()
                .AddInMemorySubscriptions()
                .AddFiltering();

#endregion

#region[Connection String]
var connectionString = builder.Configuration.GetConnectionString("default");
builder.Services.AddPooledDbContextFactory<SchoolDbContext>(options => options.UseSqlite(connectionString).LogTo(Console.WriteLine));
#endregion

#region[Register services]
builder.Services.AddScoped<CoursesRepository>();
builder.Services.AddScoped<InstructorsRepository>();
builder.Services.AddScoped<InstructorDataLoader>();
#endregion


var app = builder.Build();

#region[Run migrations before starting]
using (var scope = app.Services.CreateScope())
{
    var factory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<SchoolDbContext>>();
    using var context = factory.CreateDbContext();
    context.Database.Migrate();
}
#endregion

app.MapGet("/", () => "Hello World!");
app.UseWebSockets();
app.MapGraphQL("/graphql"); // explicitly map endpoint

app.Run();
