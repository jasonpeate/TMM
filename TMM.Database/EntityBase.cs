using System.ComponentModel.DataAnnotations.Schema;

namespace TMM.Database
{
    public abstract class EntityBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }
}
