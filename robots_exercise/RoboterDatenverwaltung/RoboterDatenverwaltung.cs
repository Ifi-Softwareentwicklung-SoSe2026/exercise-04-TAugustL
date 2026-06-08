namespace RoboterDatenverwaltung;

using System.Text.Json;
using System.Text.Json.Serialization;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
[JsonDerivedType(typeof(Roboter), "roboter")]
[JsonDerivedType(typeof(Lieferroboter), "lieferroboter")]
public class Roboter : ISerializer, ICookable
{
    public Roboter(string name, int energielevel)
    {
        Name = name;
        Energielevel = energielevel;
    }
    public Roboter()
    {
        Name = "Unbekannt";
    }
    public string Name { get; set; }
    public int Energielevel { get; set; }
    public bool Active { get; set; }

    public void SpeichernAlsCSV(string dateipfad)
    {
        string inhalt = this is Lieferroboter lieferroboter
            ? $"{Name},{GetType().Name},{Energielevel},{lieferroboter.Lieferkapazität}"
            : $"{Name},{GetType().Name},{Energielevel}";
        File.WriteAllText(dateipfad, inhalt);
    }

    public static Roboter LadenAusCSV(string dateipfad)
    {
        string[] zeilen = File.ReadAllLines(dateipfad);
        string[] werte = zeilen[0].Split(',');

        string name = werte[0];
        string typ = werte[1];
        int energielevel = int.Parse(werte[2]);

        if (typ == "Lieferroboter" && werte.Length > 3)
        {
            uint lieferkapazitaet = uint.Parse(werte[3]);
            return new Lieferroboter(name, energielevel, lieferkapazitaet);
        }

        return new Roboter
        {
            Name = name,
            Energielevel = energielevel
        };
    }

    public void SpeichernAlsJSON(string dateipfad)
    {
        var json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(dateipfad, json);
    }

    public static Roboter LadenAusJSON(string dateipfad)
    {
        string json = File.ReadAllText(dateipfad);
        Roboter? roboter = JsonSerializer.Deserialize<Roboter>(json) ?? throw new InvalidDataException($"JSON-Datei konnte nicht gelesen werden: {dateipfad}");
        return roboter;
    }

    public virtual string GetStatus()
    {
        return $"Roboter - Name: {Name}, Typ: {GetType().Name} , Energielevel: {Energielevel}";
    }

    public virtual void Activate()
    {
        if (Energielevel > 0)
        {
            Console.WriteLine("activated");
            Active = true;
            Energielevel--;
            return;
        }

        Console.WriteLine("energy depleted");
    }

    public void Deactivate()
    {
        Active = false;
        Console.WriteLine("robot powered down");
    }

    public static void gibRezept()
    {
        Console.WriteLine("Mhm... Keksblech...");
    }

    public int gibNaerwertangaben()
    {
        return 2;
    }
    
}

public class Lieferroboter : Roboter
{
    public uint Lieferkapazität { get; set; }
    public Lieferroboter() : base()
    {
    }
    public Lieferroboter(string name, int energielevel, uint lieferkapazität) : base(name, energielevel)
    {
        Lieferkapazität = lieferkapazität;
    }

    public override string GetStatus()
    {
        return $"Lieferroboter - Name: {Name}, Energielevel: {Energielevel}, Lieferkapazität: {Lieferkapazität}";
    }
}