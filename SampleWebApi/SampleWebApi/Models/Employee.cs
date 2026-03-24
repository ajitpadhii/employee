using System.ComponentModel.DataAnnotations;

namespace SampleWebApi.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Position is required")]
        public string? Position { get; set; }
    }
}
