using RoboterDatenverwaltung;

class Program
{
    private const string ROBOT_DATA_FOLDER = "robot_data";
    private const int ROBOT_COUNT = 10;
    private static readonly Random RandomGenerator = new();
    private static readonly string[] StandardTypen =
    [
        "ServiceRoboter",
        "Schwimmroboter",
        "Wartungsroboter",
        "Explorationsroboter"
    ];

    static void Main(string[] args)
    {
        List<Roboter> robot = InitialisiereZufaelligeRoboter(ROBOT_COUNT);

        Console.WriteLine("Initiale Roboter:");
        GibStatusAus(robot);

        SpeichereAlleRoboter(robot, ROBOT_DATA_FOLDER);

        robot.Clear();
        Console.WriteLine("\nRoboterliste geleert.");

        robot = LadeAlleCsvRoboter(ROBOT_DATA_FOLDER);
        Console.WriteLine("\nAus CSV geladene Roboter:");
        GibStatusAus(robot);

        robot.Clear();
        robot = LadeAlleJsonRoboter(ROBOT_DATA_FOLDER);
        Console.WriteLine("\nAus JSON geladene Roboter:");
        GibStatusAus(robot);
        
    }

    private static List<Roboter> InitialisiereZufaelligeRoboter(int anzahl)
    {
        var roboter = new List<Roboter>();

        for (int i = 0; i < anzahl; i++)
        {
            roboter.Add(ErzeugeZufaelligenRoboter(i + 1));
        }

        return roboter;
    }

    private static Roboter ErzeugeZufaelligenRoboter(int nummer)
    {
        string name = $"Robo_{nummer:D2}";
        int energielevel = RandomGenerator.Next(0, 101);
        bool istLieferroboter = RandomGenerator.Next(0, 2) == 1;

        if (istLieferroboter)
        {
            int lieferkapazitaet = RandomGenerator.Next(1, 51);
            return new Lieferroboter(name, energielevel, lieferkapazitaet);
        }

        string typ = StandardTypen[RandomGenerator.Next(0, StandardTypen.Length)];
        return new Roboter(name, typ, energielevel);
    }

    private static void GibStatusAus(IEnumerable<Roboter> roboter)
    {
        foreach (Roboter einzelnerRoboter in roboter)
        {
            Console.WriteLine(einzelnerRoboter.GetStatus());
        }
    }

    private static void SpeichereAlleRoboter(IEnumerable<Roboter> roboter, string ordner)
    {
        Directory.CreateDirectory(ordner);

        RemoveExistingRobots(ordner);

        int index = 1;
        foreach (Roboter einzelnerRoboter in roboter)
        {
            string basisname = $"roboter_{index:D2}";
            string csvPfad = Path.Combine(ordner, $"{basisname}.csv");
            string jsonPfad = Path.Combine(ordner, $"{basisname}.json");

            einzelnerRoboter.SpeichernAlsCSV(csvPfad);
            einzelnerRoboter.SpeichernAlsJSON(jsonPfad);
            index++;
        }
    }

    private static void RemoveExistingRobots(string ordner)
    {
        foreach (string datei in Directory.GetFiles(ordner, "*.csv"))
        {
            File.Delete(datei);
        }

        foreach (string datei in Directory.GetFiles(ordner, "*.json"))
        {
            File.Delete(datei);
        }
    }

    private static List<Roboter> LadeAlleCsvRoboter(string ordner)
    {
        return Directory
            .GetFiles(ordner, "*.csv")
            .OrderBy(datei => datei)
            .Select(Roboter.LadenAusCSV)
            .ToList();
    }

    private static List<Roboter> LadeAlleJsonRoboter(string ordner)
    {
        return Directory
            .GetFiles(ordner, "*.json")
            .OrderBy(datei => datei)
            .Select(Roboter.LadenAusJSON)
            .ToList();
    }
}