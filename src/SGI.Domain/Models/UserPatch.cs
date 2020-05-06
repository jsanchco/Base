namespace SGI.Domain.Models
{
    public class UserPatch
    {
        public int id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string birthdate { get; set; }
        public int roleId { get; set; }
    }
}
