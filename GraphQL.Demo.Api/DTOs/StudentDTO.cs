namespace GraphQL.Demo.Api.DTOs
{
    public class StudentDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double GPA { get; set; }
        public IEnumerable<CouseDTO> Courses { get; set; }
    }
}
