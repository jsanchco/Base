namespace SGI.Domain.Models
{
    public class TransactionResult<T>
    {
        public T Item { get; set; }
        public bool Result { get; set; }
    }
}
