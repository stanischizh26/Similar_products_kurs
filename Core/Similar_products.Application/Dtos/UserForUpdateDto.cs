namespace Similar_products.Application.Dtos
{
    public class UserForUpdateDto
    {
        public Guid Id { get; set; }

        public string? FullName { get; set; }
        public string? UserName { get; set; }


        public string? Role { get; set; }
    }
}
