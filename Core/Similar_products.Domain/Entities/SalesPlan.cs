namespace Similar_products.Domain.Entities;

public class SalesPlan 
{
	public Guid Id { get; set; }
	public Guid EnterpriseId { get; set; }
	public Enterprise Enterprise { get; set; }
	public Guid ProductId { get; set; }
	public Product Product { get; set; }
	public int PlannedSales { get; set; }
	public int ActualSales { get; set; }
	public int Quarter { get; set; }
	public int Year { get; set; }
}
