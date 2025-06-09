using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class Cart
    {
        public Cart(bool ordered) {
            this.isOrdered = ordered;
        }
        public virtual Product id { get; set; }
        public virtual Account account { get; set; }
        public bool isOrdered { get; set; }
    }
}
