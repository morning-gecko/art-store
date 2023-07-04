using System;
using api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace api.Repositories
{
    public interface IArtworkItemRepository
    {
        Task<IEnumerable<ArtworkItem>> GetAllArtworks();
        Task<ArtworkItem?> GetArtworkItem(long id);
        Task<ArtworkItem> UpsertArtwork(ArtworkItem artworkItem);
        Task DeleteArtwork(long id);
    }
}

