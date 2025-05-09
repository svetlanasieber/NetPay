namespace NetPay.Common
{
    public static class ValidationConstants
    {

        public const int HouseholdContactPersonMinLength = 5;
        public const int HouseholdContactPersonMaxLength = 50;

        public const int EmailMinLength = 6;
        public const int EmailMaxLength = 80;

        public const int PhoneNumberLength = 15;
        public const string PhoneNumberPattern = @"^\+\d{3}/\d{3}-\d{6}$";

      
        public const int ExpenceNameMinLength = 5;
        public const int ExpenceNameMaxLength = 50;

        public const string MinAmount = "0.01";
        public const string MaxAmount = "100000";

    
        public const int ServiceNameMinLength = 5;
        public const int ServiceNameMaxLength = 30;

      
        public const int SupplierNameMinLength = 3;
        public const int SupplierNameMaxLength = 60;
    }
} 
