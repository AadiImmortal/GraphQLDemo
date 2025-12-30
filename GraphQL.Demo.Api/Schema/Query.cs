using Bogus;
using System.Reflection.Metadata.Ecma335;

namespace GraphQL.Demo.Api.Schema
{
    public class Query
    {
        private readonly Faker<InstructorType> _instructorTypeFaker;
        private readonly Faker<StudentType> _studentTypeFaker;
        private readonly Faker<CourseType> _courseTypeFaker;


        public Query()
        {
            _instructorTypeFaker = new Faker<InstructorType>()
          .RuleFor(i => i.Id, f => Guid.NewGuid())
          .RuleFor(i => i.FirstName, f => f.Name.FirstName())
          .RuleFor(i => i.LastName, f => f.Name.LastName())
          .RuleFor(i => i.Salary, f => f.Random.Double(0, 10000));

            _studentTypeFaker = new Faker<StudentType>()
           .RuleFor(s => s.Id, f => Guid.NewGuid())
           .RuleFor(s => s.FirstName, f => f.Name.FirstName())
           .RuleFor(s => s.LastName, f => f.Name.LastName())
           .RuleFor(s => s.GPA, f => f.Random.Double(1, 4));

            _courseTypeFaker = new Faker<CourseType>()
         .RuleFor(c => c.Id, f => Guid.NewGuid())
         .RuleFor(c => c.Name, f => f.Name.JobTitle())
         .RuleFor(c => c.Subjects, f => f.PickRandom<Subject>())
         .RuleFor(c => c.Instructor, f => _instructorTypeFaker.Generate())
         .RuleFor(c => c.Students, f => _studentTypeFaker.Generate(3));
        }

        public IEnumerable<CourseType> GetCources()
        {
            return _courseTypeFaker.Generate(5);
        }

        public async Task<CourseType> GetCourseByIdAsync(Guid id)
        {
            await Task.Delay(1000);
            CourseType course = _courseTypeFaker.Generate();
            course.Id = id;
            return course;
        }

        [GraphQLDeprecated("This query is deprecated")]
        public string Insructions => "Welcome to GraphQL ";
    }
}
