using GraphQL.Demo.Api.Models;
using GraphQL.Demo.Api.Schema.Queries;

namespace GraphQL.Demo.Api.DTOs
{
    public class CouseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public IEnumerable<StudentDTO> Students { get; set; }
        public Subject Subjects { get; set; }
        public Guid InstructorId { get; set; }
        public InstructorDTO Instructor { get; set; }
    }
}
