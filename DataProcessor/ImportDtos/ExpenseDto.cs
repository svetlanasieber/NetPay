using NetPay.Common;
using NetPay.Data.Models.Enums;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace NetPay.DataProcessor.ImportDtos
{
    [JsonObject("Expense")]
    public class ExpenseDto
    {
        [Required]
        [StringLength(ValidationConstants.ExpenceNameMaxLength, MinimumLength = ValidationConstants.ExpenceNameMinLength)]
        [JsonProperty("ExpenseName")]
        public string ExpenseName { get; set; } = null!;

        [Required]
        [Range(typeof(decimal), ValidationConstants.MinAmount, ValidationConstants.MaxAmount)]
        [JsonProperty("Amount")]
        public decimal Amount { get; set; }

        [Required]
        [JsonProperty("DueDate")]
        public string DueDate { get; set; } = null!;

        [Required]
        [JsonProperty("PaymentStatus")]
        [EnumDataType(typeof(PaymentStatus))]
        public string PaymentStatus { get; set; } = null!;

        [JsonProperty("HouseholdId")]
        [Required]
        public int HouseholdId { get; set; }

        [JsonProperty("ServiceId")]
        [Required]
        public int ServiceId { get; set; }
    }
} 