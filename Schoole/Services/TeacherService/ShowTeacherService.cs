using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Services.TeacherService
{
    public interface IShowTeacherService
    {
        Output Execute(string nCode);
    }

    public class ShowTeacherService : IShowTeacherService
    {
        private readonly ITeacherRepository _teacherRepository;

        public ShowTeacherService(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }


        public Output Execute(string nCode)
        {

            var existing = _teacherRepository.GetTeacherByNCode(nCode);
            var output = new Output();
            if (existing == null)
            {
                output.Success = false;
                output.Message = "No teacher with this national code was found.";
                return output;
            }
            else
            {
                output.Success = true;
                output.Message = $"Teacher successfully found.\nName: {existing.FullName}\nNational code: {existing.NCode}\nExpertise: {existing.Expertise}";
                return output;
            }
        }
    }


}
