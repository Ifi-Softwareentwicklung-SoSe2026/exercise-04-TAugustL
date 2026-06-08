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
            ? $"{Name},{Energielevel},{lieferroboter.Lieferkapazität}"
            : $"{Name},{Energielevel}";
        File.WriteAllText(dateipfad, inhalt);
    }

    public static Roboter LadenAusCSV(string dateipfad)
    {
        string[] zeilen = File.ReadAllLines(dateipfad);
        string[] werte = zeilen[0].Split(',');

        string name = werte[0];
        int energielevel = int.Parse(werte[2]);

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
        return $"Roboter - Name: {Name}, Energielevel: {Energielevel}";
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

    public static string gibRezept()
    {
        return "Mhm... Keksblech...";
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
    public Lieferroboter(string name, int energielevel, uint lieferkapazität) : base(name, "Lieferroboter", energielevel)
    {
        Lieferkapazität = lieferkapazität;
    }

    public override string GetStatus()
    {
        return $"Lieferroboter - Name: {Name}, Energielevel: {Energielevel}, Lieferkapazität: {Lieferkapazität}";
    }
}