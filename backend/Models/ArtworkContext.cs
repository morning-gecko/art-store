using System;
using Microsoft.EntityFrameworkCore;

namespace api.Models
{
	public class ArtworkContext : DbContext
	{
		public ArtworkContext(DbContextOptions<ArtworkContext> options)
			: base(options) { }

		public DbSet<ArtworkItem> ArtworkItems { get; set; } = null!;
	}
}

