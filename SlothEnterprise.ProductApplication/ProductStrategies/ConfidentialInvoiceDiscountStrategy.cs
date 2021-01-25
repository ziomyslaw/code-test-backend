using SlothEnterprise.External;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Products;
using SlothEnterprise.ProductApplication.Products.ConfidentialInvoiceDiscount;

namespace SlothEnterprise.ProductApplication.ProductStrategies
{
    public class ConfidentialInvoiceDiscountStrategy : IProductStrategy
    {
        private readonly IConfidentialInvoiceService _confidentialInvoiceWebService;

        public ConfidentialInvoiceDiscountStrategy(IConfidentialInvoiceService confidentialInvoiceWebService)
        {
            _confidentialInvoiceWebService = confidentialInvoiceWebService;
        }

        public int SubmitApplicationFor(IProduct product, ISellerCompanyData companyData)
        {
            var confidentialInvoiceDiscount = (ConfidentialInvoiceDiscount)product;
            var result = _confidentialInvoiceWebService.SubmitApplicationFor(
                new CompanyDataRequest
                {
                    CompanyFounded = companyData.Founded,
                    CompanyNumber = companyData.Number,
                    CompanyName = companyData.Name,
                    DirectorName = companyData.DirectorName
                },
                confidentialInvoiceDiscount.TotalLedgerNetworth,
                confidentialInvoiceDiscount.AdvancePercentage,
                confidentialInvoiceDiscount.VatRate);

            return result.ToProductId();
        }
    }
}
