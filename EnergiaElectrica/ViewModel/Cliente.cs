namespace EnergiaElectrica.ViewModel
{
    public class Cliente
    {
        public long id { get; set; }
        public byte tipo { get; set; }
        public string tipoNombre { get; set; }
        public byte medidor { get; set; }
        public string medidorNombre { get; set; }
        public string correo { get; set; }
        public string nombre { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string numeroContador { get; set; }
    }
}
