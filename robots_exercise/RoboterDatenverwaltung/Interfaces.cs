namespace RoboterDatenverwaltung;

public interface ISerializer
{
    void SpeichernAlsJSON(string dateipfad);
    static abstract Roboter LadenAusJSON(string dateipfad);
    void SpeichernAlsCSV(string dateipfad);
    static abstract Roboter LadenAusCSV(string dateipfad);
}

public interface ICookable
{
    static abstract void gibRezept();
    int gibNaerwertangaben();
}