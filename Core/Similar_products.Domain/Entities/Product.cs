namespace Similar_products.Domain.Entities;

public class Product 
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public string Characteristics { get; set; }
	public string Unit { get; set; }
	public string Photo { get; set; }
	public Guid ProductTypeId { get; set; }
	public ProductType ProductType { get; set; }
}
