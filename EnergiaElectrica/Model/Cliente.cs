namespace EnergiaElectrica.Model
{
    public class Cliente
    {
        public long id { get; set; }
        public string TipoCliente { get; set; }
        public string TipoMedidor { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string NumeroContador { get; set; }
    }
}
