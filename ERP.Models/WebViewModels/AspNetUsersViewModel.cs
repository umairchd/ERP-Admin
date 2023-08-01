using System.Collections.Generic;
using ERP.Models.DomainModels;
using ERP.Models.WebModels;

namespace ERP.Models.WebViewModels
{
    public class AspNetUsersViewModel
    {
        public AspNetUserModel AspNetUserModel { get; set; }
        public List<AspNetRole> Roles { get; set; }
    }
}