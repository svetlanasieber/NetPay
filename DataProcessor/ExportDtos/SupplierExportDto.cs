using Newtonsoft.Json;

namespace NetPay.DataProcessor.ExportDtos
{
    public class SupplierExportDto
    {
        [JsonProperty("SupplierName")]
        public string SupplierName { get; set; }
    }
} 