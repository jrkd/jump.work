﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class TestData
    {
        public static int MIN_AGE = 8;
        public static int MAX_AGE = 100;

        public static int oldestCompetition = 1950;
        public static Random NumGen = new Random();

        public static IDictionary<Skill, List<Competition>> GenerateCompetitions(IEnumerable<Skill> skills)
        {
            Dictionary<Skill, List<Competition>> compsBySkill = new Dictionary<Skill, List<Competition>>();
            foreach(var skill in skills)
            {
                List<Competition> comps = new List<Competition>();
                compsBySkill.Add(skill, comps);
                for (int year = oldestCompetition; year <= DateTime.Today.Year; ++year)
                {
                    if (year % 4 == 0) //every four years
                    {
                        bool isChampionship = NumGen.NextDouble() >= 0.5;
                        bool isWorld = NumGen.NextDouble() >= 0.5;
                        bool isTour = NumGen.NextDouble() >= 0.5;
                        string[] sponsors = new string[] { "", "Qantas", "Dell", "TFL" };
                        string name = string.Format("The {0} {1} {2} {3} {4}",sponsors[NumGen.Next(0, sponsors.Length)], (isWorld ? "World" : ""), skill.Name, (isChampionship ? "Championship" : ""), (isTour ? "Tour" : ""));
                        comps.Add(new Competition()
                        {
                            Name = name,
                            Year = year
                        });
                    }
                }
            }
            return compsBySkill;
        }

        public static IEnumerable<Person> GeneratePeople(int numToGenerate, IDictionary<Skill, List<Competition>> competitionsBySkill)
        {
            var textFormat = System.Globalization.CultureInfo.CurrentCulture.TextInfo;
            
            int numNames = LASTNAMES.Count();
            List<Person> generated = new List<Person>();
            for(int index = 0; index < numToGenerate; ++index)
            {
                int numComps = NumGen.Next(0, 4);
                IEnumerable<KeyValuePair<Skill, List<Competition>>> personSkillKeys = competitionsBySkill.OrderBy(a => Guid.NewGuid()).Take(5).ToList();
                //shuffle the comps list for person's skills
                //take random num
                IEnumerable<Competition> personComps = personSkillKeys.SelectMany(pair => pair.Value.OrderBy(a => Guid.NewGuid())) 
                                                                      .Take(numComps);
                generated.Add(new Person()
                {
                    age = NumGen.Next(MIN_AGE, MAX_AGE),
                    Name = 
                        textFormat.ToTitleCase(LASTNAMES[NumGen.Next(0, numNames)].ToLower()) //First name
                        + " "
                        + textFormat.ToTitleCase(LASTNAMES[NumGen.Next(0, numNames)].ToLower()),// Last name
                    Skills = personSkillKeys.Select(pair=>pair.Key).ToList(),
                    Competitions = personComps.ToList()
                });
            }
            return generated;
        }
        
        public static Skill[] SKILL_TREE = new Skill[]{
            new Skill() {
                Name ="Competitive model sports"
            },
            new Skill() {
                Name ="Mind sports",
                SubSkills = new List<Skill>(){
                    new Skill(){Name = "Card games"},
                    new Skill(){Name = "Other"},
                    new Skill(){Name = "Speedcubing"},
                    new Skill(){Name = "Strategy board games"}
                }
            },
            new Skill() {
                Name ="Physical sports",
                SubSkills = new List<Skill>(){
                    new Skill(){ Name="Air sports" },
                    new Skill(){ Name="Archery" },
                    new Skill(){ Name="Ball-over-net games" },
                    new Skill(){ Name="Basketball family" },
                    new Skill(){ Name="Bat-and-ball (safe haven)" },
                    new Skill(){ Name="Baton twirling" },
                    new Skill(){ Name="Acro sports" },
                    new Skill(){ Name="Performance sports" },
                    new Skill(){ Name="Board sports" },
                    new Skill(){ Name="Catch games" },
                    new Skill(){ Name="Climbing" },
                    new Skill(){ Name="Cycling", SubSkills = new List<Skill>(){
                        new Skill(){Name = "Bicycle"},
                        new Skill(){Name = "Skibob"},
                        new Skill(){Name = "Unicycle"}
                    } },
                    new Skill(){ Name="Martial arts", SubSkills = new List<Skill>(){
                        new Skill(){Name="Grappling"},
                        new Skill(){Name="Martial arts (Striking)"},
                        new Skill(){Name="Martial arts (Mixed or hybrid)"},
                        new Skill(){Name="Martial arts (Weapons)", SubSkills = new List<Skill>(){
                            new Skill(){Name="Martial arts (Weapons - Skirmish)"}
                        } }
                    } },
                    new Skill(){ Name="Cue sports" },
                    new Skill(){ Name="Equine sports" },
                    new Skill(){ Name="Fishing" },
                    new Skill(){ Name="Flying disc sports" },
                    new Skill(){ Name="Football" },
                    new Skill(){ Name="Golf" },
                    new Skill(){ Name="Gymnastics" },
                    new Skill(){ Name="Handball family" },
                    new Skill(){ Name="Hunting" },
                    new Skill(){ Name="Ice sports" },
                    new Skill(){ Name="Kite sports" },
                    new Skill(){ Name="Mixed discipline" },
                    new Skill(){ Name="Orienteering family" },
                    new Skill(){ Name="Pilota family" },
                    new Skill(){ Name="Racquet (or racket) sports" },
                    new Skill(){ Name="Remote control" },
                    new Skill(){ Name="Rodeo-originated" },
                    new Skill(){ Name="Running" },
                    new Skill(){ Name="Sailing" },
                    new Skill(){ Name="Snow sports", SubSkills=new List<Skill>(){
                        new Skill(){Name="Skiing"},
                        new Skill(){Name="Snowboarding"},
                        new Skill(){Name="Sled sports"}
                    } },
                    new Skill(){ Name="Shooting sports" },
                    new Skill(){ Name="Stacking" },
                    new Skill(){ Name="Stick and ball games", SubSkills = new List<Skill>(){
                        new Skill(){ Name="Hockey" },
                        new Skill(){ Name="Hurling and shinty" },
                        new Skill(){ Name="Lacrosse" },
                        new Skill(){ Name="Polo" }
                    } },
                    new Skill(){ Name="Street sports" },
                    new Skill(){ Name="Tag games" },
                    new Skill(){ Name="Walking" },
                    new Skill(){ Name="Wall-and-ball" },
                    new Skill(){ Name="Aquatic & paddle sports" , SubSkills= new List<Skill>(){
                        new Skill(){ Name="Canoeing" },
                        new Skill(){ Name="Kayaking" },
                        new Skill(){ Name="Rafting" },
                        new Skill(){ Name="Rowing" },
                        new Skill(){ Name="Other paddling sports" },
                        new Skill(){ Name="Aquatic ball sports", SubSkills = new List<Skill>(){
                            new Skill(){ Name= "Aquatic ball sports (Surface)" },
                            new Skill(){ Name= "Aquatic ball sports (Underwater)" }
                        } },
                        new Skill(){ Name="Competitive swimming", SubSkills = new List<Skill>(){
                            new Skill(){Name="Competitive swimming (Kindred activities)"}
                        } },
                        new Skill(){ Name="Subsurface and recreational swimming" },
                        new Skill(){ Name="Diving" }
                    } },
                    new Skill(){ Name="Weightlifting" },
                    new Skill(){ Name="Motorized sports", SubSkills= new List<Skill>()
                    {
                        new Skill(){ Name = "Auto racing" },
                        new Skill(){ Name = "Motorboat racing" },
                        new Skill(){ Name = "Motorcycle racing" },
                        new Skill(){ Name = "ATV racing" }
                    } },
                    new Skill(){ Name="Marker sports" },
                    new Skill(){ Name="Musical sports" },
                    new Skill(){ Name="Fantasy sports" },
                    new Skill(){ Name="Physical sports (Other)" },
                    new Skill(){ Name="Overlapping sports" }
                }
            },

        };
        public static string[] LASTNAMES = new string[]
        {
            "WOOLUMS",
"ESPINOLA",
"HAWMAN",
"HURSEY",
"FAULKES",
"SILCOX",
"GRAMACY",
"ANDEREGG",
"PRIODE",
"LOUIL",
"WONG",
"BROWNLOW",
"ROSENAU",
"DAMICO",
"HAUGAARD",
"POITER",
"BHATIA",
"WHIDDEN",
"BRICKEY",
"MADRIGAL",
"PARRENO",
"BUSSER",
"BAGNER",
"BUCIO",
"GRONVALL",
"LAPIER",
"SHELKOFF",
"LEATHAM",
"PINTADO",
"WEBB",
"YONO",
"CONTRATTO",
"BOEDING",
"PRUITT",
"NERLICH",
"PARHAN",
"VANDEWEERT",
"KADY",
"HASKIN",
"THURN",
"FRERE",
"PURPLE",
"COTMAN",
"TOWN",
"SHURTLIFF",
"PINYAN",
"CARUANA",
"DONAVAN",
"PATAGUE",
"WMITH",
"SHANKLE",
"BELLENDIR",
"LEONARDO",
"HILLIE",
"HOUSEN",
"REYNALDS",
"SPASIANO",
"TRUSSLER",
"MACON",
"ULCH",
"VANDEPUTTE",
"DYNES",
"MATTHEY",
"WATTENBARGER",
"MONTFORT",
"CASSAR",
"HOKUTAN",
"STEINHOFF",
"HOVANEC",
"DRONEN",
"TOUART",
"LABAUVE",
"LASKE",
"BRACE",
"HANDT",
"MIN",
"HIGINBOTHAM",
"TERRENCE",
"BODFISH",
"HORD",
"CONOLE",
"TENNER",
"SCHUT",
"HAFLETT",
"BOATRIGHT",
"COTTER",
"DRAYER",
"MANFREDI",
"VANYO",
"SOLEM",
"LYSHER",
"MARXEN",
"THIEDE",
"COWLES",
"APPLEGARTH",
"BRATTIN",
"BECKES",
"TIEMEYER",
"BURKETTE",
"HILLIS",
"GUIDERA",
"MOUNSEY",
"REMME",
"FISHER",
"JOLLY",
"IRR",
"WISSINGER",
"BIROS",
"ADERHOLT",
"HOEL",
"FILS",
"GROOMBRIDGE",
"VIDA",
"CZAR",
"FANDINO",
"IBACH",
"RIVENBURG",
"PALLOTTO",
"LEGETTE",
"HARAWAY",
"RENFER",
"CRANSHAW",
"KORALEWSKI",
"GUADIAN",
"BLAU",
"BOUILLON",
"ZACZEK",
"NEHRING",
"FLAUTT",
"LYLES",
"MARCELL",
"CUBITO",
"HELMICH",
"SERMON",
"MUNDO",
"MAGEL",
"SORBER",
"STOBAUGH",
"FIZER",
"DLABAJ",
"LEANOS",
"STRASSNER",
"BESCO",
"LAUDADIO",
"ROESE",
"ROCCHI",
"GRIEB",
"DILEY",
"LEFCHIK",
"GLADWIN",
"NIESEN",
"KITAGAWA",
"HACKMAN",
"MCCLURKIN",
"GROESCHEL",
"AZEEM",
"SERVANT",
"FUS",
"SWICK",
"EITNIEAR",
"SEMSEM",
"TARNOFF",
"BRAMLET",
"WHITESCARVER",
"MCCRAKEN",
"CHAIKEN",
"HUSKY",
"CASTELLANI",
"GRUNDMAN",
"NOURI",
"THETFORD",
"JOSEY",
"YBARRO",
"WODICKA",
"ZICKLER",
"JOLICOEUR",
"LAUSELL",
"FISCHBEIN",
"GERHOLD",
"TOLDNESS",
"GAIR",
"STIRN",
"CUTLER",
"COOMBS",
"KIMBRIL",
"FARNESE",
"KICHLINE",
"SHIMSKY",
"PETTAWAY",
"KOLICH",
"BALDASSARE",
"PIEDRA",
"PALEN",
"SKEELE",
"EMIGH",
"STOECKEL",
"STROOP",
"LACUESTA",
"SCHRIEBER",
"INGLIN",
"NETH",
"SHUCKHART",
"PRIZIO",
"ZACHARY",
"BALKO",
"COMELLA",
"DELPRIORE",
"JEANES",
"KOMAR",
"PRUSINSKI",
"KLEMKE",
"STIEF",
"DEPPNER",
"BINET",
"BILLER",
"DYER",
"KEATOR",
"MALANADO",
"FLEISHER",
"BALTZELL",
"YAMAOKA",
"MEDOVICH",
"EWTON",
"CORDLE",
"ZAISS",
"METGE",
"TREGAN",
"CHOJNACKI",
"LAUBACHER",
"BURCZYK",
"COOKMAN",
"JUERGENS",
"MCGROTHA",
"MCNEAL",
"SANTONI",
"LOCKLEY",
"ALPERIN",
"HOAR",
"STUCKMEYER",
"MCGLORY",
"WITTMANN",
"MENTO",
"WACASTER",
"HILYER",
"YELDON",
"WESTWATER",
"KURTTI",
"WISECARVER",
"WAEGNER",
"BEDNARZ",
"HANNAFORD",
"POCOCK",
"DEMELO",
"KOENNING",
"ZANDERS",
"COKEL",
"BETTLE",
"BERNIERI",
"HICKOX",
"MILORD",
"BOHONIK",
"BRODKA",
"GOLETZ",
"URIBE",
"SIGLAR",
"WISSLER",
"UBOLDI",
"KLAPPER",
"WIENKE",
"WHISTON",
"MEHARG",
"BUJARSKI",
"KANELOS",
"SMYLIE",
"DROUBAY",
"FRAYRE",
"ODONELL",
"SWEETLAND",
"DROSS",
"HOUTS",
"KAY",
"ROBERSON",
"MANGANO",
"MCELRATH",
"QUELETTE",
"GUZZETTA",
"BOWDOIN",
"MIYAZAKI",
"ZELEZNIK",
"MATTOCK",
"ALTONEN",
"GUTTMAN",
"HUBERTZ",
"SHIMASAKI",
"HASAK",
"BUCKELEW",
"KAIL",
"WICHROWSKI",
"HITZLER",
"TRUMBLEY",
"CITRO",
"MAYMI",
"HAYNE",
"MERCURIO",
"HYDER",
"BREITENBUCHER",
"KOSOROG",
"KITHCART",
"PROIA",
"OCHWAT",
"MILLIKEN",
"ZBIKOWSKI",
"ALEN",
"PASQUIN",
"FASCI",
"EPPERLEY",
"MARC",
"CALVERT",
"TOXEY",
"CASON",
"STRETZ",
"SEGRAVE",
"SHATT",
"OESTERREICH",
"GOERLICH",
"SIMMIONS",
"GRADLE",
"LAUR",
"DECASANOVA",
"GLAZA",
"VANDEWERKER",
"HORR",
"KRASOVEC",
"TURREY",
"BOLVIN",
"TOWNE",
"REISMAN",
"ANKENMAN",
"PLAGMANN",
"HATT",
"DEESE",
"BEVLY",
"SALVERSON",
"PASSMORE",
"WAKINS",
"MAINGUY",
"BIELSER",
"BAWEK",
"TOPER",
"WINIK",
"REPP",
"EMHOFF",
"ARELLEANO",
"BUYCK",
"NIEBYL",
"RAFEL",
"VERWERS",
"MARN",
"SAUCHEZ",
"GRAISE",
"ANTILL",
"DESCHENE",
"PLOUGH",
"SHIBATA",
"CATINO",
"GULICK",
"HOPPINS",
"RICHEL",
"GLUECKERT",
"WELSON",
"QUALEY",
"GOGAN",
"ILIC",
"BEHLKE",
"MENDEL",
"PEAKER",
"GRABE",
"BOCCIA",
"BIANCANIELLO",
"BEVERS",
"RINGLER",
"HILTUNEN",
"KRYLO",
"ETTISON",
"LATHE",
"WHITMOYER",
"BUBOLTZ",
"CRAGUN",
"MASHORE",
"LORENT",
"DARGENIO",
"BRUSCO",
"LAGASSIE",
"GOAD",
"CUTFORTH",
"NIEDECKEN",
"FORNIER",
"SPECCHIO",
"PELEG",
"FALLIE",
"ADSIDE",
"MOUSSETTE",
"UNVARSKY",
"BOARDMAN",
"MUNDAHL",
"LEININGER",
"LEIENDECKER",
"FICKEL",
"ANDRADE",
"AMRINE",
"RITZ",
"KASSEM",
"ETTL",
"RENFROE",
"ELFENBEIN",
"BATTEN",
"MCCLUNEY",
"MEALOR",
"ZUCHOWSKI",
"GAMBARDELLA",
"LUKEN",
"BEACHAM",
"KHATIB",
"PARODY",
"BOLLBACH",
"DUNSTON",
"KORF",
"NAUMOFF",
"AVANTS",
"GAGER",
"WOHLGEMUTH",
"TEEGARDEN",
"ZOGG",
"SAMORA",
"SUM",
"NIVEN",
"ADI",
"CROCKER",
"MCKISSACK",
"FAIRWEATHER",
"LUNDMAN",
"GRAFFAM",
"MORELLO",
"DAIS",
"LANDO",
"OLCUS",
"SHAUD",
"FEDERICK",
"MEYN",
"PELE",
"INZANA",
"WOLINSKI",
"LOXTERCAMP",
"KINKAID",
"ACEVEDO",
"JALOMO",
"FRUTOS",
"MAKINS",
"SHAHEED",
"DAX",
"KARATZ",
"MUTCHLER",
"BAYGENTS",
"SETTY",
"CALVER",
"THEILING",
"JANN",
"STOTESBERRY",
"MCLAIN",
"HORRIDGE",
"SAENPHIMMACHA",
"LANGENFELD",
"WAUNEKA",
"JAVERY",
"MEANY",
"CUTTY",
"TEHAN",
"JENAYE",
"KOPEL",
"VETH",
"SCHIRALLI",
"BERRONG",
"PEPLAU",
"HAZELHURST",
"NEEDELS",
"FERGUSEN",
"DEGENNARO",
"GOLEBIOWSKI",
"ULLO",
"SCAFUTO",
"WEIDOWER",
"MCLIN",
"LUCIANO",
"SCHOENBERGER",
"LITAKER",
"SABINO",
"CLOSSON",
"WALEGA",
"PATCHIN",
"CONTINENZA",
"KONECNY",
"PERROTA",
"BUSCAGLIA",
"MANDOZA",
"SIGNORELLI",
"BIBLER",
"RICKENBACKER",
"DOSHIER",
"BENEKE",
"TYNON",
"SCHOWALTER",
"MARKEY",
"VANORSDALE",
"CHEEVES",
"SANTILLANA",
"GIAMBALVO",
"SUDLER",
"STEENHUIS",
"RIGGINGS",
"ISACKSON",
"ROMANELLI",
"SPRANDEL",
"MELLETT",
"BERNARDY",
"BRZOSTOWSKI",
"SUKUP",
"LOCHRIDGE",
"ENGELSON",
"COPLON",
"CHILA",
"HILLES",
"MANSKER",
"MCGRAPTH",
"HOGLAND",
"RADICS",
"HIRAOKA",
"GOTSCHE",
"FORNEY",
"HOUSKA",
"SHULT",
"LAUGHLIN",
"SBORO",
"RENA",
"HIGDON",
"MULVERHILL",
"ACKLES",
"NAUGHER",
"HOCHSTETLER",
"WOLFING",
"FOUND",
"OLHEISER",
"BOLDT",
"GODEAUX",
"SKELLY",
"KUHNS",
"STAVRIDES",
"BURKA",
"SHERBONDY",
"WRYALS",
"LAREY",
"LUBINSKY",
"BLONG",
"RUTANA",
"WANNER",
"YOKUM",
"BARTHOLD",
"VILLANOVA",
"PENSINGER",
"FOUNDS",
"PIERROT",
"ODDEN",
"PELIS",
"COLEBROOK",
"HISEL",
"COLPA",
"GONNELLA",
"HALUSKA",
"KAPPERMAN",
"PECOT",
"URBINA",
"CARLINI",
"KANNADY",
"TSUBOI",
"NICOLAU",
"CRON",
"MORI",
"LOMBARDO",
"POLYNICE",
"SEVERSON",
"LINDAMOOD",
"OFFORD",
"GOCHA",
"COGLIANO",
"HANSMAN",
"CZWAKIEL",
"GALLENTINE",
"WINDSOR",
"FOUCHA",
"UBER",
"RION",
"LINEBERRY",
"KARAPETIAN",
"GUIGGEY",
"MCINTURE",
"FREETAGE",
"MATAKA",
"NIELSEN",
"HOLTZCLAW",
"FERRUCCI",
"DOUET",
"FONTANA",
"FOO",
"KENNY",
"GRIFFTH",
"CAGNO",
"SUCHY",
"SUYDAN",
"LEUTY",
"MUSSELWHITE",
"PAITH",
"DULONG",
"HANEKE",
"SHREWSBERRY",
"STREKAS",
"CUSSEN",
"MCCANNA",
"DUFFIELD",
"MANTELLI",
"MCNAIL",
"LEMANSKY",
"GRUMER",
"SUMROW",
"KRATOFIL",
"CORBETT",
"MAURUS",
"LEGASSIE",
"EHORN",
"TINDLE",
"BATCHELDER",
"LAMPER",
"LIUZZI",
"KIVISTO",
"VIS",
"FISHBAUGH",
"LAMPKINS",
"CRUSINBERRY",
"CLYMER",
"REBOLD",
"SHAMBAUGH",
"LOCKE",
"ROCKHOLD",
"ORDMAN",
"MCCLATCHER",
"CONNON",
"LOSCHIAVO",
"FAUSKE",
"RIEHM",
"LICKTEIG",
"AMBROSIUS",
"FRESH",
"STYLES",
"LABRE",
"MENTIS",
"BATTERSON",
"KUTZNER",
"SELLMAN",
"SCHUPBACH",
"RICKELS",
"DINGELL",
"CASAUS",
"PHILLEY",
"PROVO",
"MEDEZ",
"ZANNI",
"LAMONTE",
"AFAN",
"BOYE",
"ARVIE",
"VAETH",
"RETTIG",
"PULIDO",
"SALDI",
"PUNTILLO",
"FEISTNER",
"FILIPPONI",
"SMETANA",
"BOEHME",
"MAKEL",
"CULPEPPER",
"STRITTMATER",
"INGALA",
"BILLIOTT",
"GABISI",
"REHNBERG",
"THANG",
"HAINDS",
"GRZYB",
"BEADELL",
"KYLISH",
"VALORIE",
"WADDUPS",
"ONYEAGU",
"KRONEMEYER",
"SALVIO",
"SMITHE",
"LIENHARD",
"ARKING",
"ARB",
"MCFEETERS",
"ARN",
"SONDERGAARD",
"ZIEMSKI",
"BERMEJO",
"MAZARIEGOS",
"STROTZ",
"CROSLEN",
"CUCHARES",
"WIEMER",
"FILA",
"SKALICKY",
"ANASTASI",
"GRAY",
"RUKAVINA",
"GOLONKA",
"CRAGO",
"DUBUISSON",
"DIVEN",
"OPIELSKI",
"MEIS",
"PANTON",
"THILKING",
"CAPONERA",
"WASMER",
"EKIS",
"WITTER",
"MAFFITT",
"HOLBEN",
"BATIS",
"CORUM",
"BUDAK",
"MUELLERLEILE",
"AZBILL",
"SCAVOTTO",
"ORNELAZ",
"STTHOMAS",
"RAHAL",
"BUTCH",
"CRAMPTON",
"LEZAMA",
"PFALMER",
"DEMOYA",
"HERSOM",
"VALENTINI",
"BRAXTON",
"STRENGE",
"SEISLER",
"ZAGARA",
"VERUCCHI",
"THORSTENSON",
"BOLOGNIA",
"CASSANO",
"CLINK",
"KUNS",
"SPEZIALE",
"NIMOCKS",
"STARBIRD",
"LACERENZA",
"ANGERMEIER",
"KNOCKE",
"ARNESON",
"SHARBONO",
"VANGIESON",
"TRUEAX",
"MCTIER",
"HAAKER",
"HARR",
"MACOLA",
"LANEAUX",
"HAZELTINE",
"DELANCY",
"ALAN",
"COOLER",
"LANGENESS",
"HEIMS",
"SCHNEIDERMANN",
"KERZER",
"GALINIS",
"CALAMITY",
"ANOE",
"BOYDSTUN",
"ALDECOA",
"LENDRUM",
"RUBISON",
"ELMQUIST",
"SHIP",
"PANCHANA",
"RAKER",
"JUI",
"FINNLEY",
"MADAYAG",
"FURGISON",
"DICERBO",
"URIOSTEGUI",
"BURNIAS",
"PUNTER",
"QUALLS",
"RICO",
"BJORNSON",
"AVOLA",
"KOZLOFF",
"LISIUS",
"HULLUM",
"BRETTHAUER",
"MORESCHI",
"TROMBLAY",
"VARAJAS",
"SALVADGE",
"CAMPORA",
"REUER",
"BALLOG",
"TUCKEY",
"MYRUM",
"PARIKH",
"SANPAOLO",
"FLICKINGER",
"JEST",
"BRISCO",
"DUTY",
"HERRAND",
"CUMBERLEDGE",
"BAKOS",
"FORSTON",
"CASHDOLLAR",
"BERNER",
"LITTFIN",
"DENICE",
"WINESICKLE",
"VILLAQUIRAN",
"CORRIDAN",
"DENOYELLES",
"GRONITZ",
"ROSENGARTEN",
"STROEDE",
"BREAZEALE",
"LUTTRULL",
"PEALS",
"EGUIZABAL",
"BOUDRIE",
"KRALIK",
"WEINBACH",
"OXNER",
"ABBATIELLO",
"EDLAO",
"SPRABERRY",
"GRANDOLFO",
"WATSON",
"PADEKEN",
"MCLELAND",
"RIGAZIO",
"YARRELL",
"BOUCHE",
"CLEMMONS",
"TRODDEN",
"VOELKEL",
"DEGRAZIA",
"HENNECKE",
"LIMESAND",
"GRANTO",
"PRIVITERA",
"LINDERMAN",
"METTA",
"FRITZ",
"COLATO",
"GOODSELL",
"BAVARD",
"HOUSHOLDER",
"ZAFFALON",
"MILUSH",
"JAUDON",
"LECHUGA",
"ALPHONSO",
"WHIPKEY",
"MAINELLA",
"DIDWAY",
"SKIDMORE",
"CARBONNEAU",
"PORCHE",
"ARMENDAREZ",
"AURICH",
"GIANNELL",
"FIEST",
"PITTSENBARGER",
"PETROCCO",
"KORY",
"SINDORF",
"KJELLMAN",
"FREASE",
"PIMENTEL",
"SIFFERT",
"BRUMER",
"CABOT",
"TRAUTMAN",
"WEARE",
"GRAMC",
"MAYNERICH",
"BUGH",
"OHOTTO",
"NEHRT",
"LEISS",
"REHBERG",
"CLAUSI",
"DELPIANO",
"SCHINDELDECKE",
"WISNOSKI",
"TRUCHON",
"BROWN",
"KAMENSKY",
"PLUSH",
"COPLEN",
"VIZCARRA",
"ANICK",
"PITZER",
"CIMINO",
"GARDINIER",
"HARWOOD",
"CARSTARPHEN",
"PICH",
"AHRNS",
"FRICKEL",
"YEARTA",
"CALLAS",
"BURGH",
"AGNOR",
"MOISA",
"CORLEY",
"HAGARTY",
"RADA",
"PENBERTHY",
"PEETOOM",
"NUHFER",
"BARRIENTEZ",
"WOODBRIDGE",
"MASKALY",
"SIMCO",
"KEMPLER",
"FRERICKS",
"DRAGOO",
"BRELJE",
"CARUTHERS",
"EGELSTON",
"STANOWSKI",
"GOLDTRAP",
"KRZYZANOWSKI",
"CRASS",
"TARTAR",
"OLEKSY",
"BERTHOLD",
"MINCE",
"HEADING",
"COTTONE",
"QUA",
"VACCARO",
"PINTO",
"JENKINS",
"CHICALACE",
"LAZIER",
"SANTORELLA",
"GIROLAMO",
"MAZOR",
"RODARTA",
"VALASQUEZ",
"MCCLODDEN",
"SZUBA",
"HERMENAU",
"TALK",
"GRISANTI",
"CATTABRIGA",
"AMMIRATO",
"PINEIRO",
"ISING",
"OGIBA",
"CHUI",
"OSBY",
"PICON",
"MIRA",
"JACKLIN",
"BRULE",
"LINDENPITZ",
"NIERMANN",
"WORN",
"OBORNE",
"WOLTHUIS",
"SEIM",
"KOOISTRA",
"HOWTON",
"NOEL",
"RYAN",
"SPLONSKOWSKI",
"KARELL",
"LINGAO",
"SNEEDEN",
"HIENS",
"VALLARIO",
"TOMME",
"EVETT",
"ZURITA",
"MACHAN",
"TORGERSON",
"THAL",
"OKKEN",
"KINSKY",
"SIMONETT",
"YEAREGO",
"HEW",
"GUERRIERO",
"ROULHAC",
"SANTANDER",
"BOLIEK",
"TROUTT",
"STIEN",
"LOGIE",
"THERRIAULT",
"GUSTAFSON",
"WYKE",
"VITELLO",
"DESPOSITO",
"LONZO",
"RANAH",
"ALMARAZ",
"HONES",
"ECKLES",
"PASSER",
"GALLEY",
"SCHMAUDER",
"CAWTHORN",
"AJAYI",
"RAPPA",
"GONZOLEZ",
"MAVRO",
"FRIEST",
"GORTER",
"HAYMANS",
"MONTS",
"RIETSCHLIN",
"WILLHOITE",
"MARETTI",
"MATTHIAS",
"WILENKIN",
"GRIBBLE",
"RONAN",
"AUSTAD",
"MCCLEVE",
"FILEDS",
"BUGLER",
"DUL",
"PAULL",
"AHLMAN",
"RAMISCAL",
"GILMARTIN",
"BRYAR",
"COLCHADO",
"SINEGAL",
"MATULA",
"BROACH",
"SPRINKLES",
"OTANI",
"DUNEMN",
"SIXON",
"KERNE",
"GAFFIGAN",
"LENERS",
"MAKUA",
"HABBEN",
"DEIDA",
"SCHUDEL",
"BATCH",
"MALLINSON",
"KLEMENT",
"WEIGELT",
"MCCASTLE",
"TERI",
"BLAKELEY",
"LUVENE",
"LIPPY",
"HAUTALA",
"GOCKE",
"BEVIER",
"AYALLA",
"BARRERAS",
"SIFUENTES",
"SOPHA",
"BRUST",
"KIRKLING",
"HILLIKER",
"GRANDUSKY",
"BAYES",
"BURGOYNE",
"PERROTTA",
"ARIAS",
"BIALAS",
"TOGASHI",
"HETTICH",
"CLEAR",
"POST",
"ALWARD",
"KONECNI",
"MALOOL",
"SNIVELY",
"CAMPEAU",
"SAUBY",
"BOESER",
"SCHEUERS",
"PUTHOFF",
"NAGASE",
"CHEYNE",
"KENOUO",
"PICADO",
"BRADEY",
"GARGUILO",
"CRAZE",
"ROSILLO",
"CLICKNER",
"MATSUSHIMA",
"HAIRELL",
"PERSONETTE",
"HERVIG",
"MARCHIORI",
"BONETTI",
"JINKERSON",
"PRAG",
"BANVELOS",
"SUPPLICE",
"DONOHO",
"GARZA",
"WEDWICK",
"NICOLOSI",
"GALLES",
"KIMLER",
"ROCKHOLT",
"STREETMAN",
"DOMENECH",
"NETTLES",
"ROSA",
"FALT",
"CAROLUS",
"KIEBLER",
"SOFFA",
"FURNEY",
"TRAFICANTE",
"KOCHIS",
"DUEMMEL",
"MUTHERSPAW",
"MASONER",
"BOSELL",
"DRAYTON",
"ABUGN",
"HAINLEY",
"TUGGIE",
"BACHHUBER",
"BEDELL",
"WINRICH",
"BUSCHE",
"LAMOTT",
"LEAVERTON",
"HARBORTH",
"CAWEIN",
"VANHOUTEN",
"FAUSTINO",
"BAAR",
"CONEDY",
"REVEL",
"JERNEJCIC",
"FONUA",
"SICKMEIR",
"GRAESER",
"WAYNS",
"BUCKHOLTZ",
"AMADIO",
"SAEMENES",
"SWEIGART",
"TEMPLETON",
"HRICKO",
"HULETTE",
"GOLDENMAN",
"KIRKENDOLL",
"NALDER",
"BIVONA",
"VOLLUCCI",
"VITTITOE",
"ELEFRITZ",
"BRODHAG",
"HEASLIP",
"ABILA",
"DEMMONS",
"RYSER",
"ZELASCO",
"FAES",
"VANDAGRIFF",
"COMSTOCK",
"WOODELL",
"SANDELIN",
"GODDE",
"STOCKHAUSEN",
"LAINE",
"PALLANES",
"WILDRICK",
"CASTELUM",
"ALLSTOTT",
"GOGUEN",
"ANDA",
"COBANE",
"ILARDI",
"OPLAND",
"MCHAFFIE",
"MALGIERI",
"BOVEJA",
"BROZYNA",
"KHELA",
"NISHIYAMA",
"HOGE",
"CHIPHE",
"MAROLA",
"GARTENMAYER",
"CONRY",
"SIEMBIDA",
"PETRO",
"MELEN",
"BOYL",
"RITTINGER",
"HAROLDSON",
"VREEMAN",
"GAINFORTH",
"DUNSING",
"TOWLEY",
"ENGLEBRECHT",
"PAYANT",
"DINKLE",
"HYLTON",
"GUSTIN",
"CARMAIN",
"TRUMM",
"CARACCIOLA",
"SAMWAY",
"LANNOM",
"DEMLER",
"GOUDELOCK",
"LOPER",
"COOTER",
"MCCLEAN",
"MARUGG",
"BANWELL",
"SMYKOWSKI",
"SHOR",
"PICKFORD",
"KADLE",
"CERAO",
"PILKINGTON",
"KLEINKNECHT",
"GRAICHEN",
"DOWKER",
"RUZICKA",
"SCRABECK",
"ZIHAL",
"GAVLES",
"COUNTS",
"RAVENCRAFT",
"HITCH",
"FELSKE",
"WEEDON",
"LAGATTA",
"HIBL",
"KOTECKI",
"WALDALL",
"RUSSIAN",
"LINGBERG",
"BURN",
"AXSON",
"VANOSTRAND",
"BOGAR",
"OYSTER",
"BENDLER",
"ALWAN",
"NIESENT",
"CASELLO",
"PELLICONE",
"DREWER",
"ROCKELMAN",
"ELQUIST",
"TOOKES",
"MARREN",
"LINARDI",
"BUIKE",
"KUTCHAR",
"MINIER",
"IRUEGAS",
"KOEHN",
"VANDINE",
"NOVIELLO",
"GINGG",
"LAWRENCE",
"JARVI",
"CURZ",
"BLESSING",
"TAIRA",
"GALABEAS",
"GEIMER",
"POCAI",
"EMORY",
"ACKERLEY",
"SCHAEDLER",
"HAMMEN",
"RANA",
"GINDI",
"PADDY",
"YOUNIE",
"KIMBERLING",
"CONGDON",
"FRAZEE",
"LASZLO",
"TRUEHART",
"HEBENSTREIT",
"SALSGIVER",
"KEVELIN",
"NET",
"CATT",
"STELTING",
"MAYSONET",
"DAMOUR",
"SELLARDS",
"DALE",
"PAOLUCCI",
"LIBERATI",
"MUSOLF",
"SHIPES",
"BLONDIN",
"RUMFORD",
"BIDEAUX",
"BEDAR",
"BRASSARD",
"MAROSE",
"SVEDINE",
"HANEL",
"STRAYHAND",
"DAMPEER",
"BOLEBRUCH",
"ZILER",
"ALVINE",
"SCOH",
"SCHUCKER",
"HEBRARD",
"VANHEUSEN",
"WEISFELD",
"DEJARDEN",
"KOWITZ",
"SIBAL",
"COHENS",
"SPRANKLE",
"DISOTELL",
"WISINGER",
"DUFORT",
"MIYATA",
"HARGRODER",
"COKLEY",
"SIMERSON",
"KNOWLING",
"FERRIN",
"MORLES",
"WYDNER",
"MANVELITO",
"MARADE",
"GUTIENNEZ",
"DAEHLER",
"BARICH",
"SCAVO",
"GONSALES",
"LEAVENWORTH",
"HEAVIN",
"WEINZETL",
"REY",
"BLUMENBERG",
"DITTRICK",
"MCGEADY",
"WITTHUHN",
"WENGLER",
"DURST",
"COLINA",
"REMMERS",
"MONTIETH",
"MCCASH",
"LADY",
"HETT",
"ROLLIN",
"QUENNEVILLE",
"GARVER",
"ORLICH",
"WALLIN",
"PHANOR",
"PANAK",
"PESA",
"LONGLEY",
"MINAHAN",
"YOUNGBLOOD",
"COULOMBE",
"TRITCH",
"CATTANO",
"MURPHEY",
"HEARN",
"COLQUITT",
"MANNEBACH",
"BERNES",
"LUTWIN",
"TORBIT",
"TUMBLIN",
"ZUROWSKI",
"DALENE",
"HEDON",
"REIF",
"GRUELL",
"KRUMWIEDE",
"LASK",
"DONNALLEY",
"MEREDITH",
"KULCONA",
"SCHAYER",
"BEHRAN",
"WIKOFF",
"LIPSCHUTZ",
"WATKIN",
"GOFTON",
"RYLAND",
"GRIMM",
"RYBOWIAK",
"CASELTON",
"CHEN",
"PERCIBALLI",
"BRABSON",
"LEMOINE",
"MACRUM",
"DUBOURG",
"HOLE",
"OBERLY",
"POURVASE",
"RIEGLER",
"DOBRZYKOWSKI",
"CRAGLE",
"RONDELL",
"CHICOINE",
"FONG",
"TREINE",
"ROBLE",
"ASTILLERO",
"SWEIGARD",
"FUENTES",
"NEAS",
"BUDAY",
"BONASERA",
"FITCHEARD",
"BURSON",
"DELLON",
"MAGNETT",
"CANTINE",
"MELIKYAN",
"NATANI",
"NEASE",
"JUILFS",
"YOVINO",
"NORRELL",
"HIER",
"GOYNES",
"TOMASZYCKI",
"KIELTY",
"MUSE",
"MESERVY",
"ZEGAR",
"METENOSKY",
"HORNSBY",
"JOSSELYN",
"MCTHUNE",
"QUINTELA",
"KILIAN",
"COTTIER",
"GERLT",
"REGINO",
"LUTZE",
"VONDIELINGEN",
"MERKER",
"MADDEN",
"BALENTINE",
"EVELEIGH",
"SCHOR",
"WAECKERLIN",
"PANITZ",
"AUGUSTYNIAK",
"WIERZBA",
"GINNINGS",
"POUNDER",
"SIEGMUND",
"RUETZ",
"NEWKIRK",
"DEVIN",
"OURTH",
"LOYAL",
"WINGERD",
"HODSDON",
"KANSAS",
"FADNESS",
"RIZZIO",
"MORISSETTE",
"SAWLIVICH",
"MALENSEK",
"MANIGOLD",
"COSE",
"BROEGE",
"WESTHOUSE",
"NIEMITZIO",
"PENCEK",
"KANZENBACH",
"MALBOEUF",
"ZAHNKE",
"BRUMWELL",
"POLL",
"ARMANT",
"DRELICK",
"DENGER",
"LOPP",
"PAOLETTI",
"ALLERY",
"CASTILLEJA",
"LABIER",
"SCHOUTEN",
"GOSWAMI",
"HOFMANS",
"DISTASIO",
"PETTINGILL",
"MOROZ",
"DERIGGI",
"DYCUS",
"CATHERMAN",
"LUCIO",
"LEWITT",
"JUNKE",
"BICKART",
"PEET",
"MONOZ",
"ROTTINGHAUS",
"RITTENBERRY",
"OFTEDAHL",
"BACZEWSKI",
"CRAYNE",
"GRANIERI",
"CALPIN",
"FRIDMAN",
"BRAZIEL",
"NEWBROUGH",
"DEARY",
"SUMAN",
"SHIFLETT",
"KOSKI",
"YOSHIMORI",
"BALKEY",
"BARWICK",
"GOTHAM",
"TIMONE",
"EASTES",
"BARNELL",
"CARLSTEN",
"BRINGER",
"VANPROOSDY",
"WADLEIGH",
"SECKLER",
"COYLE",
"DEBROCK",
"OHM",
"DERER",
"RUSK",
"ROSIAN",
"KITCHING",
"RYTHER",
"BEJGER",
"SANTANO",
"COLAMARINO",
"AKI",
"CHMIEL",
"TOCCO",
"HIMMELRIGHT",
"HARPER",
"MEALEY",
"CALLENDER",
"CROCKETT",
"SCHEFFLER",
"KNAEBEL",
"MEADOW",
"SEGLER",
"SEGOVIANO",
"BEAULE",
"KNOEDLER",
"RANGEL",
"REWENKO",
"GRASSO",
"BINIENDA",
"JHONSON",
"LAHIP",
"STARRY",
"LUBBEN",
"RISKA",
"KERKHOFF",
"PERKO",
"ZACCAGNINI",
"RITZMAN",
"PATRYLAK",
"DESBIENS",
"BURNES",
"LUMPKINS",
"CHITTENDEN",
"TIMBERLAKE",
"MUNSCH",
"LAGORE",
"ALLNUTT",
"GUNTO",
"MAMO",
"TIPPETTS",
"GEHRETT",
"ILDEFONSO",
"MAGBITANG",
"RODAK",
"BARBERIS",
"ZIMA",
"ROVINSKY",
"QUITTNER",
"DUBONNET",
"ZETS",
"MCEACHERN",
"ELZINGA",
"GULOTTA",
"LIKENS",
"POPP",
"MATTHEWS",
"OKI",
"MOGAN",
"FODDRELL",
"DURDA",
"GALLICHIO",
"GOETSCH",
"GRAHAN",
"CEFARATTI",
"BRECKLEY",
"FAULCON",
"HATTABAUGH",
"SANTOYA",
"GERLACH",
"SPAZIANI",
"SATUNAS",
"AUDET",
"BOHRMAN",
"WINN",
"WEISBERG",
"RUGAMA",
"LECHLITER",
"MINCKLER",
"BALONEK",
"BRANNER",
"ZAUCHA",
"NABB",
"DIERKS",
"NAPOLES",
"SWIATKOWSKI",
"BOWARD",
"FARLESS",
"WOOLLEN",
"BURGO",
"AGRESTI",
"SMULL",
"MOZEE",
"BYNES",
"OOTON",
"LEDGER",
"MCMULLIN",
"KUZEMCHAK",
"SAMSEL",
"BRUNOT",
"CHIARITO",
"REICHENBERG",
"DANIS",
"LANGRIDGE",
"RINNER",
"COLLMEYER",
"WILGUS",
"BRINCKERHOFF",
"WEAVERS",
"BAKEN",
"LAFOREST",
"MARSKE",
"FERGUSON",
"HAGSTROM",
"CINNAMON",
"BOLDERY",
"DRECKMAN",
"REINARTZ",
"STRASSBERG",
"RENUART",
"STURGEON",
"MCNEIL",
"TULIS",
"PICCINONE",
"THALLS",
"MONSANTO",
"PEUGH",
"JAKOBSEN",
"BERNATH",
"MCCREERY",
"OLEKSIAK",
"MCDAY",
"SEIB",
"SWETS",
"TWIST",
"MONDRY",
"STUCKMAN",
"SAYAS",
"TAUBLEE",
"HOLLIDGE",
"AISPURO",
"SOLWOLD",
"JUKICH",
"DIPRIMA",
"DIPIAZZA",
"KRIEG",
"LOHRENZ",
"BLIMKA",
"PATRONE",
"FRIDAL",
"KANETA",
"GRIM",
"DUENO",
"SCHEMAN",
"HEITLAND",
"THAMPHIA",
"PATCHEN",
"BONSIGNORE",
"JEZIORSKI",
"KLOSTER",
"WEGLIN",
"MERICKEL",
"MANUES",
"SZUMSKI",
"ZAPPA",
"HECKAMAN",
"SCIERKA",
"WILLI",
"ARRICK",
"COMDEN",
"BELCOURT",
"CROUSE",
"SCHWARTZE",
"HOLTER",
"TRESTON",
"OPENSHAW",
"PRIOR",
"ZAMOSTNY",
"COLDIRON",
"SCHISSEL",
"ALDACO",
"STEHNEY",
"GAETH",
"MCWADE",
"BOWLES",
"CHALITA",
"VOLLSTEDT",
"KOOSMAN",
"BRANSEUM",
"BENNER",
"SEYAL",
"PARMENTER",
"FAIREY",
"BENEDETTI",
"HOLZNER",
"WINKEY",
"AZAPINTO",
"SORGATZ",
"SHEFTE",
"RIEKEN",
"KERNEY",
"TANK",
"BALMER",
"KAPPES",
"DEMARINIS",
"HALPERN",
"WINNEWISSER",
"BORROLLI",
"DUPRAS",
"SNOKE",
"PENKINS",
"RADMACHER",
"ECKARD",
"ELIADES",
"GONNEVILLE",
"PALMITESSA",
"SAVALA",
"LADT",
"DZURO",
"MCNEW",
"GILFILLAN",
"TOASTON",
"LLANEZA",
"CARDON",
"CHMIELEWSKI",
"PATTEE",
"KLASEN",
"TEICHER",
"RAIMANN",
"MIYOSHI",
"MISEMER",
"FLADGER",
"TEDDY",
"STRAUBE",
"STEINMEIZ",
"KINSOLVING",
"LLERENA",
"AGUINAGA",
"GROM",
"DOSWELL",
"VOLCKO",
"LOVEH",
"GAILUN",
"FUNAI",
"STERLING",
"VOGELGESANG",
"KRINKE",
"DACY",
"ESTER",
"PASCUCCI",
"MUNSEN",
"BUSTOS",
"SCHNOPP",
"STUARD",
"CADLETT",
"STACY",
"SURINA",
"SPEVACEK",
"MUHR",
"NEVER",
"AMEEN",
"ROBBINSON",
"GOTTA",
"VILE",
"LAGERBERG",
"CHUN",
"KLAVUHN",
"KUCHA",
"MATUSIK",
"FLITCROFT",
"NOWLAND",
"RASAVONG",
"GABRELCIK",
"TRUDE",
"LANIER",
"HIRAO",
"OFLYNN",
"HOOGHEEM",
"BOLKA",
"LAMBRIGHT",
"GILBO",
"JEANCY",
"SAKKINEN",
"SITTERUD",
"HIPPEN",
"STREETON",
"NORSE",
"VLCEK",
"FETTERS",
"MCCAFFITY",
"FIORELLA",
"KILLEBREW",
"BOCKHORN",
"CREVIER",
"TEMPE",
"ZYBIA",
"PLINER",
"CHANDRASEKARA",
"URFER",
"MENDONSA",
"BOHRER",
"HAMZIK",
"SOFKA",
"TANI",
"SCHUH",
"SOUFFRANT",
"BELIEU",
"RIOBE",
"LOVERN",
"ODMARK",
"VILLETAS",
"VANGOMPEL",
"BOHMKER",
"BURGOON",
"NOEGEL",
"MALADY",
"SANCHZ",
"HAMMACHER",
"KLIGER",
"GARTENHAUS",
"SWINEY",
"GAMMON",
"KOKUBUN",
"VONSTADEN",
"SWAMY",
"TRUSTY",
"GIESEN",
"BRITTON",
"BORYSEWICZ",
"CYPHER",
"MCKENDRY",
"FORNI",
"BROUK",
"CANES",
"MCGINNIS",
"STOA",
"EDDINGTON",
"VIMONT",
"HARTWELL",
"EBERSPACHER",
"CUBA",
"STETZEL",
"SCOTLAND",
"VALLES",
"RACZKOWSKI",
"GROCOTT",
"HINT",
"GROSVENOR",
"HETTLER",
"MORREN",
"LEBOUF",
"LARANCE",
"STEFANIAK",
"HINEY",
"JIRAK",
"FISCUS",
"PROVENCIO",
"NITA",
"ETHEN",
"ESHENBRENNER",
"SPROAT",
"ROSETH",
"TARAZON",
"QUEZAD",
"SHILL",
"POMA",
"BURLAZA",
"PAVAO",
"TREADWELL",
"IRIAS",
"SPRAFKA",
"WOLFINGER",
"RYCKMAN",
"HERRITT",
"COACH",
"SABEIHA",
"GEORGALES",
"OEHLER",
"ROMEY",
"TAUBMAN",
"DUESTERHAUS",
"FOURNET",
"NEALEY",
"VANDEVANTER",
"VIVERETTE",
"RINGS",
"RUMRILL",
"GOGA",
"EICHENAUER",
"PITCOCK",
"WEASEL",
"STRAMEL",
"VENNEMAN",
"CHE",
"DZINSKI",
"COLBERT",
"LEYDA",
"UMFLEET",
"HEITMEYER",
"MACVICAR",
"SIMS",
"HONGACH",
"LEIBERT",
"GERMERSHAUSEN",
"LOCHER",
"WALL",
"PILLETTE",
"CONNEALY",
"CASELDEN",
"DEGENHARDT",
"MURDY",
"PETRICONE",
"WOYAHN",
"CRETELLA",
"WILHOITE",
"SANDINE",
"WALP",
"PEAKE",
"BARBERO"        };
    }
}
