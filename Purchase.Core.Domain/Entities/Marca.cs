﻿using System;
using System.Collections.Generic;

namespace Purchase.Core.Domain.Entities
{
    public partial class Marca
    {
        public Marca()
        {
            Productos = new HashSet<Producto>();
        }

        public long IdMarca { get; set; }
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
