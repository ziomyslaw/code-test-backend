using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Products;

namespace SlothEnterprise.ProductApplication.ProductStrategies
{
    public interface IProductStrategy
    {
        int SubmitApplicationFor(IProduct product, ISellerCompanyData companyData);
    }
}
