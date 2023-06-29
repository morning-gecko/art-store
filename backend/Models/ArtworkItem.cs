using System;

namespace api.Models
{
	public class ArtworkItem
	{
		public long Id { get; set; }
		public string? Name { get; set; }
		public bool IsComplete { get; set; }
		public float Price { get; set; }
	}
}

