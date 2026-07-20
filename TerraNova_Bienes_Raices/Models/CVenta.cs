namespace TerraNova_Bienes_Raices.Models
{
    public class CVenta
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public int IdPropiedad { get; set; }
        public int IdTrabajador { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public decimal Monto { get; set; }
        public decimal ComisionGenerada { get; set; }
        public string FormaPago { get; set; } = "Contado";  // Contado, Crédito, Financiado
        public string Estado { get; set; } = "Completada";  // Completada, En proceso, Cancelada
        public string Observaciones { get; set; } = string.Empty;
    }
}
