using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingPortal.Infrastructure.Extensions.Helpers
{
    public static class TemplateResolverHelper
    {
        public static string ResolveTemplate(string templateContent, Dictionary<string, object> values)
        {
            foreach (var kvp in values)
            {
                string placeholder = "{" + kvp.Key + "}";
                templateContent = templateContent.Replace(placeholder, kvp.Value.ToString());
            }
            return templateContent;
        }
    }
}
