﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Fct.Domain.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
    }
}
