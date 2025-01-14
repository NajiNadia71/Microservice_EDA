namespace Advertisement_Company.DTO
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
	

	public class AdDTO
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public int ProductionId { get; set; }
		public string ProductionName { get; set; } = string.Empty;
		public string CreateDate { get; set; } = string.Empty;
		public string Text { get; set; }

		
	}
}