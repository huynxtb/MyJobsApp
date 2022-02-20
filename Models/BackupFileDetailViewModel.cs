using System;

namespace MyJobsApp.Models
{
    public class BackupFileDetailViewModel
    {
        public string BackupFileId { get; set; }
        public string BackupFileName { get; set; }
        public string BackupFileUrl { get; set; }
        public string Website { get; set; }
        public string DatabaseName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}