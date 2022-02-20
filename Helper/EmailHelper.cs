using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MyJobsApp.Helper
{
    public static class EmailHelper
    {
        public static async Task<string> GetEmailTemplate(string path)
        {
            string bodyContent;
            using (var reader = new StreamReader(path))
            {
                bodyContent = await reader.ReadToEndAsync();
            }

            return bodyContent;
        }

        public static async Task<string> EmailTemplateBuilder(string pathEmailHeader, string pathEmailBody,
            params object[] parameters)
        {
            var sb = new StringBuilder();

            var header = await GetEmailTemplate(pathEmailHeader);
            var body = await GetEmailTemplate(pathEmailBody);
            var newBody = string.Format(body, parameters);
            const string head = "<!DOCTYPE html>\n"
                                + "<html lang = \"en\">";
            const string end = "</html>";

            sb.Append(head);
            sb.Append(header);
            sb.Append(newBody);
            sb.Append(end);

            return sb.ToString();
        }
    }
}