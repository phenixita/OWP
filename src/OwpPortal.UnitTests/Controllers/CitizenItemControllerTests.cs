using System;
using Xunit;
using Moq;
using owp_web.Data;
using Microsoft.EntityFrameworkCore;
using owp_web.Models;
using owp_web.Controllers;

namespace OwpPortal.UnitTests
{
    public class CitizenItemControllerTests
    {
        private readonly Mock<IOwpContext> testContext;
        private readonly Mock<DbSet<WorkItem>> testEntities;

        public CitizenItemControllerTests()
        {
            // Initiate ICustomContext
            testContext = new Mock<IOwpContext>();
            // Initiate DbSet
            testEntities = new Mock<DbSet<WorkItem>>();
            // Setup DbSet
            testContext.Setup(ctx => ctx.WorkItem).Returns(testEntities.Object);
        }

        [Fact]
        public void WhenValidIdisUsedToRequestWorkitemTheWorkitemIsReturned()
        {
            //arrange
            var ctrl = new CitizenItemsController(testContext.Object);
            //act
            var result = ctrl.Details(5);
            //assert


        }
    }
}
