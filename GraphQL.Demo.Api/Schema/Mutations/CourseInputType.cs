using GraphQL.Demo.Api.Models;
using GraphQL.Demo.Api.Schema.Queries;

namespace GraphQL.Demo.Api.Schema.Mutations
{
    public class CourseInputType
    {
        //public Guid Id { get; set; }
        public string Name { get; set; }
        public Subject Subject { get; set; }
        public Guid InstructorId { get; set; }
    }
}
