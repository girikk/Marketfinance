using System;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Products;
using SlothEnterprise.External;
using SlothEnterprise.External.V1;

namespace SlothEnterprise.ProductApplication
{

    public class SelectInvoiceServiceWrapper : ISubmitApplication
    {

        private readonly ISelectInvoiceService _selectInvoiceService;
        private readonly decimal _invoiceAmount;
        private readonly decimal _advancePercentage;

        public SelectInvoiceServiceWrapper(decimal invoiceAmount, decimal advancePercentage, ISelectInvoiceService selectInvoiceService)
        {
            _invoiceAmount = invoiceAmount;
            _advancePercentage = advancePercentage;
            _selectInvoiceService = selectInvoiceService;
        }

        public int SubmitApplication(ISellerApplication application)
        {
            return _selectInvoiceService.SubmitApplicationFor(
                application.CompanyData.Number.ToString(),
                _invoiceAmount,
                _advancePercentage);
            
        }
    }

    public class ConfidentialInvoiceWebServiceWrapper : ISubmitApplication
    {

        private readonly IConfidentialInvoiceService _confidentialInvoiceWebService;
        private readonly decimal _totalLedgerNetworth;
        private readonly decimal _advancePercentage;
        private readonly decimal _vatRate;

        public ConfidentialInvoiceWebServiceWrapper(IConfidentialInvoiceService confidentialInvoiceWebService, decimal totalLedgerNetworth, decimal advancePercentage, decimal vatRate)
        {
            _confidentialInvoiceWebService = confidentialInvoiceWebService;
            _totalLedgerNetworth = totalLedgerNetworth;
            _advancePercentage = advancePercentage;
            _vatRate = vatRate;
        }

        public int SubmitApplication(ISellerApplication application)
        {
            var result = _confidentialInvoiceWebService.SubmitApplicationFor(
                    new CompanyDataRequest
                    {
                        CompanyFounded = application.CompanyData.Founded,
                        CompanyNumber = application.CompanyData.Number,
                        CompanyName = application.CompanyData.Name,
                        DirectorName = application.CompanyData.DirectorName
                    }, _totalLedgerNetworth, _advancePercentage, _vatRate);

            return (result.Success) ? result.ApplicationId ?? -1 : -1;
        }
    }

    public class BusinessLoansServiceWrapper : ISubmitApplication
    {
        private readonly IBusinessLoansService _businessLoansService;
        private readonly decimal _interestRatePerAnnum;
        private readonly decimal _loanAmount;

        public BusinessLoansServiceWrapper(IBusinessLoansService businessLoansService, decimal interestRatePerAnnum, decimal loanAmount)
        {
            _businessLoansService = businessLoansService;
            _interestRatePerAnnum = interestRatePerAnnum;
            _loanAmount = loanAmount;
        }

        public int SubmitApplication(ISellerApplication application)
        {
            var result = _businessLoansService.SubmitApplicationFor(new CompanyDataRequest
            {
                CompanyFounded = application.CompanyData.Founded,
                CompanyNumber = application.CompanyData.Number,
                CompanyName = application.CompanyData.Name,
                DirectorName = application.CompanyData.DirectorName
            }, new LoansRequest
            {
                InterestRatePerAnnum = _interestRatePerAnnum,
                LoanAmount = _loanAmount
            });
            return (result.Success) ? result.ApplicationId ?? -1 : -1;
        }
    }
}
