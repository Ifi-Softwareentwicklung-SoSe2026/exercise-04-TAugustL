namespace RoboterDatenverwaltung;

using System.Text.Json;
using System.Text.Json.Serialization;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
[JsonDerivedType(typeof(Roboter), "roboter")]
[JsonDerivedType(typeof(Lieferroboter), "lieferroboter")]
public class Roboter : ISerializer
{
    public Roboter(string name, string typ, int energielevel)
    {
        Name = name;
        Typ = typ;
        Energielevel = energielevel;
    }
    public Roboter()
    {
        Name = "Unbekannt";
        Typ = "Unbekannt";
    }
    public string Name { get; set; }
    public string Typ { get; set; } // z. B. "Lieferroboter", "Schwimmroboter", etc.
    public int Energielevel { get; set; }

    public void SpeichernAlsCSV(string dateipfad)
    {
        string inhalt = this is Lieferroboter lieferroboter
            ? $"{Name},{Typ},{Energielevel},{lieferroboter.Lieferkapazität}"
            : $"{Name},{Typ},{Energielevel}";
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
            int lieferkapazitaet = int.Parse(werte[3]);
            return new Lieferroboter(name, energielevel, lieferkapazitaet);
        }

        return new Roboter
        {
            Name = name,
            Typ = typ,
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
        return $"Roboter - Name: {Name}, Typ: {Typ}, Energielevel: {Energielevel}";
    }

    public virtual void Activate()
    {
        if (Energielevel > 0)
        {
            Console.WriteLine("activated");
            Energielevel--;
            return;
        }

        Console.WriteLine("energy depleted");
    }
}

public class Lieferroboter : Roboter
{
    public int Lieferkapazität { get; set; }
    public Lieferroboter() : base()
    {
        Typ = "Lieferroboter";
    }
    public Lieferroboter(string name, int energielevel, int lieferkapazität) : base(name, "Lieferroboter", energielevel)
    {
        Lieferkapazität = lieferkapazität;
    }

    public override string GetStatus()
    {
        return $"Lieferroboter - Name: {Name}, Typ: {Typ}, Energielevel: {Energielevel}, Lieferkapazität: {Lieferkapazität}";
    }
}