using System.ComponentModel.DataAnnotations;

namespace TMM.Database
{
    public class Customer : EntityBase
    {
        [Required]
        [StringLength(20)] //TODO : Should be a lookup in its own sql table
        public string Title { get; set; }

        [Required]
        [StringLength(50)]
        public string Forename { get; set; }

        [Required]
        [StringLength(50)]
        public string SureName { get; set; }

        [Required]
        [StringLength(75)]
        [RegularExpression(Constants.EmailRegex)]
        public string EmailAddress { get; set; }

        [Required]
        [StringLength(15)] //TODO : Regex Validation
        public string MobileNo { get; set; }

        public bool Active { get; set; }

        public ICollection<Address> Addresses { get; set; }
    }
}