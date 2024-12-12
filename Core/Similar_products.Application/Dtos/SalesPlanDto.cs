namespace Similar_products.Application.Dtos;

public class SalesPlanDto 
{
	public Guid Id { get; set; }
	public Guid EnterpriseId { get; set; }
	public EnterpriseDto Enterprise { get; set; }
	public Guid ProductId { get; set; }
	public ProductDto Product { get; set; }
	public int PlannedSales { get; set; }
	public int ActualSales { get; set; }
	public int Quarter { get; set; }
	public int Year { get; set; }
}

