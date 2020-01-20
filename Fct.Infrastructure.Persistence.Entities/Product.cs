using System;
using System.Collections.Generic;
using System.Text;

namespace Fct.Infrastructure.Persistence.Entities
{
    public class Product
    {
        public Product()
        {
            Purchase = new HashSet<Purchase>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public decimal? Price { get; set; }
        public virtual ICollection<Purchase> Purchase { get; set; }
    }
}
