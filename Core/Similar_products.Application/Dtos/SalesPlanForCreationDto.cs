namespace Similar_products.Application.Dtos;

public class SalesPlanForCreationDto 
{
	public Guid EnterpriseId { get; set; }
	public Guid ProductId { get; set; }
	public int PlannedSales { get; set; }
	public int ActualSales { get; set; }
	public int Quarter { get; set; }
	public int Year { get; set; }
}

