namespace Fct.Infrastructure.Persistence.Entities
{
    public class Purchase
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
        public virtual Customer User { get; set; }
    }
}
