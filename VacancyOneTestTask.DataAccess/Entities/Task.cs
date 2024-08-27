using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacancyOneTestTask.DataAccess.Entities
{
    public class Task
    {
        public long Id { get; set; }

        public DateTime Date { get; set; }

        public TicketStatus Status { get; set; }

        public List<AttachedFile> Files { get; set; }
    }
}