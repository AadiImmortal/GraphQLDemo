using GraphQL.Demo.Api.Schema.Queries;
using GraphQL.Demo.Api.Schema.Subscriptions;
using HotChocolate.Subscriptions;
using System.Threading.Tasks;

namespace GraphQL.Demo.Api.Schema.Mutations
{
    public class Mutation
    {
        private readonly List<CourseResult> _courcses;
        public Mutation()
        {
            _courcses = new List<CourseResult>();
        }
        public async Task<CourseResult> CreateCourse(CourseInputType courseInput, [Service] ITopicEventSender topicEventSender)
        {
            CourseResult course = new CourseResult()
            {
                Id = Guid.NewGuid(),
                Name = courseInput.Name,
                Subject = courseInput.Subject,
                InstructorId = courseInput.InstructorId,

            };
            _courcses.Add(course);
            await topicEventSender.SendAsync(nameof(Subscription.CourseCreated), course);
            return course;
        }


        public async Task<CourseResult> UpdateCourse(Guid id, CourseInputType courseInput, [Service] ITopicEventSender topicEventSender)
        {
            CourseResult course = _courcses.FirstOrDefault(c => c.Id == id);
            if (course == null)
            {
                throw new GraphQLException(new Error("404 Course Not Found", "404"));
            }
            course.Name = courseInput.Name;
            course.Subject = courseInput.Subject;
            course.Id = id;
            course.InstructorId = courseInput.InstructorId;


            //Custome Topic 

            string updateCourseTopic = $"{course.Id}_{nameof(Subscription.CourseUpdated)}";
            await topicEventSender.SendAsync(updateCourseTopic, course);

            return course;
        }

        public bool DeleteCourse(Guid id)
        {
            return _courcses.RemoveAll(c => c.Id == id) >= 1;
        }
    }
}
