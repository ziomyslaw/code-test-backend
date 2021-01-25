using FluentAssertions;
using Moq;
using SlothEnterprise.External;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Products.BusinessLoans;
using SlothEnterprise.ProductApplication.Products.ConfidentialInvoiceDiscount;
using SlothEnterprise.ProductApplication.Products.SelectiveInvoiceDiscount;
using SlothEnterprise.ProductApplication.ProductStrategies;
using Xunit;

namespace SlothEnterprise.ProductApplication.Tests
{
    public class ProductApplicationServiceTests
    {
        private readonly IProductApplicationService _sut;
        private readonly Mock<IBusinessLoansService> _businessLoansServiceMock = new Mock<IBusinessLoansService>();
        private readonly Mock<IConfidentialInvoiceService> _confidentialInvoiceServiceMock = new Mock<IConfidentialInvoiceService>();
        private readonly Mock<ISelectInvoiceService> _selectInvoiceServiceMock = new Mock<ISelectInvoiceService>();
        private readonly Mock<IApplicationResult> _result = new Mock<IApplicationResult>();
        private readonly Mock<ISellerApplication> _sellerApplicationMock = new Mock<ISellerApplication>();

        public ProductApplicationServiceTests()
        {
            _result.SetupProperty(p => p.ApplicationId, 1);
            _result.SetupProperty(p => p.Success, true);

            _businessLoansServiceMock
                .Setup(m => m.SubmitApplicationFor(It.IsAny<CompanyDataRequest>(), It.IsAny<LoansRequest>()))
                .Returns(_result.Object);

            _confidentialInvoiceServiceMock
                .Setup(m => m.SubmitApplicationFor(It.IsAny<CompanyDataRequest>(), It.IsAny<decimal>(), It.IsAny<decimal>(), It.IsAny<decimal>()))
                .Returns(_result.Object);

            _selectInvoiceServiceMock
                .Setup(m => m.SubmitApplicationFor(It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<decimal>()))
                .Returns(1);

            _sellerApplicationMock.SetupProperty(p => p.CompanyData, new SellerCompanyData());

            var productApplicationService = new Mock<IProductApplicationService>();
            _sut = new ProductApplicationService(new ProductContext(
                new BusinessLoansStrategy(_businessLoansServiceMock.Object),
                new ConfidentialInvoiceDiscountStrategy(_confidentialInvoiceServiceMock.Object),
                new SelectiveInvoiceDiscountStrategy(_selectInvoiceServiceMock.Object)));

            productApplicationService.Setup(m => m.SubmitApplicationFor(It.IsAny<ISellerApplication>())).Returns(1);
            
        }

        [Fact]
        public void ProductApplicationService_SubmitApplicationFor_WhenCalledWithValidProduct_ShouldReturnNonNegativeId()
        {
            _sellerApplicationMock.SetupProperty(p => p.Product, new BusinessLoans());

            var result = _sut.SubmitApplicationFor(_sellerApplicationMock.Object);
            result.Should().Be(1);

            _sellerApplicationMock.SetupProperty(p => p.Product, new ConfidentialInvoiceDiscount());

            result = _sut.SubmitApplicationFor(_sellerApplicationMock.Object);
            result.Should().Be(1);


            _sellerApplicationMock.SetupProperty(p => p.Product, new SelectiveInvoiceDiscount());

            result = _sut.SubmitApplicationFor(_sellerApplicationMock.Object);
            result.Should().Be(1);
        }
    }
}