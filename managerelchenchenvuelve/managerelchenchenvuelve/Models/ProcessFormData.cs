using System.ComponentModel.DataAnnotations;

namespace managerelchenchenvuelve.Models
{
    public class ProcessFormData
    {
        [Required]
        public string CodigoDeSolicitud { get; set; }

        [Required]
        public string GestionRealizada { get; set; }

        [Required]
        public string TipoFormulario { get; set; }

        public string? Razon { get; set; }

        public string? TipoAtencion { get; set; }
    }
} 