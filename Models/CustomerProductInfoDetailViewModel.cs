namespace MyJobsApp.Models
{
    public class CustomerProductInfoDetailViewModel
    {
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public double PaymentAmount { get; set; }
        public string DatabaseName { get; set; }
        public string VpsIPAddress { get; set; }
        public string VpsLocation { get; set; }
        public string VpsCPU { get; set; }
        public string VpsRam { get; set; }
        public string VpsTransfer { get; set; }
        public string VpsStorage { get; set; }
        public string ExpirationDate { get; set; }
        public string BackupType { get; set; }
    }
}