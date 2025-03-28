using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetPay.Data.Models
{
    public class SupplierService
    {
        [Key]
        [Column(Order = 0)]
        public int SupplierId { get; set; }

        [Key]
        [Column(Order = 1)]
        public int ServiceId { get; set; }

        public virtual Supplier Supplier { get; set; } = null!;
        public virtual Service Service { get; set; } = null!;
    }
} 