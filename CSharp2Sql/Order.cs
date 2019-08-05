using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp2Sql {
    class Order {
        public int Id;
        public DateTime Date;
        public string Note;
        public int CustomerId;

        public Order(int id, DateTime date, string note, int customerid) {
            this.Id = id;
            this.Date = date;
            this.Note = note;
            this.CustomerId = customerid;
        }
        }
    
}
