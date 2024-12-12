namespace Similar_products.Application.Dtos;

public class ProductionPlanDto 
{
	public Guid Id { get; set; }
	public Guid EnterpriseId { get; set; }
	public EnterpriseDto Enterprise { get; set; }
	public Guid ProductId { get; set; }
	public ProductDto Product { get; set; }
	public int PlannedVolume { get; set; }
	public int ActualVolume { get; set; }
	public int Quarter { get; set; }
	public int Year { get; set; }
}

