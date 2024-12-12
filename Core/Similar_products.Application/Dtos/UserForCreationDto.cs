using System.ComponentModel.DataAnnotations;

namespace Similar_products.Application.Dtos
{
    public class UserForCreationDto
    {
        public Guid Id { get; set; }

        public string? FullName { get; set; }
        public string? UserName { get; set; }

        public string HashedPassword { get; set; }

        public string? Role { get; set; }
    }
}
