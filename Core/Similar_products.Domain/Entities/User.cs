using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Similar_products.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        public string? FullName { get; set; }
        public string? UserName { get; set; }

        public string HashedPassword { get; set; }

        public string ?Role { get; set; }
    }
}
