using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Products;
using SlothEnterprise.ProductApplication.Products.SelectiveInvoiceDiscount;

namespace SlothEnterprise.ProductApplication.ProductStrategies
{
    public class SelectiveInvoiceDiscountStrategy : IProductStrategy
    {
        private readonly ISelectInvoiceService _selectInvoiceService;

        public SelectiveInvoiceDiscountStrategy(ISelectInvoiceService selectInvoiceService)
        {
            _selectInvoiceService = selectInvoiceService;
        }

        public int SubmitApplicationFor(IProduct product, ISellerCompanyData companyData)
        {
            var selectiveInvoiceDiscount = (SelectiveInvoiceDiscount)product;
            return _selectInvoiceService.SubmitApplicationFor(companyData.Number.ToString(), selectiveInvoiceDiscount.InvoiceAmount, selectiveInvoiceDiscount.AdvancePercentage);
        }
    }
}
