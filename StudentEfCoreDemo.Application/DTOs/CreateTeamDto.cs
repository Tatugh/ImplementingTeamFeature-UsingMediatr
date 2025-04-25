using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentEfCoreDemo.Application.DTOs
{
    public class CreateTeamDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SportType { get; set; }
        public DateTime FoundedDate { get; set; }
        public string HomeStadium { get; set; }
        public int MaxRosterSize { get; set; }
    }
}
