﻿using System.Collections.Generic;
using ERP.Models.MenuModels;

namespace ERP.Models.DomainModels
{
    public partial class AspNetRole
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<AspNetUser> AspNetUsers{ get; set; }
        public virtual ICollection<MenuRight> MenuRights { get; set; }
    }
}
