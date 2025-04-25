using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentEfCoreDemo.Domain.Entities
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string SportType { get; set; } = null!;
        public DateTime FoundedDate { get; set; }
        public string HomeStadium { get; set; }
        public int MaxRosterSize { get; set; }
    }
}
