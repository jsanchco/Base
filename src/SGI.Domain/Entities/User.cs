namespace SGI.Domain.Entities
{
    #region Using

    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    #endregion

    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Surname { get; set; }
        public DateTime? Birthdate { get; set; }
        public string Username { get; set; }
        public double Salary { get; set; }
        public string Password { get; set; }
        
        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
    }
}
