using GraphQL.Demo.Api.Schema.Mutations;
using GraphQL.Demo.Api.Schema.Queries;
using GraphQL.Demo.Api.Schema.Subscriptions;
using GraphQL.Demo.Api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddGraphQLServer()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>()
                .AddSubscriptionType<Subscription>()
                .AddInMemorySubscriptions();

var connectionString = builder.Configuration.GetConnectionString("default");

builder.Services.AddPooledDbContextFactory<SchoolDbContext>(options => options.UseSqlite(connectionString));

var host = builder.Build(); using var scope = host.Services.CreateScope();
var factory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<SchoolDbContext>>();
using var context = factory.CreateDbContext();
context.Database.Migrate();
host.Run();




var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.UseWebSockets();
app.MapGraphQL();
app.Run();
