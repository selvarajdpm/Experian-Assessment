using Microsoft.VisualStudio.TestTools.UnitTesting;
using Experian.API.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Experian.API.Integration;
using Experian.API.Entities;

namespace Experian.API.Business.Tests
{
    [TestClass()]
    public class PhotoAlbumBusinessTests
    {
        private MockRepository _mockRepository;
        private Mock<IPhotoAlbumIntegration> _mockPhotoAlbumIntegration;

        [TestInitialize]
        public void TestInitialize()
        {
            this._mockRepository = new MockRepository(MockBehavior.Strict);
            this._mockPhotoAlbumIntegration = this._mockRepository.Create<IPhotoAlbumIntegration>();
        }

        private PhotoAlbumBusiness CreatePhotoAlbumBusiness()
        {
            return new PhotoAlbumBusiness(this._mockPhotoAlbumIntegration.Object);
        }

        [TestMethod()]
        public async Task GetPhotoAlbumsByUserAsyncTestAsync()
        {
            var photoAlbumBusiness = CreatePhotoAlbumBusiness();
            var albumSetup = new List<Album>()
            {
                new Album()
                {
                    Id = 1,
                    Title = "First",
                    UserId = 1
                },
                new Album()
                {
                    Id = 2,
                    Title = "Second",
                    UserId = 1
                },
                new Album()
                {
                    Id = 3,
                    Title = "Third",
                    UserId = 2
                }
            };
            int userId = 2;
            _mockPhotoAlbumIntegration.Setup(x => x.GetAlbumsByUserAsync(It.IsAny<int>())).ReturnsAsync(albumSetup.Where(x => x.UserId == userId));
            _mockPhotoAlbumIntegration.Setup(x => x.GetAllPhotosAsync()).ReturnsAsync(new List<Photo>()
            {
                new Photo()
                {
                    Id = 1,
                    AlbumId = 1
                },
                new Photo()
                {
                    Id = 2,
                    AlbumId = 2
                },
                new Photo()
                {
                    Id = 3,
                    AlbumId = 3
                },
                new Photo()
                {
                    Id = 4,
                    AlbumId = 2
                }
            });

            var album = await photoAlbumBusiness.GetPhotoAlbumsByUserAsync(userId);

            Assert.IsTrue(album.Count() == 1 && album.All(x => x.UserId == userId) && album.SelectMany(a => a.Photos).ToList().Count() == 1);
        }

        [TestMethod()]
        public async Task GetAllPhotoAlbumsAsyncTestAsync()
        {
            var photoAlbumBusiness = CreatePhotoAlbumBusiness();
            var albumSetup = new List<Album>()
            {
                new Album()
                {
                    Id = 1,
                    Title = "First",
                    UserId = 1
                },
                new Album()
                {
                    Id = 2,
                    Title = "Second",
                    UserId = 1
                },
                new Album()
                {
                    Id = 3,
                    Title = "Third",
                    UserId = 2
                }
            };
            _mockPhotoAlbumIntegration.Setup(x => x.GetAllAlbumsAsync()).ReturnsAsync(albumSetup);
            _mockPhotoAlbumIntegration.Setup(x => x.GetAllPhotosAsync()).ReturnsAsync(new List<Photo>()
            {
                new Photo()
                {
                    Id = 1,
                    AlbumId = 1
                },
                new Photo()
                {
                    Id = 2,
                    AlbumId = 2
                },
                new Photo()
                {
                    Id = 3,
                    AlbumId = 3
                },
                new Photo()
                {
                    Id = 4,
                    AlbumId = 2
                }
            });

            var album = await photoAlbumBusiness.GetAllPhotoAlbumsAsync();

            Assert.IsTrue(album.Count() == 3 && album.SelectMany(a => a.Photos).ToList().Count() == 4);
        }
    }
}