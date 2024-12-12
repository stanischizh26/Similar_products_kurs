namespace Similar_products.Application.Dtos;

public class ProductForCreationDto 
{
	public string Name { get; set; }
	public string Characteristics { get; set; }
	public string Unit { get; set; }
	public string Photo { get; set; }
    public Guid ProductTypeId { get; set; }

}

