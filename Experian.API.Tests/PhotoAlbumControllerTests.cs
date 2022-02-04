using Microsoft.VisualStudio.TestTools.UnitTesting;
using Experian.API.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Experian.API.Business;
using Microsoft.Extensions.Logging;
using Experian.API.Entities;

namespace Experian.API.Tests
{
    [TestClass()]
    public class PhotoAlbumControllerTests
    {
        private MockRepository _mockRepository;

        private Mock<IPhotoAlbumBusiness> _mockPhotoAlbumBusiness;
        private Mock<ILogger<PhotoAlbumController>> _mocklogger;

        [TestInitialize]
        public void TestInitialize()
        {
            this._mockRepository = new MockRepository(MockBehavior.Default);
            this._mockPhotoAlbumBusiness = this._mockRepository.Create<IPhotoAlbumBusiness>();
            this._mocklogger = this._mockRepository.Create<ILogger<PhotoAlbumController>>();
        }

        private PhotoAlbumController CreatePhotoAlbumController()
        {
            return new PhotoAlbumController(this._mocklogger.Object, this._mockPhotoAlbumBusiness.Object);
        }

        [TestMethod()]
        public async Task GetTestAsync()
        {
            _mockPhotoAlbumBusiness.Setup(x => x.GetAllPhotoAlbumsAsync()).ReturnsAsync(MockPhotoAlbumData.GetAlbumWithPhotoSetup());
            var photoAlbumController = CreatePhotoAlbumController();
            var response = await photoAlbumController.Get();
            var album = (IEnumerable<Album>)((Microsoft.AspNetCore.Mvc.ObjectResult)response.Result).Value;
            Assert.IsTrue(album.Count() == 3 && album.SelectMany(a => a.Photos).ToList().Count() == 4);
        }

        [TestMethod()]
        public async Task GetByUserTestAsync()
        {
            int userId = 2;
            _mockPhotoAlbumBusiness.Setup(x => x.GetPhotoAlbumsByUserAsync(userId)).ReturnsAsync(MockPhotoAlbumData.GetAlbumWithPhotoSetup().Where(x => x.UserId == userId));
            var photoAlbumController = CreatePhotoAlbumController();
            var response = await photoAlbumController.Get(userId);
            var album = (IEnumerable<Album>)((Microsoft.AspNetCore.Mvc.ObjectResult)response.Result).Value;
            Assert.IsTrue(album != null && album.Count() == 1);
        }
    }
}