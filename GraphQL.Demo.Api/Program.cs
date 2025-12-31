using GraphQL.Demo.Api.Schema.Mutations;
using GraphQL.Demo.Api.Schema.Queries;
using GraphQL.Demo.Api.Schema.Subscriptions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGraphQLServer()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>()
                .AddSubscriptionType<Subscription>()
                .AddInMemorySubscriptions();



var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.UseWebSockets();
app.MapGraphQL();

app.Run();
