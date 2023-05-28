using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TMM.Database
{
    public class Address
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [Required]
        [StringLength(80)] 
        public string AddressLine1 { get; set; }

        [StringLength(80)]
        public string? AddressLine2 { get; set; }

        [Required]
        [StringLength(50)] //TODO : should be a lookup
        public string Town { get; set; }

        [StringLength(50)] //TODO : should be a lookup
        public string? County { get; set; }

        [Required]
        [StringLength(10)] //TODO : should be a lookup
        public string Postcode { get; set; }

        [StringLength(10)] //TODO : should be a lookup
        public string? Country { get; set; }

        public bool MainAddress { get; set; }
    }
}
