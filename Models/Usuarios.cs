using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TicketsWebApp.Models
{
    public class Usuarios
    {
        [Key]
        public int us_identificador { get; set; }

        [Required]
        [StringLength(150)]
        public string us_nombre_completo { get; set; }

        [Required]
        [StringLength(150)]
        public string us_correo { get; set; }

        [Required]
        [StringLength(255)]
        public string us_clave { get; set; }

        [Required]
        public int us_ro_identificador { get; set; }

        [Required]
        [StringLength(1)]
        public string us_estado { get; set; }

        public DateTime us_fecha_adicion { get; set; }

        [Required]
        [StringLength(10)]
        public string us_adicionado_por { get; set; }

        public DateTime? us_fecha_modificacion { get; set; }

        public string? us_modificado_por { get; set; }
    }
}
