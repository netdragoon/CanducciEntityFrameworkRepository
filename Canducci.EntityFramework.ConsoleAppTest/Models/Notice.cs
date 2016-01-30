using System;

namespace Canducci.EntityFramework.ConsoleAppTest.Models
{
    public partial class Notice
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Texto { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<bool> Active { get; set; }
        public int TagId { get; set; }

        public virtual Tags Tags { get; set; }
    }
}
