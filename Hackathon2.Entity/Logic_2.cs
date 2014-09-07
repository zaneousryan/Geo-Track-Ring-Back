using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon2.Entity
{
    public partial class Logic
    {
        private static Tuple<string, decimal, decimal>[] _PossibleLocations =
        {
            new Tuple<string, decimal, decimal>("Imperial Palace Hotel and Casino",(decimal)36.118,(decimal)-115.172),
            new Tuple<string, decimal, decimal>("Harrah's Las Vegas",(decimal)36.1194,(decimal)-115.1725),
            new Tuple<string, decimal, decimal>("Flamingo Las Vegas",(decimal)36.1157,(decimal)-115.169799),
            new Tuple<string, decimal, decimal>("Excalibur",(decimal)36.0998,(decimal)-115.1747),
            new Tuple<string, decimal, decimal>("Circus Circus Las Vegas",(decimal)36.1368,(decimal)-115.1635),
            new Tuple<string, decimal, decimal>("Caesars Palace",(decimal)36.116,(decimal)-115.174),
            new Tuple<string, decimal, decimal>("Bill's Gamblin' Hall and Saloon",(decimal)36.1098,(decimal)-115.173),
            new Tuple<string, decimal, decimal>("Bellagio",(decimal)36.111,(decimal)-115.1731),
            new Tuple<string, decimal, decimal>("Ballys Las Vegas",(decimal)36.1138,(decimal)-115.1708),
            new Tuple<string, decimal, decimal>("Luxor Hotel",(decimal)36.0943,(decimal)-115.1755),
            new Tuple<string, decimal, decimal>("MGM Grand Hotel and Casino",(decimal)36.1013,(decimal)-115.1689),
            new Tuple<string, decimal, decimal>("Mandalay Bay",(decimal)36.0918,(decimal)-115.1737),
            new Tuple<string, decimal, decimal>("Monte Carlo Resort and Casino",(decimal)36.1043,(decimal)-115.1729),
            new Tuple<string, decimal, decimal>("New York-New York Hotel & Casino",(decimal)36.1014,(decimal)-115.1748),
            new Tuple<string, decimal, decimal>("Paris Las Vegas",(decimal)36.1116,(decimal)-115.172),
            new Tuple<string, decimal, decimal>("Planet Hollywood Resort & Casino",(decimal)36.1086,(decimal)-115.17)
        };

        private List<Tuple<string, decimal, decimal>> _PossibleLocationsSorted;
        public List<Tuple<string, decimal, decimal>> PossibleLocationsSorted
        {
            get
            {
                if (_PossibleLocationsSorted == null)
                {
                    _PossibleLocationsSorted = _PossibleLocations.OrderBy(x => x.Item1).ToList();
                }
                return _PossibleLocationsSorted;
            }
        }

    }
}


//        {
//        "latitude": "36.1429",
//        "longitude": "-115.1578",
//        "name": "Sahara Hotel and Casino Las Vegas",
//        "description": "The Sahara Hotel and Casino opened its doors in 1952 and quickly became one of Las Vegas' most exciting destinations. Featuring the era's most celebrated performers, including Ann-Margret, Johnny Carson and Tina Turner. The Sahara set the standard in Las Vegas' growing entertainment scene. The Sahara blazed new ground by inviting the Beatles to Las Vegas for the very first time. The Sahara offers 1,720 classically styled guestrooms and suites decorated in a Moroccan motif.",
//        "url": "http://www.orbitz.com/App/ViewSpecificHotelLP?masterId=120768",
//        "stars": "3",
//        "address1": "2535 Las Vegas Boulevard South",
//        "address2": "",
//        "city": "Las Vegas",
//        "state": "NV",
//        "postalcode": ""

//    },
//        {
//        "latitude": "36.0918",
//        "longitude": "-115.1737",
//        "name": "THEhotel at Mandalay Bay",
//        "description": "THEhotel at Mandalay Bay has forever changed the perception of Las Vegas with its warm earthy tones, rich wood architecture and of course its modern contemporary feel. Featuring 1,117 of the largest standard suite rooms on The Strip, THEhotel at Mandalay Bay is THEdestination for work, and for play.",
//        "url": "http://www.orbitz.com/App/ViewSpecificHotelLP?masterId=67541",
//        "stars": "5",
//        "address1": "3950 Las Vegas Boulevard South",
//        "address2": "",
//        "city": "Las Vegas",
//        "state": "NV",
//        "postalcode": ""

//    },
//        {
//        "latitude": "36.1221",
//        "longitude": "-115.1728",
//        "name": "The Mirage Hotel and Casino",
//        "description": "Set amidst lush foliage, towering waterfalls and sparkling lagoons, The Mirage is a South Seas oasis, offering the serenity of the tropics and the excitement of Las Vegas. A fiery volcano welcomes you to this AAA-Four Diamond resort featuring 2,763 deluxe rooms and 281 luxuriously appointed suites.",
//        "url": "http://www.orbitz.com/App/ViewSpecificHotelLP?masterId=13375",
//        "stars": "4",
//        "address1": "3400 Las Vegas Boulevard South",
//        "address2": "",
//        "city": "Las Vegas",
//        "state": "NV",
//        "postalcode": ""

//    },
//        {
//        "latitude": "36.1209",
//        "longitude": "-115.1704",
//        "name": "The Venetian Hotel",
//        "description": "The Venetian is the first all-suites hotel on the \"Strip\" with 4,027 suites (standard room averages 700 square feet), a gaming facility of 120,000 square feet, and The Venetian Congress Center of 500,000 square feet - all connected to the existing Sands Expo and Convention Center. \"The Grand Canal Shoppes,\" the indoor mall area, 500,000 square feet, features a one-quarter mile Venetian streetscape and a \"Grand Canal\" running in length with functional gondolas, singing gondoliers and waterside cafes by authentically-styled Venetian bridges",
//        "url": "http://www.orbitz.com/App/ViewSpecificHotelLP?masterId=24111",
//        "stars": "5",
//        "address1": "3355 Las Vegas Boulevard South",
//        "address2": "",
//        "city": "Las Vegas",
//        "state": "NV",
//        "postalcode": ""

//    },
//        {
//        "latitude": "36.1241",
//        "longitude": "-115.1714",
//        "name": "Treasure Island - TI",
//        "description": "Treasure Island \"TI\" offers casual elegance with a high-energy atmosphere featuring AAA Four Diamond award winning service. Experience the sensual Sirens of TI in a battle of the sexes with a band of renegade pirates at Sirens' Cove. VIP viewing is exclusive to TI hotel guests in these free, live performances each night on the Strip.",
//        "url": "http://www.orbitz.com/App/ViewSpecificHotelLP?masterId=25675",
//        "stars": "4",
//        "address1": "3300 Las Vegas Boulevard South",
//        "address2": "",
//        "city": "Las Vegas",
//        "state": "NV",
//        "postalcode": ""

//    },
//        {
//        "latitude": "36.1",
//        "longitude": "-115.1697",
//        "name": "Tropicana Hotel and Casino Las Vegas",
//        "description": "Tropicana Hotel & Casino features every kind of game, including 98 percent payback on selected dollar slots. Tropicana is the home of the world famous Folies Bergere, and the Xtreme Magic. The hotel features excellent dining in all seven restaurants. Surround yourself with lush, tropical foliage, cascading waterfalls, towering palms, and sparkling pools.",
//        "url": "http://www.orbitz.com/App/ViewSpecificHotelLP?masterId=6123",
//        "stars": "3",
//        "address1": "3801 Las Vegas Blvd South",
//        "address2": "",
//        "city": "Las Vegas",
//        "state": "NV",
//        "postalcode": ""

//    },
//        {
//        "latitude": "36.1275",
//        "longitude": "-115.1667",
//        "name": "Wynn Las Vegas",
//        "description": "Escape to the pinnacle of luxury this summer at Wynn Las Vegas. Enjoy exclusive access to artistically sculptured pools set among an oasis of elegant gardens. Sip a refreshing libation or play poolside blackjack at the Cabana Bar. Liberate your spirit at the European style topless pool. If this is your idea of summer luxury, youâ€™ll find it poolside at Wynn Las Vegas.",
//        "url": "http://www.orbitz.com/App/ViewSpecificHotelLP?masterId=222369",
//        "stars": "5",
//        "address1": "3131 Las Vegas Blvd South",
//        "address2": "",
//        "city": "Las Vegas",
//        "state": "NV",
//        "postalcode": ""

//    },
	
//    // attractions/activities
	
//    {
//        "latitude": "36.1209",
//        "longitude": "-115.1704",
//        "name": "Blue Man Group at the Venetian",
//        "description": "Blue Man Group is best known for its award-winning theatrical productions, which critics have described as ground-breaking, visually stunning and musically powerful.",
//        "url": "http://www.orbitz.com/Secure/PrepareOASProductDetails?productId=3152&prodIdx=-1&dateless=true&market=LAS",
//        "iconimage": "/img/icons/dp_oas.gif",
//        "iconheight": "16",
//        "iconwidth": "25"
//    },
//    {
//        "latitude": "36.1014",
//        "longitude": "-115.1748",
//        "name": "ZUMANITY - The Sensual Side of Cirque Du Soleil",
//        "description": "ZUMANITY, The Sensual Side of Cirque Du Soleil, is a provocative cabaret-style production with a Cirque du Soleil twist. On the cutting edge of contemporary Las Vegas entertainment, ZUMANITY blends playful innuendo with daring eroticism in the intimate ZUMANITY Theatre. Only at New York-New York Hotel & Casino in Las Vegas.",
//        "url": "http://www.orbitz.com/Secure/PrepareOASProductDetails?productId=1929&prodIdx=-1&dateless=true&market=LAS",
//        "iconimage": "/img/icons/dp_oas.gif",
//        "iconheight": "16",
//        "iconwidth": "25"
//    },
//    {
//        "latitude": "36.1241",
//        "longitude": "-115.1714",
//        "name": "Mystere by Cirque du Soleil",
//        "description": "The show that breaks all the rules! Mystere by Cirque du Soleil - a surrealistic celebration of music, dance, acrobatics and comedy. The vivid sets of Mystere are punctuated by colorful costumes and signature Cirque du Soleil acts such as Chinese Poles, Hand-to-Hand balancing, Aerial High Bar and Bungee.",
//        "url": "http://www.orbitz.com/Secure/PrepareOASProductDetails?productId=1930&prodIdx=-1&dateless=true&market=LAS",
//        "iconimage": "/img/icons/dp_oas.gif",
//        "iconheight": "16",
//        "iconwidth": "25"
//    },
//    {
//        "latitude": "36.1013",
//        "longitude": "-115.1689",
//        "name": "Ka by Cirque du Soleil",
//        "description": "KA, the unprecedented new theatrical show from Cirque du Soleil at The MGM Grand, combines acrobatic performances, martial arts, puppetry, multimedia and pyrotechnics to tell the epic saga of Imperial Twins. The twins, a boy and a girl, embark on an adventurous journey to fulfill their destinies. Features over 80 performers from around the world.",
//        "url": "http://www.orbitz.com/Secure/PrepareOASProductDetails?productId=2926&prodIdx=-1&dateless=true&market=LAS",
//        "iconimage": "/img/icons/dp_oas.gif",
//        "iconheight": "16",
//        "iconwidth": "25"
//    },
//    {
//        "latitude": "36.1153",
//        "longitude": "-115.1871",
//        "name": "Tony n' Tina's Wedding - Las Vegas",
//        "description": "Get yourself invited to the World's number #1 Dinner Comedy Show. It's the wildest, wackiest wedding ever! Audience members are treated like family at this groundbreaking interactive comedy you'll be laughing about for years to come. The fun starts with the happy couple's hilarious wedding ceremony then the all-inclusive reception dinner, wedding cake, live band, and lots of laughs!",
//        "url": "http://www.orbitz.com/Secure/PrepareOASProductDetails?productId=3159&prodIdx=-1&dateless=true&market=LAS",
//        "iconimage": "/img/icons/dp_oas.gif",
//        "iconheight": "16",
//        "iconwidth": "25"
//    },
//    {
//        "latitude": "36.1221",
//        "longitude": "-115.1728",
//        "name": "The Beatles LOVE by Cirque du Soleil",
//        "description": "LOVE brings the magic of Cirque du Soleil together with the spirit and passion behind the most beloved rock group of all time The Beatles to create a vivid, intimate and powerful entertainme`nt experience.",
//        "url": "http://www.orbitz.com/Secure/PrepareOASProductDetails?productId=5541&prodIdx=-1&dateless=true&market=LAS",
//        "iconimage": "/img/icons/dp_oas.gif",
//        "iconheight": "16",
//        "iconwidth": "25"
//    },
//    {
//        "latitude": "36.1209",
//        "longitude": "-115.1704",
//        "name": "Madame Tussaud's Interactive Wax Museum",
//        "description": "The World Famous Madame Tussauds Las Vegas is a Must See, Must Feel interactive celebrity experience. Featuring lifelike wax creations of your favorite celebrities, you'll feel like youâ€™re on the A-list.",
//        "url": "http://www.orbitz.com/Secure/PrepareOASProductDetails?productId=2593&prodIdx=-1&dateless=true&market=LAS",
//        "iconimage": "/img/icons/dp_oas.gif",
//        "iconheight": "16",
//        "iconwidth": "25"
//    }	

//]
//    }
//}
