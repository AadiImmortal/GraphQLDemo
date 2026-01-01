using GraphQL.Demo.Api.Models;

namespace GraphQL.Demo.Api.Schema.Queries
{
    public class CourseType
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public IEnumerable<StudentType> Students { get; set; }
        public Subject Subjects { get; set; }

        [GraphQLNonNullType]
        public InstructorType Instructor { get; set; }

        public string Duration { get; set; }


        public string Description()
        {
            return $"{Name}: This is course Name";
        }
    }


   
}
