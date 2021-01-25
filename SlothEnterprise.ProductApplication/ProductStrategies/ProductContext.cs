using System;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Products;
using SlothEnterprise.ProductApplication.Products.BusinessLoans;
using SlothEnterprise.ProductApplication.Products.ConfidentialInvoiceDiscount;
using SlothEnterprise.ProductApplication.Products.SelectiveInvoiceDiscount;

namespace SlothEnterprise.ProductApplication.ProductStrategies
{
    public class ProductContext
    {
        private IProductStrategy _productStrategy;
        private readonly BusinessLoansStrategy _businessLoansStrategy;
        private readonly ConfidentialInvoiceDiscountStrategy _confidentialInvoiceDiscountStrategy;
        private readonly SelectiveInvoiceDiscountStrategy _selectiveInvoiceDiscountStrategy;

        public ProductContext(
            BusinessLoansStrategy businessLoansStrategy, 
            ConfidentialInvoiceDiscountStrategy confidentialInvoiceDiscountStrategy, 
            SelectiveInvoiceDiscountStrategy selectiveInvoiceDiscountStrategy)
        {
            _businessLoansStrategy = businessLoansStrategy;
            _confidentialInvoiceDiscountStrategy = confidentialInvoiceDiscountStrategy;
            _selectiveInvoiceDiscountStrategy = selectiveInvoiceDiscountStrategy;
        }

        public void SetStrategy(IProduct product)
        {
            switch (product)
            {
                case BusinessLoans _:
                    _productStrategy = _businessLoansStrategy;
                    break;
                case ConfidentialInvoiceDiscount _:
                    _productStrategy = _confidentialInvoiceDiscountStrategy;
                    break;
                case SelectiveInvoiceDiscount _:
                    _productStrategy = _selectiveInvoiceDiscountStrategy;
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }

        public int SubmitApplicationFor(IProduct product, ISellerCompanyData companyData)
        {
            return _productStrategy.SubmitApplicationFor(product, companyData);
        }
    }
}
