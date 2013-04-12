using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Moq;
using Xunit;

namespace NuGetGallery.Services
{
    public class ContentServiceFacts
    {
        public class TheConstructor
        {
            [Fact]
            public void GivenANullFileStorageService_ItShouldThrow()
            {
                ContractAssert.ThrowsArgNull(() => new ContentService(null), "fileStorage");
            }
        }

        public class TheGetContentItemMethod
        {
            [Fact]
            public void GivenANullOrEmptyName_ItShouldThrow()
            {
                ContractAssert.ThrowsArgNullOrEmpty(s => new TestableContentService().GetContentItem(s, bypassCache: true), "name");
            }

            [Fact]
            public void GivenAContentItemNameAndAnEmptyCache_ItShouldFetchThatItemFromFileStorage()
            {
                // Arrange
                var file = TestFileReference.Create("This is **a** test", "TestContentItem.md", DateTime.UtcNow);
                var stream = TestUtility.CreateTestStream("NuGet Rocks!");
                var contentService = new TestableContentService();
                contentService.MockFileStorage
                              .Setup(fs => fs.GetFileReferenceAsync(ContentService.ContentFolderName, "TestContentItem.md"))
                              .Returns(Task.FromResult<IFileReference>(file));

                // Act
                var actual = contentService.GetContentItem("TestContentItem.md");

                // Assert
                Assert.Equal("This is <strong>a</strong> test", actual.ToHtmlString());
            }
        }

        public class TestableContentService : ContentService
        {
            public Mock<IFileStorageService> MockFileStorage { get; private set; }

            public TestableContentService()
            {
                FileStorage = (MockFileStorage = new Mock<IFileStorageService>()).Object;
            }
        }
    }
}
