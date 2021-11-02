using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp3
{
    class Seat
    {
        public int SeatNo { get; set; }
        public string SeatKind { get; set; }
        public int SeatPrice { get; set; }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public bool Used { get; set; }
        public DateTime UsedAt { get; set; }

    }
}
