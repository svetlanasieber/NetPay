using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static NetPay.Common.EntityValidationConstants.Service;

namespace NetPay.Data.Models
{
    public class Service
    {
        public Service()
        {
            this.Expenses = new HashSet<Expense>();
            this.SuppliersServices = new HashSet<SupplierService>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(ServiceNameMaxLength, MinimumLength = ServiceNameMinLength)]
        public string ServiceName { get; set; } = null!;

        public virtual ICollection<Expense> Expenses { get; set; }
        public virtual ICollection<SupplierService> SuppliersServices { get; set; }
    }
} 