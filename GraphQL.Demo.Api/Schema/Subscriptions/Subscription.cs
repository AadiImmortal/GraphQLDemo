using GraphQL.Demo.Api.Schema.Mutations;
using GraphQL.Demo.Api.Schema.Queries;
using HotChocolate.Execution;
using HotChocolate.Subscriptions;

namespace GraphQL.Demo.Api.Schema.Subscriptions
{
    public class Subscription
    {
        [Subscribe]
        public CourseResult CourseCreated([EventMessage] CourseResult course) => course;

        [SubscribeAndResolve]
        public ValueTask<ISourceStream<CourseResult>> CourseUpdated(Guid courseId, [Service] ITopicEventReceiver topicEventReceiver)
        {
            string topicName = $"{courseId}_{nameof(Subscription.CourseUpdated)}";
            return topicEventReceiver.SubscribeAsync<CourseResult>(topicName);
        }
    }
}
