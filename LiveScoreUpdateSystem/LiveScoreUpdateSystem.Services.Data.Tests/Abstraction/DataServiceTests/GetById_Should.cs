using LiveScoreUpdateSystem.Data.Models.Abstraction;
using LiveScoreUpdateSystem.Data.Repositories.Contracts;
using LiveScoreUpdateSystem.Services.Data.Abstraction;
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
    public class GetById_Should
    {
        [Test]
        public void CallDataAllMethodOnce_WhenMethodInvoked()
        {
            // arrange
            var repository = new Mock<IEfRepository<DataModel>>();

            var modelsList = new List<DataModel>()
            {
                new DataModel(){IsDeleted = false, Id = Guid.NewGuid()},
                new DataModel(){IsDeleted = true, Id = Guid.NewGuid()},
            };

            repository.Setup(r => r.All).Returns(modelsList.AsQueryable());

            var dataService = new DataService<DataModel>(repository.Object);

            // act
            var returnedResult = dataService.GetById(modelsList[0].Id);

            // assert
            repository.Verify(r => r.All, Times.Once);
        }

        [Test]
        public void ReturnTheCorrectEntity_WhenThereIsSuchEntityWithTheProvidedIdInTheCollection()
        {
            // arrange
            var repository = new Mock<IEfRepository<DataModel>>();

            var modelsList = new List<DataModel>()
            {
                new DataModel(){Id = Guid.NewGuid()},
                new DataModel(){Id = Guid.NewGuid()},
            };

            repository.Setup(r => r.All).Returns(modelsList.AsQueryable());
            var dataService = new DataService<DataModel>(repository.Object);

            // act
            var returnedResult = dataService.GetById(modelsList[1].Id);

            // assert
            Assert.AreSame(returnedResult, modelsList[1]);
        }
    }
}
