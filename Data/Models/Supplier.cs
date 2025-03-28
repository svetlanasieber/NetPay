using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static NetPay.Common.EntityValidationConstants.Supplier;

namespace NetPay.Data.Models
{
    public class Supplier
    {
        public Supplier()
        {
            this.SuppliersServices = new HashSet<SupplierService>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(SupplierNameMaxLength, MinimumLength = SupplierNameMinLength)]
        public string SupplierName { get; set; } = null!;

        public virtual ICollection<SupplierService> SuppliersServices { get; set; }
    }
} 