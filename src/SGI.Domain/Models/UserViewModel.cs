namespace SGI.Domain.Models
{
    #region Using

    using System;

    #endregion

    public class UserViewModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string fullName => $"{name} {surname}";
        public string surname { get; set; }
        public string birthdate { get; set; }

        public int roleId { get; set; }
        public string roleName { get; set; }
    }
}
