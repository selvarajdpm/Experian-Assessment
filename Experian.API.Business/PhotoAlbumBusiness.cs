using Experian.API.Entities;
using Experian.API.Integration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Experian.API.Business
{
    /// <summary>
    /// Business logic to cater user needs of albums
    /// </summary>
    public class PhotoAlbumBusiness : IPhotoAlbumBusiness
    {
        private readonly IPhotoAlbumIntegration _photoAlbumIntegration;

        public PhotoAlbumBusiness(IPhotoAlbumIntegration photoAlbumIntegration)
        {
            this._photoAlbumIntegration = photoAlbumIntegration;
        }

        /// <summary>
        /// Gets all albums and their photos and return them together
        /// </summary>
        /// <returns>Album with photos</returns>
        public async Task<IEnumerable<Album>> GetAllPhotoAlbumsAsync()
        {
            var albums = _photoAlbumIntegration.GetAllAlbumsAsync();
            return await getPhotosForAlbum(albums);
        }

        /// <summary>
        /// Gets albums and their photos and return them together fpr a user
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>Album with photos</returns>
        public async Task<IEnumerable<Album>> GetPhotoAlbumsByUserAsync(int userId)
        {
            var albums = _photoAlbumIntegration.GetAlbumsByUserAsync(userId);
            return await getPhotosForAlbum(albums);
        }

        private async Task<IEnumerable<Album>> getPhotosForAlbum(Task<IEnumerable<Album>> albums)
        {
            var photos = _photoAlbumIntegration.GetAllPhotosAsync();

            // await both the responses
            await Task.WhenAll(albums, photos);

            // Match albums with their photos
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
