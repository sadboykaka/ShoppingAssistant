using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAssistant.Views
{

    public class MDPMenuItem
    {
        public MDPMenuItem()
        {
        }
        public int Id { get; set; }
        public string IconSource { get; set; }

        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}