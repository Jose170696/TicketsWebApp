using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TicketsWebApp.Models
{
    public class Tiquetes
    {
        [Key]
        public int ti_identificador { get; set; }

        [Required]
        [StringLength(150)]
        public string ti_asunto { get; set; }
        [Required]
        public int ti_ca_id { get; set; }
        [Required]
        public int ti_us_id_asigna { get; set; }
        [Required]
        public int ti_ur_id { get; set; }
        [Required]
        public int ti_im_id { get; set; }

        [Required]
        [StringLength(1)]
        public string ti_estado { get; set; }

        [Required]
        [StringLength(150)]
        public string ti_solucion { get; set; }

        public DateTime ti_fecha_adicion { get; set; } = DateTime.Now;

        [Required]
        [StringLength(10)]
        public string ti_adicionado_por { get; set; }

        public DateTime? ti_fecha_modificacion { get; set; }

        public string? ti_modificado_por { get; set; }

    }
}
