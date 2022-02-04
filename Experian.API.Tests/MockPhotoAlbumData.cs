using Experian.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Experian.API.Tests
{
    public static class MockPhotoAlbumData
    {
        public static List<Album> GetAlbumSetup()
        {
            return new List<Album>()
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
        }

        public static List<Photo> GetPhotoSetup()
        {
            return new List<Photo>()
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
            };
        }

        public static List<Album> GetAlbumWithPhotoSetup()
        {
            return new List<Album>()
            {
                new Album()
                {
                    Id = 1,
                    Title = "First",
                    UserId = 1,                    
                    Photos = new List<Photo>()
                    {
                        new Photo()
                        {
                            Id = 1,
                            AlbumId = 1,
                            ThumbnailUrl = "",
                            Title = "",
                            Url = ""
                        }
                    }
                },
                new Album()
                {
                    Id = 2,
                    Title = "Second",
                    UserId = 1,
                    Photos = new List<Photo>()
                    {
                        new Photo()
                        {
                            Id = 2,
                            AlbumId = 2
                        }
                    }
                },
                new Album()
                {
                    Id = 3,
                    Title = "Third",
                    UserId = 2,
                    Photos = new List<Photo>()
                    {
                        new Photo()
                        {
                            Id = 3,
                            AlbumId = 3
                        },
                        new Photo()
                        {
                            Id = 4,
                            AlbumId = 3
                        }
                    }
                }
            };
        }
    }
}
