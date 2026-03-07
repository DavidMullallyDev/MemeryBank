using System.ComponentModel.DataAnnotations;

namespace Entities
{
    /// <summary>
    /// Person domain model class
    /// </summary>
    public class Person
    {
        [Key]
        public Guid Id { get; set; }
        [StringLength(50)] //nvarchar(40)
        public string? Name { get; set; }
        [StringLength(50)]
        public string? Email { get; set; }
        public DateTime? Dob { get; set; }
        [StringLength(10)]
        public string? Gender { get; set; }
        public Guid? CountryId { get; set; }
        [StringLength(500)]
        public string? Address { get; set; }
        public bool RecieveNewsLetters { get; set; }
    }
}
