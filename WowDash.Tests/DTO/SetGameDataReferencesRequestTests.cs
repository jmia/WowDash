using FluentAssertions;
using NUnit.Framework;
using WowDash.ApplicationCore.DTO;
using WowDash.UnitTests.Common;

namespace WowDash.UnitTests.DTO
{
    [TestFixture]
    public class SetGameDataReferencesRequestTests
    {
        [Test]
        public void GivenANullListOfReferences_CreatesEmptyListOfReferencesOnRequest()
        {
            // Arrange, Act
            var dto = new SetGameDataReferencesRequest(TestConstants.AllOnesGuid, null);

            // Assert
            dto.GameDataReferenceItems.Should().NotBeNull();
            dto.GameDataReferenceItems.Should().BeEmpty();
        }
    }
}
