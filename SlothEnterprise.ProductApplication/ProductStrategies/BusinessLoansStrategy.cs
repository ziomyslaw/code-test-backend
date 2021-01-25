using SlothEnterprise.External;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Products;
using SlothEnterprise.ProductApplication.Products.BusinessLoans;

namespace SlothEnterprise.ProductApplication.ProductStrategies
{
    public class BusinessLoansStrategy : IProductStrategy
    {
        private readonly IBusinessLoansService _businessLoansService;

        public BusinessLoansStrategy(IBusinessLoansService businessLoansService)
        {
            _businessLoansService = businessLoansService;
        }

        public int SubmitApplicationFor(IProduct product, ISellerCompanyData companyData)
        {
            var businessLoans = (BusinessLoans)product;
            var result = _businessLoansService.SubmitApplicationFor(
                new CompanyDataRequest
                {
                    CompanyFounded = companyData.Founded,
                    CompanyNumber = companyData.Number,
                    CompanyName = companyData.Name,
                    DirectorName = companyData.DirectorName
                }, 
                new LoansRequest
                {
                    InterestRatePerAnnum = businessLoans.InterestRatePerAnnum,
                    LoanAmount = businessLoans.LoanAmount
                });

            return result.ToProductId();
        }
    }
}
