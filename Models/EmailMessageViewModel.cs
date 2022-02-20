using System.Collections.Generic;

namespace MyJobsApp.Models
{
    public class EmailMessageViewModel
    {
        public List<string> SendTos { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}
