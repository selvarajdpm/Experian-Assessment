using Experian.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Experian.API.Business
{
    /// <summary>
    /// Specifies the contracts to get photos and albums and process them as per needs
    /// </summary>
    public interface IPhotoAlbumBusiness
    {
        /// <summary>
        /// Get all albums with their photos
        /// </summary>
        /// <returns>Album with photos</returns>
        Task<IEnumerable<Album>> GetAllPhotoAlbumsAsync();

        /// <summary>
        /// Get all albums with their photos for a user
        /// </summary>
        /// <returns>Album with photos</returns>
        Task<IEnumerable<Album>> GetPhotoAlbumsByUserAsync(int userId);
    }
}
