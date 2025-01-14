namespace Production_Company.DTO
{
	public class ProductionDTO
	{
		public int Id { get; set; }
		public int Count { get; set; }
		public string Title { get; set; }
		public int ProductionTypeId { get; set; }
		public string CreateDate { get; set; }
		public string Comment { get; set; }
		public string ProductTypeName { get; set; } = string.Empty;  // Joining related data
	}
	

	public class ProductionTypeDto
	{
		public int ProductTypeId { get; set; }
		public string ProductTypeName { get; set; } = string.Empty;
	}
}