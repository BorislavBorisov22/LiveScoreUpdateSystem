using LiveScoreUpdateSystem.Data.Models.Abstraction;
using LiveScoreUpdateSystem.Data.Models.Contracts;
using LiveScoreUpdateSystem.Data.Repositories.Contracts;
using LiveScoreUpdateSystem.Services.Data.Abstraction;
using LiveScoreUpdateSystem.Services.Data.Tests.Abstraction.DataServiceTests.Fakes;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveScoreUpdateSystem.Services.Data.Tests.Abstraction.DataServiceTests
{
    [TestFixture]
    public class Countructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenPassedDataSetIsNull()
        {
            // arrange & act & assert
            Assert.Throws<ArgumentNullException>(() => new DataService<DataModel>(null));
        }

        [Test]
        public void NotThrow_WhenPassedDateSetIsNotNull()
        {
            // arrange
            var repository = new Mock<IEfRepository<DataModel>>();

            // act & assert
            Assert.DoesNotThrow(() => new DataService<DataModel>(repository.Object));
        }

        [Test]
        public void SetDatePropertyCorrectly_WhenPassedDateSetIsValid()
        {
            // arrange
            var repository = new Mock<IEfRepository<DataModel>>();

            // act
            var dataService = new FakeDateService<DataModel>(repository.Object);

            // assert
            Assert.AreSame(repository.Object, dataService.DataTest);
        }
    }
}
