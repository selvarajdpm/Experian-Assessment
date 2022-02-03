using Experian.API.Business;
using Experian.API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Experian.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PhotoAlbumController : ControllerBase
    {
        private readonly IPhotoAlbumBusiness _photoAlbumBusiness;


        private readonly ILogger<PhotoAlbumController> _logger;

        public PhotoAlbumController(ILogger<PhotoAlbumController> logger, IPhotoAlbumBusiness photoAlbumBusiness)
        {
            _logger = logger;
            _photoAlbumBusiness = photoAlbumBusiness;
        }
        /// <summary>
        /// Get All Albums with Photos
        /// </summary>
        /// <returns>Album</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Album>>> Get()
        {
            _logger.LogInformation("Get Album Started");
            var albums = await _photoAlbumBusiness.GetAllPhotoAlbumsAsync();
            _logger.LogInformation("Get Album Completed");
            return Ok(albums);
        }

        /// <summary>
        /// Get Albums with Photos By User ID
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>Album</returns>
        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<Album>>> Get(int userId)
        {
            _logger.LogInformation("Get Album By User Started");
            var albums = await _photoAlbumBusiness.GetPhotoAlbumsByUserAsync(userId);
            _logger.LogInformation("Get Album By User Started");
            return Ok(albums);
        }
    }
}
