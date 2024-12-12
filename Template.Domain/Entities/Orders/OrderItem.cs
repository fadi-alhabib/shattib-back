﻿namespace Template.Domain.Entities.Orders
{
	public class OrderItem
	{
		public int OrderId { get; set; }
		public int ProductId { get; set; }
		public int Quantity { get; set; }
		public float Price { get; set; }
	}
}
