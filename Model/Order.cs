using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class Order
    {
        public Order() { }
        public Order(DateTime date, DateTime wait, DateTime deadline,
            bool sended, bool lated) :this()
        {
            this.date_of_order = date;
            this.wait_time = wait;
            this.deadline = deadline;
            this.isSended= sended;
            this.isLate= lated;
        }
        public virtual Product id { get; set; }
        public virtual Account account { get; set; }
        public DateTime date_of_order { get; set; }
        public DateTime? wait_time {  get; set; }
        public DateTime deadline { get; set; }
        public bool isSended {  get; set; }
        public bool isLate  { get; set; }
    }
}
