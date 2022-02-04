using Microsoft.VisualStudio.TestTools.UnitTesting;
using Experian.API.Integration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Experian.API.Helpers.RestClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Experian.API.Helpers;
using Experian.API.Entities;
using Experian.API.Tests;

namespace Experian.API.Integration.Tests
{
    [TestClass()]
    public class PhotoAlbumIntegrationTests
    {
        private MockRepository _mockRepository;

        private Mock<IRestClientHelper> _mockRestClientHelper;
        private Mock<IOptions<Configuration>> _mockConfiguration;

        [TestInitialize]
        public void TestInitialize()
        {
            this._mockRepository = new MockRepository(MockBehavior.Default);
            this._mockRestClientHelper = this._mockRepository.Create<IRestClientHelper>();
            this._mockConfiguration = this._mockRepository.Create<IOptions<Configuration>>();
        }

        private PhotoAlbumIntegration CreatePhotoAlbumIntegration()
        {
            return new PhotoAlbumIntegration(
                this._mockRestClientHelper.Object, this._mockConfiguration.Object);
        }

        [TestMethod()]
        public async Task GetAllAlbumsAsyncTestAsync()
        {
            _mockRestClientHelper.Setup(x => x.GetAsync<List<Album>>(It.IsAny<string>()))
                .ReturnsAsync(MockPhotoAlbumData.GetAlbumSetup());
            _mockConfiguration.Setup(x => x.Value).Returns(new Configuration()
            {
                Url = new Url()
                {
                    PhotoAlbumBase = "https://baseurl/",
                    AlbumByUser = "users/{0}/albums",
                    Album = "albums",
                    Photo = "photos"
                }
            });
            var photoAlbumIntegration = CreatePhotoAlbumIntegration();
            var album = await photoAlbumIntegration.GetAllAlbumsAsync();

            Assert.IsTrue(album != null && album.Count() == 3);
        }

        [TestMethod()]
        public async Task GetAlbumsByUserAsyncTestAsync()
        {
            int userId = 2;
            _mockRestClientHelper.Setup(x => x.GetAsync<List<Album>>(It.IsAny<string>()))
                .ReturnsAsync(MockPhotoAlbumData.GetAlbumSetup().Where(x => x.UserId == userId).ToList());
            _mockConfiguration.Setup(x => x.Value).Returns(new Configuration()
            {
                Url = new Url()
                {
                    PhotoAlbumBase = "https://baseurl/",
                    AlbumByUser = "users/{0}/albums",
                    Album = "albums",
                    Photo = "photos"
                }
            });
            var photoAlbumIntegration = CreatePhotoAlbumIntegration();
            var album = await photoAlbumIntegration.GetAlbumsByUserAsync(userId);

            Assert.IsTrue(album != null && album.Count() == 1);
        }

        [TestMethod()]
        public async Task GetAllPhotosAsyncTestAsync()
        {
            _mockRestClientHelper.Setup(x => x.GetAsync<List<Photo>>(It.IsAny<string>()))
                .ReturnsAsync(MockPhotoAlbumData.GetPhotoSetup());
            _mockConfiguration.Setup(x => x.Value).Returns(new Configuration()
            {
                Url = new Url()
                {
                    PhotoAlbumBase = "https://baseurl/",
                    AlbumByUser = "users/{0}/albums",
                    Album = "albums",
                    Photo = "photos"
                }
            });
            var photoAlbumIntegration = CreatePhotoAlbumIntegration();
            var album = await photoAlbumIntegration.GetAllPhotosAsync();

            Assert.IsTrue(album != null && album.Count() == 4);
        }
    }
}