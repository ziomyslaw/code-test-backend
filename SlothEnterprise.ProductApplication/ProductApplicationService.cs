using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.ProductStrategies;

namespace SlothEnterprise.ProductApplication
{
    public class ProductApplicationService : IProductApplicationService
    {
        private readonly ProductContext _productContext;

        public ProductApplicationService(ProductContext productContext)
        {
            _productContext = productContext;
        }

        public int SubmitApplicationFor(ISellerApplication application)
        {
            _productContext.SetStrategy(application.Product);
            return _productContext.SubmitApplicationFor(application.Product, application.CompanyData);
        }
    }
}
