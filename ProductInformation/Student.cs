namespace ProductInformation
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Student
    {
        public int StudentId { get; set; }

        public string Name { get; set; }

        public string Class { get; set; }

        public int TeacherId { get; set; }

        public virtual Teacher Teacher { get; set; }
    }
}
