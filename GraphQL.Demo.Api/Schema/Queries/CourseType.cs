using GraphQL.Demo.Api.DataLoaders;
using GraphQL.Demo.Api.DTOs;
using GraphQL.Demo.Api.Models;
using GraphQL.Demo.Api.Services.Instructors;

namespace GraphQL.Demo.Api.Schema.Queries
{
    public class CourseType
    {
        public Guid Id { get; set; }

        [IsProjected(false)]
        public string Name { get; set; } = "";
        public IEnumerable<StudentType> Students { get; set; }
        public Subject Subjects { get; set; }

        [IsProjected(true)]
        public Guid InstructorId { get; set; }

        [GraphQLNonNullType]
        public async Task<InstructorType> Instructor([Service] InstructorDataLoader instructorDataLoader)
        {
            InstructorDTO instructorDTO = await instructorDataLoader.LoadAsync(InstructorId, CancellationToken.None);
            return new InstructorType
            {
                Id = instructorDTO.Id,
                FirstName = instructorDTO.FirstName,
                LastName = instructorDTO.LastName,
                Salary = instructorDTO.Salary,
            };
        }

        public string Duration { get; set; }


        public string Description()
        {
            return $"{Name}: This is course Name";
        }
    }



}
