using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using asp_tender_be.ApiControllers;
using asp_tender_be.ApiModels;
using asp_tender_be.Models;
using asp_tender_be.Services;

namespace Tests
{
    [TestClass]
    public class TestJobApplicationsController
    {
        [TestMethod]
        public async Task TestPostJobApplicationSuccess()
        {
            var position = new Position { ID = 42 };

            var repositoryMock = new Mock<IRepository>();
            repositoryMock.Setup(r => r.GetPositionByID(It.IsAny<int>())).Returns(Task.FromResult(position));

            var hubConnectorMock = new Mock<IJobApplicationsHubConnector>();

            var jobApplicationsController = new JobApplicationsController(repositoryMock.Object, hubConnectorMock.Object);

            var result = await jobApplicationsController.PostJobApplication(new PostJobApplicationModel
            {
                PositionID = position.ID,
                Text = "Some text.",
                Email = "me@internet",
                Phone = "777 222 888",
                Cv = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("Dummy file")), 0, 10, "dummy", "dummy.txt") { Headers = new HeaderDictionary(), ContentType = "text/plain" },
            });

            Assert.IsInstanceOfType(result.Result, typeof(ObjectResult));

            hubConnectorMock.Verify(mock => mock.RefreshOverviewViaHub(), Times.Once());
            repositoryMock.Verify(mock => mock.InsertJobApplication(It.IsAny<JobApplication>()), Times.Once());
            repositoryMock.Verify(mock => mock.Save(), Times.Once());
        }

        // validace všech pøíchozích dat kromì PositionID probíhá automaticky díky [ApiController] atributu

        [TestMethod]
        public async Task TestPostJobApplicationPositionNotFound()
        {
            var repositoryMock = new Mock<IRepository>();
            repositoryMock.Setup(r => r.GetPositionByID(It.IsAny<int>())).Returns(Task.FromResult<Position>(null));

            var hubConnectorMock = new Mock<IJobApplicationsHubConnector>();

            var jobApplicationsController = new JobApplicationsController(repositoryMock.Object, hubConnectorMock.Object);

            var result = await jobApplicationsController.PostJobApplication(new PostJobApplicationModel
            {
                PositionID = 42,
                Text = "Some text.",
                Email = "me@internet",
                Phone = "777 222 888",
                Cv = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("Dummy file")), 0, 10, "dummy", "dummy.txt") { Headers = new HeaderDictionary(), ContentType = "text/plain" },
            });

            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));

            hubConnectorMock.Verify(mock => mock.RefreshOverviewViaHub(), Times.Never());
            repositoryMock.Verify(mock => mock.InsertJobApplication(It.IsAny<JobApplication>()), Times.Never());
            repositoryMock.Verify(mock => mock.Save(), Times.Never());
        }

        [TestMethod]
        public async Task TestDownloadSuccess()
        {
            var jobApplication = new JobApplication { CvFileName = "dummy.txt", CvMimeType = "text/plain", CvData = Encoding.UTF8.GetBytes("Dummy file") };

            var repositoryMock = new Mock<IRepository>();
            repositoryMock.Setup(r => r.GetPendingJobApplicationByID(It.IsAny<int>())).Returns(Task.FromResult(jobApplication));

            var hubConnectorMock = new Mock<IJobApplicationsHubConnector>();

            var jobApplicationsController = new JobApplicationsController(repositoryMock.Object, hubConnectorMock.Object);

            var result = await jobApplicationsController.Download(42);

            Assert.IsInstanceOfType(result, typeof(FileContentResult));
            Assert.AreEqual((result as FileContentResult).FileContents, jobApplication.CvData);
        }

        [TestMethod]
        public async Task TestDownloadJobApplicationNotFound()
        {
            var repositoryMock = new Mock<IRepository>();
            repositoryMock.Setup(r => r.GetPendingJobApplicationByID(It.IsAny<int>())).Returns(Task.FromResult<JobApplication>(null));

            var hubConnectorMock = new Mock<IJobApplicationsHubConnector>();

            var jobApplicationsController = new JobApplicationsController(repositoryMock.Object, hubConnectorMock.Object);

            var result = await jobApplicationsController.Download(42);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
