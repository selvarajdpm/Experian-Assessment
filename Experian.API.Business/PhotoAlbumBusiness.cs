using Experian.API.Entities;
using Experian.API.Integration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Experian.API.Business
{
    public class PhotoAlbumBusiness : IPhotoAlbumBusiness
    {
        private readonly IPhotoAlbumIntegration _photoAlbumIntegration;

        public PhotoAlbumBusiness(IPhotoAlbumIntegration photoAlbumIntegration)
        {
            this._photoAlbumIntegration = photoAlbumIntegration;
        }
        public async Task<IEnumerable<Album>> GetAllPhotoAlbumsAsync()
        {
            var albums = _photoAlbumIntegration.GetAllAlbumsAsync();
            return await getPhotosForAlbum(albums);
        }

        public async Task<IEnumerable<Album>> GetPhotoAlbumsByUserAsync(int userId)
        {
            var albums = _photoAlbumIntegration.GetAlbumsByUserAsync(userId);
            return await getPhotosForAlbum(albums);
        }

        private async Task<IEnumerable<Album>> getPhotosForAlbum(Task<IEnumerable<Album>> albums)
        {
            var photos = _photoAlbumIntegration.GetAllPhotosAsync();
            await Task.WhenAll(albums, photos);

            var photoAlbums = from a in albums.Result
                              join p in photos.Result on a.Id equals p.AlbumId into matchedPhotos
                              select new Album
                              {
                                  Id = a.Id,
                                  Title = a.Title,
                                  UserId = a.UserId,
                                  Photos = matchedPhotos
                              };
            return photoAlbums.ToList();
        }
    }
}
