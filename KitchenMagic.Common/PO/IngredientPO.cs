using System;
using KitchenMagic.Common.DTO;

namespace KitchenMagic.Common.PO {
	public class IngredientPO : BasePO
	{
		public Guid Id { get; set; }
		public int Count { get; set; }
		public Unit Unit { get; set; }
		public string Name { get; set; }
	}
}