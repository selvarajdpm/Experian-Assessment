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
using Experian.API.Tests;

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
            int userId = 2;
            _mockPhotoAlbumIntegration.Setup(x => x.GetAlbumsByUserAsync(It.IsAny<int>())).ReturnsAsync(MockPhotoAlbumData.GetAlbumSetup().Where(x => x.UserId == userId));
            _mockPhotoAlbumIntegration.Setup(x => x.GetAllPhotosAsync()).ReturnsAsync(MockPhotoAlbumData.GetPhotoSetup());

            var album = await photoAlbumBusiness.GetPhotoAlbumsByUserAsync(userId);

            Assert.IsTrue(album.Count() == 1 && album.All(x => x.UserId == userId) && album.SelectMany(a => a.Photos).ToList().Count() == 1);
        }        

        [TestMethod()]
        public async Task GetAllPhotoAlbumsAsyncTestAsync()
        {
            var photoAlbumBusiness = CreatePhotoAlbumBusiness();
            _mockPhotoAlbumIntegration.Setup(x => x.GetAllAlbumsAsync()).ReturnsAsync(MockPhotoAlbumData.GetAlbumSetup());
            _mockPhotoAlbumIntegration.Setup(x => x.GetAllPhotosAsync()).ReturnsAsync(MockPhotoAlbumData.GetPhotoSetup());

            var album = await photoAlbumBusiness.GetAllPhotoAlbumsAsync();

            Assert.IsTrue(album.Count() == 3 && album.SelectMany(a => a.Photos).ToList().Count() == 4);
        }
    }
}