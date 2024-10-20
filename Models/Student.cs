using System.ComponentModel.DataAnnotations;

namespace WebApplicationMVC.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public int Stander { get; set; }
        [Required]

        public string FatherName { get; set; }
        


    }
}


