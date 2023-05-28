using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace TMM.Database
{
    public class Customer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(20)] //TODO : Should be a lookup
        public string Title { get; set; }

        [Required]
        [StringLength(50)]
        public string Forename { get; set; }

        [Required]
        [StringLength(50)]
        public string SureName { get; set; }

        [Required]
        [StringLength(75)] //TODO : Regex Validation
        public string EmailAddress { get; set; }

        [Required]
        [StringLength(15)] //TODO : Regex Validation
        public string MobileNo { get; set; }

        public bool Active { get; set; }

        public ICollection<Address> Addresses { get; set; }
    }
}