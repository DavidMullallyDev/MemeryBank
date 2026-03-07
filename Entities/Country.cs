using System.ComponentModel.DataAnnotations;

namespace Entities
{
    /// <summary>
    /// Domain model for storing country details.
    /// </summary>
    public class Country
    {
        [Key]
        public Guid CountryId {  get; set; }
        [StringLength(50)]
        public string? CountryName { get; set; }
    }
}
