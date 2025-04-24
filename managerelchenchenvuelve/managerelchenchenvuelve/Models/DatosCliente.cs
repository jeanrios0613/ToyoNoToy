namespace managerelchenchenvuelve.Models
{
    public class DatosCliente
    {
        private string? id;

        public string? Id { get => id; set => id = value; }

        public string? name { get; set; }

        public string? description { get; set; }

        public string? type { get; set; }

        public DateTime? created { get; set; }

        public DateTime? updated { get; set; }

        public string? reference {  get; set; }
 
    }
}
