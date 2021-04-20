using System;
using System.Collections.Generic;
using System.Text;

namespace Uplift.Models.ViewModels
{
    public class CartViewModel
    {

        public List<Service> ServicesList { get; set; }

        public OrderHeader OrderHeader { get; set; }
    }
}
