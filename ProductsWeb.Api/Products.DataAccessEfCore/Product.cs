using System;
using System.Collections.Generic;

namespace Products.DataAccessEfCore
{
    public partial class Product
    {
        public Product()
        {
            ProductVersions = new HashSet<ProductVersion>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public virtual ICollection<ProductVersion> ProductVersions { get; set; }
    }
}
