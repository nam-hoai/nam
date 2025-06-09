using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class Account
    {
        public Account() {
            Orders = new HashSet<Order>();
            Carts = new HashSet<Cart>();
        }
        public Account(string name, string pass, string full, string address, 
            string dob, string email, string phone, string id, bool check) {
            this.account_name = name;
            this.password = pass;
            this.full_name = full;
            this.address = address;
            this.date_of_birth = dob;
            this.email = email;
            this.number_phone = phone;
            this.role_id = id;
            this.isActive = check;
        }
        public string account_name { get; set; }
        public string password { get; set; }
        public string? full_name { get; set; }
        public string? address { get; set; }
        public string? date_of_birth { get; set; }
        public string? email { get; set; }
        public string? number_phone { get; set; }
        public string role_id { get; set; }
        public bool isActive { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
    }
}
