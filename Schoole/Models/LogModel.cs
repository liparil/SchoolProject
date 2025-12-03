using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schoole.Models
{
    public class LogModel
    {
        public int Id { get; set; }
        public string LogType { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
