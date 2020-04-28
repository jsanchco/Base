namespace SGI.API.Models
{
    #region Using

    using SGI.Domain.Models;

    #endregion

    public class Session
    {
        public UserViewModel user { get; set; }
        public string token { get; set; }
    }
}
