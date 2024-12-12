namespace Similar_products.Application.Dtos;

public class ProductionPlanForCreationDto 
{
	public Guid EnterpriseId { get; set; }
	public Guid ProductId { get; set; }
	public int PlannedVolume { get; set; }
	public int ActualVolume { get; set; }
	public int Quarter { get; set; }
	public int Year { get; set; }
}

