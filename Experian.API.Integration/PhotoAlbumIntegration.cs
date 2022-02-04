using Experian.API.Entities;
using Experian.API.Helpers;
using Experian.API.Helpers.RestClient;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Experian.API.Integration
{
    public class PhotoAlbumIntegration : IPhotoAlbumIntegration
    {
        private readonly IRestClientHelper _restClientHelper;
        private readonly Configuration _configuration;

        public Task<T> GetAsync<T>(object p)
        {
            throw new NotImplementedException();
        }

        public PhotoAlbumIntegration(IRestClientHelper restClientHelper, IOptions<Configuration> configOptions)
        {
            this._restClientHelper = restClientHelper;
            this._configuration = configOptions.Value;
        }

        /// <summary>
        /// Get all albums from external service
        /// </summary>
        /// <returns>Albums</returns>
        public async Task<IEnumerable<Album>> GetAllAlbumsAsync()
        {
            return await _restClientHelper.GetAsync<List<Album>>(_configuration.Url.PhotoAlbumBase + _configuration.Url.Album);
        }

        /// <summary>
        /// Get albums from external service for a user
        /// </summary>
        /// <returns>Albums</returns>
        public async Task<IEnumerable<Album>> GetAlbumsByUserAsync(int userId)
        {
            return await _restClientHelper.GetAsync<List<Album>>(_configuration.Url.PhotoAlbumBase + string.Format(_configuration.Url.AlbumByUser, userId));
        }

        /// <summary>
        /// Get all photos from external service
        /// </summary>
        /// <returns>photos</returns>
        public async Task<IEnumerable<Photo>> GetAllPhotosAsync()
        {
            return await _restClientHelper.GetAsync<List<Photo>>(_configuration.Url.PhotoAlbumBase + _configuration.Url.Photo);
        }
    }
}
