using LiveScoreUpdateSystem.Data.Models.Abstraction;
using LiveScoreUpdateSystem.Data.Repositories.Contracts;
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
    public class GetAll_Should
    {
        [Test]
        public void SetDatePropertyCorrectly_WhenMethodIsCalled()
        {
            // arrange
            var repository = new Mock<IEfRepository<DataModel>>();
            repository.Setup(r => r.All).Returns(new List<DataModel>().AsQueryable());

            var dataService = new FakeDateService<DataModel>(repository.Object);

            // act
            dataService.GetAll();

            // assert
            repository.Verify(r => r.All, Times.Once);
        }

        [Test]
        public void ReturnTheResultFromRepositoriesAsIEnumerable_WhenMathodIsCalled()
        {
            // arrange
            var repository = new Mock<IEfRepository<DataModel>>();

            var modelsList = new List<DataModel>()
            {
                new DataModel(){IsDeleted = false},
                new DataModel(){IsDeleted = true},
            };

            repository.Setup(r => r.All).Returns(modelsList.AsQueryable());

            var dataService = new FakeDateService<DataModel>(repository.Object);

            // act
            var returnedResult = dataService.GetAll();

            // assert
            Assert.AreEqual(returnedResult.Count(), modelsList.Count);
            Assert.AreEqual(returnedResult.First().IsDeleted, modelsList[0].IsDeleted);
            Assert.AreEqual(returnedResult.Skip(1).First().IsDeleted, modelsList[1].IsDeleted);
        }
    }
}
