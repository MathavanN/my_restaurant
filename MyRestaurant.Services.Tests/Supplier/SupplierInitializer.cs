using MyRestaurant.Core;
using MyRestaurant.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyRestaurant.Services.Tests
{
    public class SupplierInitializer
    {
        public static void Initialize(MyRestaurantContext context)
        {
            if (!context.Suppliers.Any())
            {
                var suppliers = new List<Supplier>
                {
                    new Supplier
                    {
                        Name = "ABC Pvt Ltd", Address1 = "American Mission School Road", Address2 = "Madduvil South",
                        City = "Chavakachcheri", Country = "Sri Lanka", Telephone1 = "0765554345", Telephone2 = "0766554567", 
                        Fax = "", Email = "goldendining2010@gmail.com", ContactPerson = "James"
                    },
                    new Supplier
                    {
                        Name = "VBT Pvt Ltd", Address1 = "VBT Road", Address2 = "VBTt",
                        City = "Jaffna", Country = "Sri Lanka", Telephone1 = "0777113644", Telephone2 = "",
                        Fax = "", Email = "test@test.com", ContactPerson = "James"
                    },
                    new Supplier
                    {
                        Name = "Defense & Space", Address1 = "170 Blick Trail", Address2 = "New Hampshire",
                        City = "West Camrynport", Country =  "Colombia", Telephone1 = "679-846-5956", Telephone2 = "523-275-0788",
                        Fax = "971-812-9638", Email = "Tyra_Schaden0@gmail.com", ContactPerson = "Deanna"
                    },
                    new Supplier
                    {
                        Name = "Dairy", Address1 = "941 Jarod Camp", Address2 = "Wyoming",
                        City = "Charleston", Country = "New Caledonia", Telephone1 = "912-369-3329", Telephone2 = "090-234-3464",
                        Fax = "934-688-9150", Email = "Napoleon_Stanton@hotmail.com", ContactPerson = "Nicklaus"
                    },
                    new Supplier
                    {
                        Name = "Construction", Address1 = "95102 Rogahn Loaf", Address2 = "Wyoming",
                        City = "Charleston", Country = "New Caledonia", Telephone1 = "050-853-8227", Telephone2 = "515-785-8383",
                        Fax = "928-570-9780", Email = "Elza_Larson34@gmail.com", ContactPerson = "Larissa"
                    },
                    new Supplier
                    {
                        Name = "Recreational", Address1 = "73429 Richard Course", Address2 = "South Carolina",
                        City = "East Constantin", Country = "Ecuador", Telephone1 = "832-319-6315", Telephone2 = "586-173-3164",
                        Fax = "477-916-9881", Email = "Floy89@hotmail.com", ContactPerson = "Lue"
                    },
                    new Supplier
                    {
                        Name = "Accounting", Address1 = "55693 Berge Walk", Address2 = "Alabama",
                        City = "Mullerchester", Country = "Sudan", Telephone1 = "192-637-6273", Telephone2 = "074-655-7364",
                        Fax = "311-587-2726", Email = "Alta.Bechtelar66@gmail.com", ContactPerson = "Emmalee"
                    },
                    new Supplier 
                    {
                        Name = "Political Organization", Address1 = "58204 Renner Divide", Address2 = "Maryland",
                        City = "North Nikko", Country = "Gabon", Telephone1 = "538-881-6913", Telephone2 = "932-546-7844",
                        Fax = "173-733-1304", Email = "Hudson_Pfannerstill54@yahoo.com", ContactPerson = "Prudence"
                    },
                    new Supplier
                    {
                        Name = "Automotive", Address1 = "424 Cesar Walks", Address2 = "New Mexico",
                        City = "Port Manleyland", Country = "Cyprus", Telephone1 = "188-694-9543", Telephone2 = "485-188-2440",
                        Fax = "203-944-7683", Email = "Lavinia_Feil@gmail.com", ContactPerson = "Audie"
                    },
                    new Supplier
                    {
                        Name = "Executive Office", Address1 = "32433 Shaun Inlet", Address2 = "Florida", 
                        City = "Lueilwitzmouth", Country = "Switzerland", Telephone1 = "908-867-1023", Telephone2 = "730-452-3702",
                        Fax = "436-099-9286", Email = "Armand65@hotmail.com", ContactPerson = "Candace"
                    },
                    new Supplier
                    {
                        Name = "Architecture & Planning", Address1 = "51220 Quigley Estate", Address2 = "Hawaii",
                        City = "Marianaside", Country = "Burkina Faso", Telephone1 = "768-250-5153", Telephone2 = "812-707-9313",
                        Fax = "814-374-0668", Email = "Geovanny92@yahoo.com", ContactPerson = "Dawson"
                    },
                    new Supplier
                    {
                        Name = "Renewables & Environment", Address1 = "09101 Alvera Rapids", Address2 = "Kentucky",
                        City = "Marcusside", Country = "Armenia", Telephone1 = "071-193-7154", Telephone2 = "587-668-3800",
                        Fax = "835-177-1774", Email = "Vivian.Feest76@hotmail.com", ContactPerson = "Marilou"
                    },
                    new Supplier
                    {
                        Name = "Philanthropy", Address1 = "5697 Collins Spurs", Address2 = "Mississippi",
                        City = "Eastvale", Country = "Dominican Republic", Telephone1 = "344-135-7771", Telephone2 = "459-745-5501",
                        Fax = "361-907-8030", Email = "Leda3@yahoo.com", ContactPerson = "Coralie"
                    },
                    new Supplier
                    {
                        Name = "Executive Office", Address1 = "6847 Steuber Mount", Address2 = "Minnesota",
                        City = "Fargo", Country = "Vietnam", Telephone1 = "879-576-7067", Telephone2 = "333-751-8481",
                        Fax = "415-199-1971", Email = "Regan_Dickens@gmail.com", ContactPerson = "Julianne"
                    },
                    new Supplier
                    {
                        Name = "Utilities", Address1 = "3818 McCullough Ridges", Address2 = "Missouri",
                        City = "North Highlands", Country = "Somalia", Telephone1 = "286-643-2457",  Telephone2 = "669-049-9251",
                        Fax = "487-362-9802", Email = "Lizzie25@hotmail.com", ContactPerson = "Johnnie"
                    },
                    new Supplier
                    {
                        Name = "Civil Engineering", Address1 = "7658 Carey Ways", Address2 = "Louisiana",
                        City = "Hellerbury", Country = "Mali", Telephone1 = "096-069-1543", Telephone2 = "085-480-9084",
                        Fax = "294-839-6064", Email = "Chance_Koelpin79@hotmail.com", ContactPerson = "Dariana"
                    },
                    new Supplier
                    {
                        Name = "Tobacco", Address1 = "7729 Marietta Estate", Address2 = "Illinois",
                        City = "Lake Evans", Country = "Afghanistan", Telephone1 = "789-029-7980", Telephone2 = "678-944-6380",
                        Fax = "293-028-8740", Email = "Sydnie0@hotmail.com", ContactPerson = "Stephen"
                    },
                    new Supplier
                    {
                        Name = "Translation & Localization", Address1 = "80228 Feest Loaf", Address2 = "Tennessee",
                        City = "Trantowshire", Country = "Pakistan", Telephone1 = "901-358-0826", Telephone2 = "660-927-3749",
                        Fax = "125-987-0988", Email = "Rasheed19@yahoo.com", ContactPerson = "Ivy"
                    },
                    new Supplier
                    {
                        Name = "Graphic Design", Address1 = "62070 Gabriella Key", Address2 = "Kansas",
                        City = "Harveymouth", Country = "Tuvalu", Telephone1 = "732-060-9477", Telephone2 = "162-490-2457",
                        Fax = "472-110-7266", Email = "Dominic.Zulauf75@gmail.com", ContactPerson = "Efrain"
                    },
                    new Supplier
                    {
                        Name = "Entertainment", Address1 = "163 Paula Plaza", Address2 = "Massachusetts",
                        City = "Genevievebury", Country = "Switzerland", Telephone1 = "983-958-1244", Telephone2 = "973-925-5438",
                        Fax = "039-246-0901", Email = "Orland.Hoppe@yahoo.com", ContactPerson = "Silas"
                    },
                    new Supplier
                    {
                        Name = "Executive Office", Address1 = "7227 Schroeder Club", Address2 = "Hawaii",
                        City = "Lombard", Country = "Norway", Telephone1 = "146-035-4135", Telephone2 = "079-664-5145",
                        Fax = "455-125-3578", Email = "Adelle.Abshire@hotmail.com", ContactPerson = "Hayden"
                    },
                    new Supplier 
                    {
                        Name = "Computer Software", Address1 = "8084 Noe Highway", Address2 = "South Dakota",
                        City = "New Leonieshire", Country = "Yemen", Telephone1 = "719-837-6088", Telephone2 = "493-282-1487",
                        Fax = "583-786-1752", Email = "Elody92@gmail.com", ContactPerson = "Macy"
                    },
                    new Supplier 
                    {
                        Name = "Banking", Address1 = "56290 Kovacek Underpass", Address2 = "Arkansas",
                        City = "North Richland Hills", Country = "Guatemala", Telephone1 = "967-857-5255", Telephone2 = "535-449-7505",
                        Fax = "850-149-5994", Email = "Savanah.Denesik36@hotmail.com", ContactPerson = "Shanie"
                    },
                    new Supplier
                    {
                        Name = "Wholesale", Address1 = "339 Daphnee Turnpike", Address2 = "Nebraska",
                        City = "Hellerland", Country = "Palau", Telephone1 = "431-705-1593", Telephone2 = "793-778-6576",
                        Fax = "683-841-3526", Email = "Florida_Stanton@hotmail.com", ContactPerson = "Evangeline"
                    },
                    new Supplier
                    {
                        Name = "Nonprofit Organization Management", Address1 = "706 Kertzmann Port", Address2 = "Florida",
                        City = "West Brooke", Country = "Palestinian Territory", Telephone1 = "775-366-3840", Telephone2 = "159-564-5886",
                        Fax = "167-239-3145", Email = "Maye55@hotmail.com", ContactPerson = "Asha"
                    },
                    new Supplier
                    {
                        Name = "Entertainment", Address1 = "0374 Jocelyn Springs", Address2 = "Iowa",
                        City = "Tacoma", Country = "Sri Lanka", Telephone1 = "574-031-4037", Telephone2 = "044-453-9539",
                        Fax = "998-167-7334", Email = "Adela.Wiegand86@yahoo.com", ContactPerson = "Demario"
                    },
                    new Supplier
                    {
                        Name = "Computer Software", Address1 = "41108 Wuckert Harbor", Address2 = "Tennessee",
                        City = "Lake Valentin", Country = "Aruba", Telephone1 = "933-064-1967", Telephone2 = "123-209-5865",
                        Fax = "936-416-0079", Email = "Ole_Koelpin65@gmail.com", ContactPerson = "Karley"
                    }
                };
                context.Suppliers.AddRange(suppliers);
                context.SaveChanges();
            }
        }
    }
}
