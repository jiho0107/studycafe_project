using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WindowsFormsApp3
{
    class DataManager
    {
        public static List<User> Users = new List<User>();
        public static List<Seat> Seats = new List<Seat>();
        static DataManager() { Load(); }
        public static void Load()
        {
            try
            {
                string usersOutput = File.ReadAllText(@"./users.xml");
                XElement usersXElement = XElement.Parse(usersOutput);
                Users = (from item in usersXElement.Descendants("user")
                         select new User()
                         {
                             Id = int.Parse(item.Element("id").Value),
                             Name = item.Element("name").Value,
                             Charge = int.Parse(item.Element("charge").Value)
                         }).ToList<User>();

                string seatsOutput = File.ReadAllText(@"./seats.xml");
                XElement seatsXElement = XElement.Parse(seatsOutput);
                Seats = (from item in seatsXElement.Descendants("seat")
                         select new Seat()
                         {
                             SeatNo = int.Parse(item.Element("seatNo").Value),
                             SeatKind = item.Element("seatKind").Value,
                             SeatPrice = int.Parse(item.Element("seatPrice").Value),
                             UserId = int.Parse(item.Element("userId").Value),
                             UserName = item.Element("userName").Value,
                             Used = item.Element("used").Value != "0" ? true : false,
                             UsedAt = DateTime.Parse(item.Element("usedAt").Value)
                         }).ToList<Seat>();
            }
            catch (FileNotFoundException e)
            {
                Save();
            }
        }
        public static void Save()
        {
            string seatsOutput = "";
            seatsOutput += "<seats>\n";
            foreach (var item in Seats)
            {
                seatsOutput += "<seat>\n";

                seatsOutput += "<seatNo>" + item.SeatNo + "</seatNo>\n";
                seatsOutput += "<seatKind>" + item.SeatKind + "</seatKind>\n";
                seatsOutput += "<seatPrice>" + item.SeatPrice + "</seatPrice>\n";
                seatsOutput += "<userId>" + item.UserId + "</userId>\n";
                seatsOutput += "<userName>" + item.UserName + "</userName>\n";
                seatsOutput += "<used>" + (item.Used ? 1 : 0) + "</used>\n";
                seatsOutput += "<usedAt>" + item.UsedAt.ToLongDateString() + "</usedAt>\n";

                seatsOutput += "</seat>\n";
            }
            seatsOutput += "</seats>";

            string usersOutput = "";
            usersOutput += "<users>\n";
            foreach (var item in Users)
            {
                usersOutput += "<user>\n";
                usersOutput += "<id>" + item.Id + "</id>\n";
                usersOutput += "<name>" + item.Name + "</name>\n";
                usersOutput += "<charge>" + item.Charge + "</charge>\n";
                usersOutput += "</user>\n";
            }
            usersOutput += "</users>";

            File.WriteAllText(@"./seats.xml", seatsOutput);
            File.WriteAllText(@"./users.xml", usersOutput);
        }
    }
}
