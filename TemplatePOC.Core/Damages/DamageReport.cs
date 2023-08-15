using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplatePOC.Core.Damages
{
    public class DamageReport
    {
        public int Id { get; set; }
        public double ValueOfCargo { get; set; }
        public double DamageEstimate { get; set; }
    }
}
