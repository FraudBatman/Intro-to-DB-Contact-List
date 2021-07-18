using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace WindowsFormsApp1
{
    public class Contact
    {
        public const int CONTACT_NUMBER = 200;

        private static string[] firstnames = new string[]{"James", "Mary", "Robert", "Patricia", "John", "Jennifer", "Michael", "Linda",
                "William", "Elizabeth", "David", "Barbara", "Richard", "Susan", "Joseph", "Jessica", "Thomas", "Sarah",
                "Charles", "Karen", "Christopher", "Nancy", "Daniel", "Lisa", "Matthew", "Betty", "Anthony", "Margaret",
                "Mark", "Sandra", "Donald", "Ashley", "Steven", "Kimberly", "Paul", "Emily", "Andrew", "Donna",
                "Joshua", "Michelle", "Kenneth", "Dorothy", "Kevin", "Carol", "Brian", "Amanda", "George", "Melissa",
                "Edward", "Deborah", "Ronald", "Stephanie", "Timothy", "Rebecca", "Jason", "Sharon", "Jeffrey", "Laura",
                "Ryan", "Cynthia", "Jacob", "Kathleen", "Gary", "Amy", "Nicholas", "Shirley", "Eric", "Angela",
                "Jonathan", "Helen", "Stephen", "Anna", "Larry", "Brenda", "Justin", "Pamela", "Scott", "Nicole",
                "Brandon", "Emma", "Benjamin", "Samantha", "Samuel", "Katherine", "Gregory", "Christine", "Frank",
                "Debra", "Alexander", "Rachel", "Raymond", "Catherine", "Patrick", "Carolyn", "Jack", "Janet", "Dennis",
                "Ruth", "Jerry", "Maria", "Tyler", "Heather", "Aaron", "Diane", "Jose", "Virginia", "Adam", "Julie",
                "Henry", "Joyce", "Nathan", "Victoria", "Douglas", "Olivia", "Zachary", "Kelly", "Peter", "Christina",
                "Kyle", "Lauren", "Walter", "Joan", "Ethan", "Evelyn", "Jeremy", "Judith", "Harold", "Megan", "Keith",
                "Cheryl", "Christian", "Andrea", "Roger", "Hannah", "Noah", "Martha", "Gerald", "Jacqueline", "Carl",
                "Frances", "Terry", "Gloria", "Sean", "Ann", "Austin", "Teresa", "Arthur", "Kathryn", "Lawrence",
                "Sara", "Jesse", "Janice", "Dylan", "Jean", "Bryan", "Alice", "Joe", "Madison", "Jordan", "Doris",
                "Billy", "Abigail", "Bruce", "Julia", "Albert", "Judy", "Willie", "Grace", "Gabriel", "Denise", "Logan",
                "Amber", "Alan", "Marilyn", "Juan", "Beverly", "Wayne", "Danielle", "Roy", "Theresa", "Ralph", "Sophia",
                "Randy", "Marie", "Eugene", "Diana", "Vincent", "Brittany", "Russell", "Natalie", "Elijah", "Isabella",
                "Louis", "Charlotte", "Bobby", "Rose", "Philip", "Alexis", "Johnny", "Kayla"
        };

        private static string[] lastnames = new string[] {"Smith","Johnson","Williams","Jones","Brown","Davis","Miller",
            "Wilson","Moore","Taylor","Anderson","Thomas","Jackson","White","Harris","Martin","Thompson","Garcia",
            "Martinez","Robinson","Clark","Rodriguez","Lewis","Lee","Walker","Hall","Allen","Young","Hernandez","King",
            "Wright","Lopez","Hill","Scott","Green","Adams","Baker","Gonzalez","Nelson","Carter","Mitchell","Perez",
            "Roberts","Turner","Phillips","Campbell","Parker","Evans","Edwards","Collins","Stewart","Sanchez","Morris",
            "Rogers","Reed","Cook","Morgan","Bell","Murphy","Bailey","Rivera","Cooper","Richardson","Cox","Howard",
            "Ward","Torres","Peterson","Gray","Ramirez","James","Watson","Brooks","Kelly","Sanders","Price","Bennett",
            "Wood","Barnes","Ross","Henderson","Coleman","Jenkins","Perry","Powell","Long","Patterson","Hughes",
            "Flores","Washington","Butler","Simmons","Foster","Gonzales","Bryant","Alexander","Russell","Griffin",
            "Diaz","Hayes"};

        private static string[] streets = new string[]
        {
            "Second", "Third", "First", "Fourth", "Park", "Fifth", "Main", "Sixth", "Oak",
            "Seventh", "Pine", "Maple", "Cedar", "Eighth", "Elm", "View", "Washington", "Ninth", "Lake", "Hill"
        };

        private static string[] streetEnd = new string[] {"Rd.", "Dr.", "Ln.", "Way", "Blvd.", "St."};

        private static int[] areacodes = new int[]
        {
            203, 860, 475, 959, 207, 339, 351, 413, 508, 617, 774, 781, 857, 978, 603, 201, 551, 609, 640, 732, 848,
            856, 862, 908, 973, 212, 315, 332, 347, 516, 518, 585, 607, 631, 646, 680, 716, 718, 838, 845, 914, 917,
            929, 934, 401, 215, 223, 267, 272, 412, 484, 610, 717, 724, 814, 878, 802, 217, 224, 309, 312, 331, 618,
            630, 708, 773, 779, 815, 847, 872, 219, 260, 317, 574, 765, 812, 930, 319, 515, 563, 641, 712, 316, 620,
            785, 913, 231, 248, 269, 313, 517, 586, 616, 734, 810, 906, 947, 989, 679, 218, 320, 507, 612, 651, 763,
            952, 314, 417, 573, 636, 660, 816, 308, 402, 531, 701, 216, 234, 326, 330, 380, 419, 440, 513, 567, 614,
            740, 937, 220, 605, 262, 414, 608, 534, 715, 920, 274, 205, 251, 256, 334, 938, 479, 501, 870, 302, 239,
            305, 321, 352, 386, 407, 561, 686, 727, 754, 772, 786, 813, 850, 863, 904, 941, 954, 229, 404, 470, 478,
            678, 706, 762, 770, 912, 270, 364, 502, 606, 859, 225, 318, 337, 504, 985, 240, 301, 410, 443, 667, 228,
            601, 662, 769, 252, 336, 704, 743, 828, 910, 919, 980, 984, 405, 580, 918, 539, 803, 843, 854, 864, 423,
            615, 629, 731, 865, 901, 931, 210, 214, 254, 281, 325, 346, 361, 409, 430, 432, 469, 512, 682, 713, 726,
            737, 806, 817, 830, 832, 903, 915, 936, 940, 956, 972, 979, 276, 434, 540, 571, 703, 757, 804, 304, 681
        };

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; } 
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        public static Contact[] CreateContacts()
        {
            Contact[] returnContacts = new Contact[CONTACT_NUMBER];
            Random random = new Random();
            for (int i = 0; i < CONTACT_NUMBER; i++)
            {
                returnContacts[i] = new Contact();
                returnContacts[i].FirstName = firstnames[random.Next(firstnames.Length)];
                returnContacts[i].LastName = lastnames[random.Next(lastnames.Length)];
                returnContacts[i].Birthday =
                    new DateTime(random.Next(1950, 2003), random.Next(1, 13), random.Next(1, 29));
                returnContacts[i].PhoneNumber = $"({areacodes[random.Next(areacodes.Length)]})-XXX-XXXX";
                returnContacts[i].Address =
                    $"{random.Next(1, 8001)} {streets[random.Next(streets.Length)]} {streetEnd[random.Next(streetEnd.Length)]}";
            }

            return returnContacts;
        }

        public static Contact[] ReadContacts(SQLiteDataReader reader, int radio)
        {
            var contacts = new List<Contact>();
            while (reader.Read())
            {
                var contact = new Contact();
                contact.FirstName = reader.GetString(1);
                contact.LastName = reader.GetString(2);
                contact.Birthday = DateTime.Parse(reader.GetString(3));
                contact.Address = reader.GetString(6);
                contact.PhoneNumber = reader.GetString(9);
                contacts.Add(contact);
            }

            return contacts.ToArray();
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName}, {Birthday.ToShortDateString()} | {Address} | {PhoneNumber}";
        }
    }
}