﻿namespace Tienda.Models
{
    public class ProductoModel
    {
        public int Id { get; set; }
        public long CodigoBarra { get; set; }
        public string Nombre { get; set; }
        public string Marca { get; set; }
        public string Categoria { get; set; }
        public decimal Precio { get; set; }
    }
}
