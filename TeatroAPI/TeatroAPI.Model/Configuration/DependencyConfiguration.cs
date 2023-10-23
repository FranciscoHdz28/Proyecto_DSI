using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeatroAPI.Model.Configuration
{
    public class DependencyConfiguration
    {
        public string From { get; set; }
        public string To { get; set; }
        public string DependencyType { get; set; }
    }
}
