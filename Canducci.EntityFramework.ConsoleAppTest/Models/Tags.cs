using System.Collections.Generic;
namespace Canducci.EntityFramework.ConsoleAppTest.Models
{
    public partial class Tags
    {
        
        public Tags()
        {
            Notice = new HashSet<Notice>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        
        public virtual ICollection<Notice> Notice { get; set; }
    }
}
