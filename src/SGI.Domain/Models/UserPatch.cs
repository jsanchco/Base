namespace SGI.Domain.Models
{
    #region Using

    using System;

    #endregion

    public class UserPatch
    {
        public int id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string username { get; set; }
        public DateTime birthdate { get; set; }
        public double salary { get; set; }
        public int roleId { get; set; }
    }
}
