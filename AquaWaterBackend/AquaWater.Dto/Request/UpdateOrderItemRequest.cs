﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaWater.Dto.Request
{
    public class UpdateOrderItemRequest
    {
        
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
       
    }
}
