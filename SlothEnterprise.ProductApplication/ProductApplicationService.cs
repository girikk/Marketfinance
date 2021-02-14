using System;
using SlothEnterprise.External;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Products;

namespace SlothEnterprise.ProductApplication
{
    public class ProductApplicationService : IProductApplicationService
    {
        private readonly ISubmitApplication _submitApplicationService;


        public ProductApplicationService(ISubmitApplication submitApplicationService)
        {
            _submitApplicationService = submitApplicationService;
        }

        public int SubmitApplicationFor(ISellerApplication application)
        {
            return _submitApplicationService.SubmitApplication(application);
        }
    }
}
