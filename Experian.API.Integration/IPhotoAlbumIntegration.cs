using Experian.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Experian.API.Integration
{
    /// <summary>
    /// Contract to get photos and albums from external services
    /// </summary>
    public interface IPhotoAlbumIntegration
    {
        /// <summary>
        /// Get all albums
        /// </summary>
        /// <returns>Albums</returns>
        Task<IEnumerable<Album>> GetAllAlbumsAsync();
        /// <summary>
        /// Get all photos
        /// </summary>
        /// <returns>Photos</returns>
        Task<IEnumerable<Photo>> GetAllPhotosAsync();
        /// <summary>
        /// Get albums for a user
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns>Albums</returns>
        Task<IEnumerable<Album>> GetAlbumsByUserAsync(int userId);
    }
}
