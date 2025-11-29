using Schoole.Interfaces;
using Schoole.Models;

namespace Schoole.Services.StudentsService
{
    public interface IShowStudentService
    {
        Output Execute(string nCode);
    }
    public class ShowStudentService : IShowStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public ShowStudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }


        public Output Execute(string nCode)
        {

            var existing = _studentRepository.GetStudentByNcode(nCode);
            var output = new Output();
            if (existing == null)
            {
                output.Success = false;
                output.Message = "No student with this national code was found.";
                return output;
            }
            else
            {
                output.Success = true;
                output.Message = $"Student successfully found.\nName: {existing.FullName}\nNational code: {existing.NCode}\nBirthDate: {existing.BirthDate.ToShortDateString()}";
                return output;

            }
        }
    }
}
