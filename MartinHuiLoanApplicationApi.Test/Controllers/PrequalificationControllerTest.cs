using MartinHuiLoanApplicationApi.Controllers;
using MartinHuiLoanApplicationApi.Model;
using MartinHuiLoanApplicationApi.Services;
using MartinHuiLoanApplicationApi.Test.Data;
using MartinHuiLoanApplicationApi.Test.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System.Diagnostics;

namespace MartinHuiLoanApplicationApi.Test.Controllers
{
    [TestFixture]
    public class PrequalificationControllerTest
    {
        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public async Task PostPrequalification_Ok()
        {
            Trace.Listeners.Add(new ConsoleTraceListener());
            // Arrange
            var loanProducts = new List<LoanProduct>{LoanProductData.Product31100MinSalary,LoanProductData.Product40000MinSalary};
            var mockLoanProductDbSet = loanProducts.AsQueryable().BuildMockDbSet();
            var loanApplicants = new List<LoanApplicant>();
            var mockLoanApplicantDbSet = loanApplicants.AsQueryable().BuildMockDbSet();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().Options;
            var mockApplicationDbContext = new Mock<ApplicationDbContext>(options);

            // Ensure that the DbContext returns the DbSet<LoanProduct> when queried for LoanProducts
            mockApplicationDbContext.Setup(db => db.LoanProducts).Returns(mockLoanProductDbSet.Object);
            mockApplicationDbContext.Setup(db => db.LoanApplicants).Returns(mockLoanApplicantDbSet.Object);

            var mockLoanProductService = new Mock<LoanProductService>(mockApplicationDbContext.Object);
            var mockLogger = new Mock<ILogger<PrequalificationController>>();
            var controller = new PrequalificationController(mockApplicationDbContext.Object, mockLoanProductService.Object, mockLogger.Object);
            
            //Act
            var applicant = LoanApplicantData.Applicant33333AnnualSalary;

            var controllerResult = await controller.PostPrequalification(applicant);

            //Assert result is not null
            Assert.IsNotNull(controllerResult);
            var result = controllerResult.Result as OkObjectResult;
            //Debug.WriteLine($"{JsonConvert.SerializeObject(result.Value)}");
            // Assert result is 200 success status code
            Assert.IsInstanceOf<OkObjectResult>(controllerResult.Result, "Result should be OkObjectResult");

            //Assert respond products is not null
            var entitiesReturned = (IEnumerable<LoanProduct>)result.Value;

            Assert.IsNotNull(entitiesReturned);

            //Assert the number of responded products are the same
            Assert.AreEqual(1, entitiesReturned.Count());

            //Assert the responded products are the same
            foreach (var entity in entitiesReturned)
            {
                Assert.IsTrue(entity.MinimumAnnualSalary <= applicant.AnnualIncome);
            }
            Trace.Flush();
        }

        [Test]
        public async Task PostPrequalification_NotFound()
        {
            Trace.Listeners.Add(new ConsoleTraceListener());
            // Arrange
            var loanProducts = new List<LoanProduct> { LoanProductData.Product40000MinSalary, LoanProductData.Product36900MinSalary };
            var mockLoanProductDbSet = loanProducts.AsQueryable().BuildMockDbSet();
            var loanApplicants = new List<LoanApplicant>();
            var mockLoanApplicantDbSet = loanApplicants.AsQueryable().BuildMockDbSet();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().Options;
            var mockApplicationDbContext = new Mock<ApplicationDbContext>(options);

            // Ensure that the DbContext returns the DbSet<LoanProduct> when queried for LoanProducts
            mockApplicationDbContext.Setup(db => db.LoanProducts).Returns(mockLoanProductDbSet.Object);
            mockApplicationDbContext.Setup(db => db.LoanApplicants).Returns(mockLoanApplicantDbSet.Object);

            var mockLoanProductService = new Mock<LoanProductService>(mockApplicationDbContext.Object);
            var mockLogger = new Mock<ILogger<PrequalificationController>>();
            var controller = new PrequalificationController(mockApplicationDbContext.Object, mockLoanProductService.Object, mockLogger.Object);

            //Act
            var applicant = LoanApplicantData.Applicant33333AnnualSalary;

            var controllerResult = await controller.PostPrequalification(applicant);

            //Assert result is not null
            Assert.IsNotNull(controllerResult);

            Assert.IsInstanceOf<NotFoundResult>(controllerResult.Result, "Result should be NotFoundResult");


            Trace.Flush();
        }

        [Test]
        public async Task PostPrequalification_InvalidFirstnameLength()
        {
            Trace.Listeners.Add(new ConsoleTraceListener());

            // Arrange
            var loanProducts = LoanProductData.RandomLoanProducts;
            var mockLoanProductDbSet = loanProducts.AsQueryable().BuildMockDbSet();
            var loanApplicants = new List<LoanApplicant>();
            var mockLoanApplicantDbSet = loanApplicants.AsQueryable().BuildMockDbSet();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().Options;
            var mockApplicationDbContext = new Mock<ApplicationDbContext>(options);

            // Ensure that the DbContext returns the DbSet<LoanProduct> when queried for LoanProducts
            mockApplicationDbContext.Setup(db => db.LoanProducts).Returns(mockLoanProductDbSet.Object);
            mockApplicationDbContext.Setup(db => db.LoanApplicants).Returns(mockLoanApplicantDbSet.Object);

            var mockLoanProductService = new Mock<LoanProductService>(mockApplicationDbContext.Object);
            var mockLogger = new Mock<ILogger<PrequalificationController>>();
            var controller = new PrequalificationController(mockApplicationDbContext.Object, mockLoanProductService.Object, mockLogger.Object);

            //Act
            var applicant = LoanApplicantData.Applicant33333AnnualSalary;
            applicant.FirstName = "abcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabc";
            controller.ValidateModel(applicant);
            var controllerResult = await controller.PostPrequalification(applicant);

            //Assert result is not null
            Assert.IsNotNull(controllerResult);

            //Should be a Badrequest
            Assert.IsInstanceOf<BadRequestObjectResult>(controllerResult.Result, "Result should be BadRequest");


            Trace.Flush();
        }

        [Test]
        public async Task PostPrequalification_InvalidLastnameLength()
        {
            Trace.Listeners.Add(new ConsoleTraceListener());

            // Arrange
            var loanProducts = LoanProductData.RandomLoanProducts;
            var mockLoanProductDbSet = loanProducts.AsQueryable().BuildMockDbSet();
            var loanApplicants = new List<LoanApplicant>();
            var mockLoanApplicantDbSet = loanApplicants.AsQueryable().BuildMockDbSet();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().Options;
            var mockApplicationDbContext = new Mock<ApplicationDbContext>(options);

            // Ensure that the DbContext returns the DbSet<LoanProduct> when queried for LoanProducts
            mockApplicationDbContext.Setup(db => db.LoanProducts).Returns(mockLoanProductDbSet.Object);
            mockApplicationDbContext.Setup(db => db.LoanApplicants).Returns(mockLoanApplicantDbSet.Object);

            var mockLoanProductService = new Mock<LoanProductService>(mockApplicationDbContext.Object);
            var mockLogger = new Mock<ILogger<PrequalificationController>>();
            var controller = new PrequalificationController(mockApplicationDbContext.Object, mockLoanProductService.Object, mockLogger.Object);

            //Act
            var applicant = LoanApplicantData.Applicant33333AnnualSalary;
            applicant.LastName = "abcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabc";
            Debug.WriteLine($"Applicant firstname length: {applicant.FirstName.Length}");
            controller.ValidateModel(applicant);
            var controllerResult = await controller.PostPrequalification(applicant);

            //Assert result is not null
            Assert.IsNotNull(controllerResult);

            //Should be a Badrequest
            Assert.IsInstanceOf<BadRequestObjectResult>(controllerResult.Result, "Result should be BadRequest");

            Trace.Flush();
        }


        [Test]
        public async Task PostPrequalification_InvalidAddressLength()
        {
            Trace.Listeners.Add(new ConsoleTraceListener());

            // Arrange
            var loanProducts = LoanProductData.RandomLoanProducts;
            var mockLoanProductDbSet = loanProducts.AsQueryable().BuildMockDbSet();
            var loanApplicants = new List<LoanApplicant>();
            var mockLoanApplicantDbSet = loanApplicants.AsQueryable().BuildMockDbSet();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().Options;
            var mockApplicationDbContext = new Mock<ApplicationDbContext>(options);

            // Ensure that the DbContext returns the DbSet<LoanProduct> when queried for LoanProducts
            mockApplicationDbContext.Setup(db => db.LoanProducts).Returns(mockLoanProductDbSet.Object);
            mockApplicationDbContext.Setup(db => db.LoanApplicants).Returns(mockLoanApplicantDbSet.Object);

            var mockLoanProductService = new Mock<LoanProductService>(mockApplicationDbContext.Object);
            var mockLogger = new Mock<ILogger<PrequalificationController>>();
            var controller = new PrequalificationController(mockApplicationDbContext.Object, mockLoanProductService.Object, mockLogger.Object);

            //Act
            var applicant = LoanApplicantData.Applicant33333AnnualSalary;
            applicant.Address = $"abcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcababcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcababcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcababcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcababcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcababcabc";
            controller.ValidateModel(applicant);
            var controllerResult = await controller.PostPrequalification(applicant);

            //Assert result is not null
            Assert.IsNotNull(controllerResult);

            //Should be a Badrequest
            Assert.IsInstanceOf<BadRequestObjectResult>(controllerResult.Result, "Result should be BadRequest");


            Trace.Flush();
        }


        [Test]
        public async Task PostPrequalification_InvalidCityLength()
        {
            Trace.Listeners.Add(new ConsoleTraceListener());

            // Arrange
            var loanProducts = LoanProductData.RandomLoanProducts;
            var mockLoanProductDbSet = loanProducts.AsQueryable().BuildMockDbSet();
            var loanApplicants = new List<LoanApplicant>();
            var mockLoanApplicantDbSet = loanApplicants.AsQueryable().BuildMockDbSet();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().Options;
            var mockApplicationDbContext = new Mock<ApplicationDbContext>(options);

            // Ensure that the DbContext returns the DbSet<LoanProduct> when queried for LoanProducts
            mockApplicationDbContext.Setup(db => db.LoanProducts).Returns(mockLoanProductDbSet.Object);
            mockApplicationDbContext.Setup(db => db.LoanApplicants).Returns(mockLoanApplicantDbSet.Object);

            var mockLoanProductService = new Mock<LoanProductService>(mockApplicationDbContext.Object);
            var mockLogger = new Mock<ILogger<PrequalificationController>>();
            var controller = new PrequalificationController(mockApplicationDbContext.Object, mockLoanProductService.Object, mockLogger.Object);

            //Act
            var applicant = LoanApplicantData.Applicant33333AnnualSalary;
            applicant.City = "abcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcababcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabcabc";
            controller.ValidateModel(applicant);
            var controllerResult = await controller.PostPrequalification(applicant);

            //Assert result is not null
            Assert.IsNotNull(controllerResult);

            //Should be a Badrequest
            Assert.IsInstanceOf<BadRequestObjectResult>(controllerResult.Result, "Result should be BadRequest");


            Trace.Flush();
        }

        [Test]
        public async Task PostPrequalification_InvalidCountryLength()
        {
            Trace.Listeners.Add(new ConsoleTraceListener());

            // Arrange
            var loanProducts = LoanProductData.RandomLoanProducts;
            var mockLoanProductDbSet = loanProducts.AsQueryable().BuildMockDbSet();
            var loanApplicants = new List<LoanApplicant>();
            var mockLoanApplicantDbSet = loanApplicants.AsQueryable().BuildMockDbSet();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().Options;
            var mockApplicationDbContext = new Mock<ApplicationDbContext>(options);

            // Ensure that the DbContext returns the DbSet<LoanProduct> when queried for LoanProducts
            mockApplicationDbContext.Setup(db => db.LoanProducts).Returns(mockLoanProductDbSet.Object);
            mockApplicationDbContext.Setup(db => db.LoanApplicants).Returns(mockLoanApplicantDbSet.Object);

            var mockLoanProductService = new Mock<LoanProductService>(mockApplicationDbContext.Object);
            var mockLogger = new Mock<ILogger<PrequalificationController>>();
            var controller = new PrequalificationController(mockApplicationDbContext.Object, mockLoanProductService.Object, mockLogger.Object);

            //Act
            var applicant = LoanApplicantData.Applicant33333AnnualSalary;
            applicant.Country = "gb-en";
            controller.ValidateModel(applicant);
            var controllerResult = await controller.PostPrequalification(applicant);

            //Assert result is not null
            Assert.IsNotNull(controllerResult);

            //Should be a Badrequest
            Assert.IsInstanceOf<BadRequestObjectResult>(controllerResult.Result, "Result should be BadRequest");


            Trace.Flush();
        }

        [Test]
        public async Task PostPrequalification_InvalidCountryCode()
        {
            Trace.Listeners.Add(new ConsoleTraceListener());

            // Arrange
            var loanProducts = LoanProductData.RandomLoanProducts;
            var mockLoanProductDbSet = loanProducts.AsQueryable().BuildMockDbSet();
            var loanApplicants = new List<LoanApplicant>();
            var mockLoanApplicantDbSet = loanApplicants.AsQueryable().BuildMockDbSet();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().Options;
            var mockApplicationDbContext = new Mock<ApplicationDbContext>(options);

            // Ensure that the DbContext returns the DbSet<LoanProduct> when queried for LoanProducts
            mockApplicationDbContext.Setup(db => db.LoanProducts).Returns(mockLoanProductDbSet.Object);
            mockApplicationDbContext.Setup(db => db.LoanApplicants).Returns(mockLoanApplicantDbSet.Object);

            var mockLoanProductService = new Mock<LoanProductService>(mockApplicationDbContext.Object);
            var mockLogger = new Mock<ILogger<PrequalificationController>>();
            var controller = new PrequalificationController(mockApplicationDbContext.Object, mockLoanProductService.Object, mockLogger.Object);

            //Act
            var applicant = LoanApplicantData.Applicant33333AnnualSalary;
            applicant.Country = "aa";
            controller.ValidateModel(applicant);
            var controllerResult = await controller.PostPrequalification(applicant);

            //Assert result is not null
            Assert.IsNotNull(controllerResult);

            //Should be a Badrequest
            Assert.IsInstanceOf<BadRequestObjectResult>(controllerResult.Result, "Result should be BadRequest");


            Trace.Flush();
        }

        [Test]
        public async Task PostPrequalification_InvalidPostCode_WithSymbol()
        {
            Trace.Listeners.Add(new ConsoleTraceListener());

            // Arrange
            var loanProducts = LoanProductData.RandomLoanProducts;
            var mockLoanProductDbSet = loanProducts.AsQueryable().BuildMockDbSet();
            var loanApplicants = new List<LoanApplicant>();
            var mockLoanApplicantDbSet = loanApplicants.AsQueryable().BuildMockDbSet();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().Options;
            var mockApplicationDbContext = new Mock<ApplicationDbContext>(options);

            // Ensure that the DbContext returns the DbSet<LoanProduct> when queried for LoanProducts
            mockApplicationDbContext.Setup(db => db.LoanProducts).Returns(mockLoanProductDbSet.Object);
            mockApplicationDbContext.Setup(db => db.LoanApplicants).Returns(mockLoanApplicantDbSet.Object);

            var mockLoanProductService = new Mock<LoanProductService>(mockApplicationDbContext.Object);
            var mockLogger = new Mock<ILogger<PrequalificationController>>();
            var controller = new PrequalificationController(mockApplicationDbContext.Object, mockLoanProductService.Object, mockLogger.Object);

            //Act
            var applicant = LoanApplicantData.Applicant33333AnnualSalary;
            applicant.PostalCode = "AB!*DA";
            controller.ValidateModel(applicant);
            var controllerResult = await controller.PostPrequalification(applicant);

            //Assert result is not null
            Assert.IsNotNull(controllerResult);

            //Should be a Badrequest
            Assert.IsInstanceOf<BadRequestObjectResult>(controllerResult.Result, "Result should be BadRequest");


            Trace.Flush();
        }

        [Test]
        public async Task PostPrequalification_InvalidDateOfBirth_TooYoung()
        {
            Trace.Listeners.Add(new ConsoleTraceListener());

            // Arrange
            var loanProducts = LoanProductData.RandomLoanProducts;
            var mockLoanProductDbSet = loanProducts.AsQueryable().BuildMockDbSet();
            var loanApplicants = new List<LoanApplicant>();
            var mockLoanApplicantDbSet = loanApplicants.AsQueryable().BuildMockDbSet();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().Options;
            var mockApplicationDbContext = new Mock<ApplicationDbContext>(options);

            // Ensure that the DbContext returns the DbSet<LoanProduct> when queried for LoanProducts
            mockApplicationDbContext.Setup(db => db.LoanProducts).Returns(mockLoanProductDbSet.Object);
            mockApplicationDbContext.Setup(db => db.LoanApplicants).Returns(mockLoanApplicantDbSet.Object);

            var mockLoanProductService = new Mock<LoanProductService>(mockApplicationDbContext.Object);
            var mockLogger = new Mock<ILogger<PrequalificationController>>();
            var controller = new PrequalificationController(mockApplicationDbContext.Object, mockLoanProductService.Object, mockLogger.Object);

            //Act
            var applicant = LoanApplicantData.Applicant33333AnnualSalary;
            applicant.DateOfBirth = DateTime.Now.AddYears(-1);
            controller.ValidateModel(applicant);
            var controllerResult = await controller.PostPrequalification(applicant);

            //Assert result is not null
            Assert.IsNotNull(controllerResult);

            //Should be a Badrequest
            Assert.IsInstanceOf<BadRequestObjectResult>(controllerResult.Result, "Result should be BadRequest");


            Trace.Flush();
        }


        [Test]
        public async Task PostPrequalification_InvalidDateOfBirth_TooOld()
        {
            Trace.Listeners.Add(new ConsoleTraceListener());

            // Arrange
            var loanProducts = LoanProductData.RandomLoanProducts;
            var mockLoanProductDbSet = loanProducts.AsQueryable().BuildMockDbSet();
            var loanApplicants = new List<LoanApplicant>();
            var mockLoanApplicantDbSet = loanApplicants.AsQueryable().BuildMockDbSet();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().Options;
            var mockApplicationDbContext = new Mock<ApplicationDbContext>(options);

            // Ensure that the DbContext returns the DbSet<LoanProduct> when queried for LoanProducts
            mockApplicationDbContext.Setup(db => db.LoanProducts).Returns(mockLoanProductDbSet.Object);
            mockApplicationDbContext.Setup(db => db.LoanApplicants).Returns(mockLoanApplicantDbSet.Object);

            var mockLoanProductService = new Mock<LoanProductService>(mockApplicationDbContext.Object);
            var mockLogger = new Mock<ILogger<PrequalificationController>>();
            var controller = new PrequalificationController(mockApplicationDbContext.Object, mockLoanProductService.Object, mockLogger.Object);

            //Act
            var applicant = LoanApplicantData.Applicant33333AnnualSalary;
            applicant.DateOfBirth = DateTime.Now.AddYears(-151);
            controller.ValidateModel(applicant);
            var controllerResult = await controller.PostPrequalification(applicant);

            //Assert result is not null
            Assert.IsNotNull(controllerResult);

            //Should be a Badrequest
            Assert.IsInstanceOf<BadRequestObjectResult>(controllerResult.Result, "Result should be BadRequest");


            Trace.Flush();
        }


        [Test]
        public async Task PostPrequalification_InvalidAnnualIncome_LessThanZero()
        {
            Trace.Listeners.Add(new ConsoleTraceListener());

            // Arrange
            var loanProducts = LoanProductData.RandomLoanProducts;
            var mockLoanProductDbSet = loanProducts.AsQueryable().BuildMockDbSet();
            var loanApplicants = new List<LoanApplicant>();
            var mockLoanApplicantDbSet = loanApplicants.AsQueryable().BuildMockDbSet();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().Options;
            var mockApplicationDbContext = new Mock<ApplicationDbContext>(options);

            // Ensure that the DbContext returns the DbSet<LoanProduct> when queried for LoanProducts
            mockApplicationDbContext.Setup(db => db.LoanProducts).Returns(mockLoanProductDbSet.Object);
            mockApplicationDbContext.Setup(db => db.LoanApplicants).Returns(mockLoanApplicantDbSet.Object);

            var mockLoanProductService = new Mock<LoanProductService>(mockApplicationDbContext.Object);
            var mockLogger = new Mock<ILogger<PrequalificationController>>();
            var controller = new PrequalificationController(mockApplicationDbContext.Object, mockLoanProductService.Object, mockLogger.Object);

            //Act
            var applicant = LoanApplicantData.Applicant33333AnnualSalary;
            applicant.AnnualIncome = -1.1M;
            controller.ValidateModel(applicant);
            var controllerResult = await controller.PostPrequalification(applicant);

            //Assert result is not null
            Assert.IsNotNull(controllerResult);

            //Should be a Badrequest
            Assert.IsInstanceOf<BadRequestObjectResult>(controllerResult.Result, "Result should be BadRequest");


            Trace.Flush();
        }


        [Test]
        public async Task PostPrequalification_InvalidAnnualIncome_OverMaximum()
        {
            Trace.Listeners.Add(new ConsoleTraceListener());

            // Arrange
            var loanProducts = LoanProductData.RandomLoanProducts;
            var mockLoanProductDbSet = loanProducts.AsQueryable().BuildMockDbSet();
            var loanApplicants = new List<LoanApplicant>();
            var mockLoanApplicantDbSet = loanApplicants.AsQueryable().BuildMockDbSet();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().Options;
            var mockApplicationDbContext = new Mock<ApplicationDbContext>(options);

            // Ensure that the DbContext returns the DbSet<LoanProduct> when queried for LoanProducts
            mockApplicationDbContext.Setup(db => db.LoanProducts).Returns(mockLoanProductDbSet.Object);
            mockApplicationDbContext.Setup(db => db.LoanApplicants).Returns(mockLoanApplicantDbSet.Object);

            var mockLoanProductService = new Mock<LoanProductService>(mockApplicationDbContext.Object);
            var mockLogger = new Mock<ILogger<PrequalificationController>>();
            var controller = new PrequalificationController(mockApplicationDbContext.Object, mockLoanProductService.Object, mockLogger.Object);

            //Act
            var applicant = LoanApplicantData.Applicant33333AnnualSalary;
            applicant.AnnualIncome = 10000000.1M;
            controller.ValidateModel(applicant);
            var controllerResult = await controller.PostPrequalification(applicant);

            //Assert result is not null
            Assert.IsNotNull(controllerResult);

            //Should be a Badrequest
            Assert.IsInstanceOf<BadRequestObjectResult>(controllerResult.Result, "Result should be BadRequest");


            Trace.Flush();
        }
    }
}