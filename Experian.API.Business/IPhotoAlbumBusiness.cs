using Experian.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Experian.API.Business
{
    public interface IPhotoAlbumBusiness
    {
        Task<IEnumerable<Album>> GetAllPhotoAlbumsAsync();

        Task<IEnumerable<Album>> GetPhotoAlbumsByUserAsync(int userId);
    }
}
