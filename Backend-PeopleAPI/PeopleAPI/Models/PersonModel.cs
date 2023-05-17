using System.ComponentModel.DataAnnotations;

namespace PeopleAPI.Models
{
    public class PersonModel
    {
        [Key, Required]
        public Guid Id { get; set; }

        [Required, MinLength(2)]
        public string FirstName { get; set; } = string.Empty;

        [Required, MinLength(1)]
        public string LastName { get; set; } = string.Empty;

        [Range(18, 60)]
        public int Age { get; set; }
    }
}
