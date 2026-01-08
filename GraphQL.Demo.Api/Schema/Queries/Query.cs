using Bogus;
using GraphQL.Demo.Api.DTOs;
using GraphQL.Demo.Api.Models;
using GraphQL.Demo.Api.Schema.Filters;
using GraphQL.Demo.Api.Services;
using GraphQL.Demo.Api.Services.Courses;
using HotChocolate.Data;
using System.Reflection.Metadata.Ecma335;

namespace GraphQL.Demo.Api.Schema.Queries
{
    public class Query
    {
        private readonly CoursesRepository _coursesRepository;

        public Query(CoursesRepository coursesRepository)
        {
            _coursesRepository = coursesRepository;
        }
       
        public async Task<IEnumerable<CourseType>> GetCourses()
        {
            try
            {
                IEnumerable<CourseDTO> courseDTOs = await _coursesRepository.GetAll();

                return courseDTOs.Select(c => new CourseType()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Subjects = c.Subjects,
                    InstructorId = c.InstructorId,
                });
            }
            catch (Exception)
            {

                throw;
            }
        }


        [UsePaging(IncludeTotalCount = true, DefaultPageSize = 5)]
        [UseFiltering(typeof(CourseFilterType))]
        public async Task<IQueryable<CourseType>> GetPaginatedCourses(SchoolDbContext schoolDbContext)
        {

            return schoolDbContext.Courses.Select(c => new CourseType()
            {
                Id = c.Id,
                Name = c.Name,
                Subjects = c.Subjects,
                InstructorId = c.InstructorId,
            });
        }




        //  [UseOffsetPaging(IncludeTotalCount = true, DefaultPageSize = 5)]
        public async Task<IQueryable<CourseType>> GetOffSetCourses()
        {
            var courseDTOs = await _coursesRepository.GetAll();

            return courseDTOs
                .Select(c => new CourseType
                {
                    Id = c.Id,
                    Name = c.Name,
                    Subjects = c.Subjects,
                    InstructorId = c.InstructorId
                })
                .AsQueryable();
        }


        public async Task<CourseType> GetCourseByIdAsync(Guid id)
        {
            CourseDTO courseDTO = await _coursesRepository.GetCourseById(id);
            return new CourseType()
            {
                Id = courseDTO.Id,
                Name = courseDTO.Name,
                Subjects = courseDTO.Subjects,
                InstructorId = courseDTO.InstructorId,
                //Instructor = new InstructorType()
                //{
                //    Id = courseDTO.Instructor.Id,
                //    FirstName = courseDTO.Instructor.FirstName,
                //    LastName = courseDTO.Instructor.LastName,
                //    Salary = courseDTO.Instructor.Salary
                //}
            };
        }

        [GraphQLDeprecated("This query is deprecated")]
        public string Insructions => "Welcome to GraphQL ";
    }
}
