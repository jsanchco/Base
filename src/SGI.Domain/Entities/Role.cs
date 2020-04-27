namespace SGI.Domain.Entities
{
    #region Using

    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    #endregion

    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<User> Users { get; set; } = new HashSet<User>();
    }
}
