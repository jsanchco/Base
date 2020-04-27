namespace SGI.Domain.Helpers
{
    #region Using

    using System.Collections.Generic;

    #endregion

    public class QueryResult <T>
    {
        public List <T> Items { get; set; }
        public long Count { get; set; }
    }
}
