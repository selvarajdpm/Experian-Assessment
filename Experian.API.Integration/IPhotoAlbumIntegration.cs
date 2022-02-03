using Experian.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Experian.API.Integration
{
    public interface IPhotoAlbumIntegration
    {
        Task<IEnumerable<Album>> GetAllAlbumsAsync();
        Task<IEnumerable<Photo>> GetAllPhotosAsync();
        Task<IEnumerable<Album>> GetAlbumsByUserAsync(int userId);
    }
}
