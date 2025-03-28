using Newtonsoft.Json;

namespace NetPay.DataProcessor.ExportDtos
{
    public class ServiceExportDto
    {
        [JsonProperty("ServiceName")]
        public string ServiceName { get; set; }

        [JsonProperty("Suppliers")]
        public SupplierExportDto[] Suppliers { get; set; }
    }
} 