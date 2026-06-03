<!--

author:   Volker Göhler
email:    volker.goehler@informatik.tu-freiberg.de
version:  0.0.2
language: de
narrator: Deutsch Female

edit: true
date: 2026-05-28

comment:  Übung Softwareentwicklung 04 -- UML Klassendiagramme

import: https://raw.githubusercontent.com/liascript-templates/plantUML/master/README.md

link:   https://raw.githubusercontent.com/vgoehler/LiaScript_CSS_Provider/refs/heads/main/dist/university.css

tags: [ Sommersemester2026, Softwareentwicklung, Übung04]

-->

[![LiaScript Course](https://raw.githubusercontent.com/LiaScript/LiaScript/master/badges/course.svg)](https://liascript.github.io/course/?https://raw.githubusercontent.com/Ifi-Softwareentwicklung-SoSe2026/exercise_04/refs/heads/main/README.md)

#  Aufgabe 04

Softwareentwicklung SoSe2026
============================

Bearbeitungszeitraum
====================

*01. Juni - 07. Juni 2026*

## Neue Aufgaben für diese Woche

In dieser Woche geht es UML Klassendiagramme und wie wir diese in C# implementieren.

Wir arbeiten wieder mit GitHub. Die Arbeitsaufträge finden sich in den Issues. Wenn ihr direkte fragen habt dann stellt diese mit der Kommentarfunktion des Issues. Mit zum Beispiel: `\help Wieviele Fälle hat Klingonisch?`.

---

### **📌 PlantUML: Klassendiagramme**

*Lernziele:* UML Klassendiagramme lesen und verstehen, ändern, implementieren, branches, pull requests, und issues in GitHub nutzen.

#### Grundlegende Syntax

Ein Klassendiagramm beginnt mit `@startuml` und endet mit `@enduml`.

Beispiel: Einfache Klasse
-------------------

```text @plantUML
@startuml
class Himmelskoerper {
  - name: string
  - katalogNummer: uint
  + ToString(): string
}
@enduml
```

```text 
@startuml
class Himmelskoerper {
  - name: string
  - katalogNummer: uint
  + ToString(): string
}
@enduml
```
@plantUML.eval(png)


- Klasse: `class Klassenname`
- Attribute: `-` (private), `+` (public), `#` (protected) Syntax: `[Sichtbarkeit] Name: Typ`
- Methoden: Wie Attribute, aber mit `()` und optionalem Rückgabetyp.

#### Beziehungen zwischen Klassen

| Beziehung | Symbol | Bedeutung | Beispiel |   
| --------- | ------ | --------- | -------- |
| Vererbung | `<|--` | Klasse B erbt von Klasse A | `A <|-- B` |
| Interface | `<|..` | Klasse B implementiert Interface A | `A <|.. B` |
| Assoziation | `-->` |Klasse A nutzt Klasse B | `A --> B` |
| Aggregation | `o--` |Klasse A enthält Klasse B (B existiert unabhängig) | `A o-- B` |
| Komposition | `*--` |Klasse A enthält Klasse B (B existiert nicht ohne A) | `A *-- B` |
| Abhängigkeit| `..>` | Schwache Abhängigkeit (z. B. Parameter) | `A ..> B` |
    
Beispiel: Vererbung und Assoziation
-------------------

```text @plantUML
@startuml
class Himmelskoerper {
  - name: string
  + ToString(): string
}

class Planet {
  - umlaufzeit: float
}

class Mond {
  - planet: Planet
}

Himmelskoerper <|-- Planet
Planet <|-- Mond
Mond --> Planet : umkreist
@enduml
```

```text
@startuml
class Himmelskoerper {
  - name: string
  + ToString(): string
}

class Planet {
  - umlaufzeit: float
}

class Mond {
  - planet: Planet
}

Himmelskoerper <|-- Planet
Planet <|-- Mond
Mond --> Planet : umkreist
@enduml
```
@plantUML.eval(png)

#### Sichtbarkeiten (Visibility)

| Symbol | Bedeutung |
| ------ | --------- |
| `-`    | private |
| `#`    | protected |
| `~`    | package private (internal) |
| `+`    | public |


Beispiel:
-------------------

```text @plantUML
@startuml
class Raumschiff {
  - privateField: int
  # protectedField: string
  + publicMethod(): void
  ~ internalMethod(): void
}
@enduml
```

```text 
@startuml
class Raumschiff {
  - privateField: int
  # protectedField: string
  + publicMethod(): void
  ~ internalMethod(): void
}
@enduml
```
@plantUML.eval(png)

#### Abstrakte Klassen und Interfaces

- Abstrakte Klasse: `abstract class Klassenname`
- Interface: `interface InterfaceName`

Beispiel:
-------------------

```text @plantUML
@startuml
abstract class Himmelskoerper {
  {abstract} + BerechneUmlaufbahn(): void
}

interface IBewegbar {
  + Bewege(): void
}

class Planet {
  + BerechneUmlaufbahn(): void
  + Bewege(): void
}

Himmelskoerper <|-- Planet
IBewegbar <|.. Planet
@enduml
```

```text
@startuml
abstract class Himmelskoerper {
  {abstract} + BerechneUmlaufbahn(): void
}

interface IBewegbar {
  + Bewege(): void
}

class Planet {
  + BerechneUmlaufbahn(): void
  + Bewege(): void
}

Himmelskoerper <|-- Planet
IBewegbar <|.. Planet
@enduml
```
@plantUML.eval(png)

#### Enums

Beispiel:
-------------------

```text @plantUML
@startuml
enum HimmelskoerperTyp {
  Stern
  Planet
  Mond
}

class Himmelskoerper {
  - typ: HimmelskoerperTyp
}

Himmelskoerper *-- HimmelskoerperTyp : ist vom Typ
@enduml
```

```text
@startuml
enum HimmelskoerperTyp {
  Stern
  Planet
  Mond
}

class Himmelskoerper {
  - typ: HimmelskoerperTyp
}

Himmelskoerper *-- HimmelskoerperTyp : ist vom Typ
@enduml
```
@plantUML.eval(png)

#### Pakete (Namespaces)

Beispiel:
-------------------

```text @plantUML
@startuml
package Raumfahrt {
  class Raumschiff {
    + Starten(): void
  }

  class Mission {
    + Planen(): void
  }
}

Raumschiff --> Mission : nutzt
@enduml
```

```text
@startuml
package Raumfahrt {
  class Raumschiff {
    + Starten(): void
  }

  class Mission {
    + Planen(): void
  }
}

Raumschiff --> Mission : nutzt
@enduml
```
@plantUML.eval(png)

#### Notizen (Notes)

Notizen können an Klassen, Methoden oder Beziehungen angehängt werden.

Beispiel:
-------------------

```text @plantUML
@startuml
class Planet {
  - name: string
}

note top of Planet: Diese Klasse repräsentiert einen Planeten.
note right of Planet:name Muss einzigartig sein.
@enduml
```

```text
@startuml
class Planet {
  - name: string
}

note top of Planet: Diese Klasse repräsentiert einen Planeten.
note right of Planet:name Muss einzigartig sein.
@enduml
```
@plantUML.eval(png)

#### Minimales Beispiel für Studierende (Zusammenfassung)

```text @plantUML
@startuml
interface IBuilder<T> {
  + static Build(): T
}
abstract class Himmelskoerper {
  - name: string
  + ToString(): string
  + static Build(): Himmelskoerper
}

IBuilder <|.. Himmelskoerper

class Stern {
  - leuchtkraft: float
  + BerechneLeuchtkraft(): void
  + static Build(): Stern
}

class Planet {
  - umlaufzeit: float
  + BerechneUmlaufbahn(): void
  + static Build(): Planet
}

class Mond {
  - planet: Planet
  + static Build(): Mond
}

Himmelskoerper <|-- Planet
Planet <|-- Mond
Himmelskoerper <|-- Stern 
Mond --> Planet : umkreist

note top of Himmelskoerper: Basisklasse für alle Himmelskörper.
@enduml
```

```text
@startuml
interface IBuilder<T> {
  + static Build(): T
}
abstract class Himmelskoerper {
  - name: string
  + ToString(): string
  + static Build(): Himmelskoerper
}

IBuilder <|.. Himmelskoerper

class Stern {
  - leuchtkraft: float
  + BerechneLeuchtkraft(): void
  + static Build(): Stern
}

class Planet {
  - umlaufzeit: float
  + BerechneUmlaufbahn(): void
  + static Build(): Planet
}

class Mond {
  - planet: Planet
  + static Build(): Mond
}

Himmelskoerper <|-- Planet
Planet <|-- Mond
Himmelskoerper <|-- Stern 
Mond --> Planet : umkreist

note top of Himmelskoerper: Basisklasse für alle Himmelskörper.
@enduml
```
@plantUML.eval(png)

### Zusammenfassung der wichtigsten Konzepte für die Übung

1. Klassen definieren mit Attributen und Methoden.
2. Beziehungen zwischen Klassen (`<|--`, `-->`, `o--`, `*--`).
3. Sichtbarkeiten (`-`, `#`, `~`, `+`).
4. Abstrakte Klassen und Interfaces für Abstraktion.
5. Enums für Typdefinitionen.
6. Pakete zur Gruppierung.
7. Notizen für Erläuterungen.

## Part 1: UML Diagram zum Code in `robots_exercise`

Hier bitte den Code aus `robots_exercise` in ein UML Diagramm überführen.


```text @plantUML
@startuml

class Roboter
{
  + Roboter(string, string, int): Roboter  
  + Roboter(): Roboter

  + Name: string
  + Typ: string
  + Energielevel: int

  + SpeichernAlsCSV(string): void
  + static LadenAusCSV(string): Roboter
  + SpeichernAlsJSON(string): void
  + static LadenAusJSON(string): Roboter
  + virtual GetStatus(): string
  + virtual Activate(): void
}

class Lieferroboter
{
  + Lieferkapazität: int
  + Lieferroboter(string, int, int): Lieferroboter
  + Lieferroboter(): Lieferroboter
  + override GetStatus(): string
}

interface ISerializer
{
    + SpeichernAlsJSON(string): void
    + static abstract LadenAusJSON(string): Roboter
    + SpeichernAlsCSV(string): void
    + static abstract LadenAusCSV(string): Roboter
}

ISerializer <|.. Roboter
Roboter <|-- Lieferroboter

@enduml
```
@plantUML.eval(png)


## Part 2: Überarbeitung des UML Diagrams

Hier soll das überarbeitete UML Diagramm zum Code in `robots_exercise` erstellt werden.

Das Feld 'Typ' ist unnötig, da dessen Funktion bereits von der Klasse übernommen wird.
Das Feld 'Active' gibt Auskunft über den aktuellen Zustand des Roboters, was mit der neuen Methode
Deactivate() ebenfalls deaktiviert werden kann. Die Lieferkapazität wurde zu einem uint verändert,
da diese logischerweise immer positiv ist.
Das Interface 'ICookable' ist essenziel um eine reibungslose und zukunftssichere Nutzung des
Robotikprojektes zu ermöglichen. Mit dem Interface wird eine unvergleichliche architektonische Barrierefreiheit und fortschrittliche Konsequenz geleistet. Durch hochmoderne technische Lösungen wird das Problem der nicht-essbaren Roboter
endgültig beseitigt: 3D-Drucker nutzen Schokolade und Zucker um nicht nur essbare, sondern auch durch die Umwelt zersetzbare
Komponenten zu drucken. Das Interface ist nicht nur modern — es definiert was 'modern' im technischen Zeitalter des 21. Jahrhundert bedeutet. ICookable, definiert die moderne — heute und in Zukunft. 

```text @plantUML
@startuml

class Roboter
{
  + Roboter(string, int): Roboter  
  + Roboter(): Roboter

  + Name: string
  + Energielevel: int
  + Active: bool

  + SpeichernAlsCSV(string): void
  + static LadenAusCSV(string): Roboter
  + SpeichernAlsJSON(string): void
  + static LadenAusJSON(string): Roboter
  + virtual GetStatus(): string
  + virtual Activate(): void
  + virtual Deactivate(): void
}

class Lieferroboter
{
  + Lieferkapazität: uint
  + Lieferroboter(string, int, uint): Lieferroboter
  + Lieferroboter(): Lieferroboter
  + override GetStatus(): string
}

interface ISerializer
{
    + SpeichernAlsJSON(string): void
    + static abstract LadenAusJSON(string): Roboter
    + SpeichernAlsCSV(string): void
    + static abstract LadenAusCSV(string): Roboter
}

interface ICookable
{
    + static abstract gibRezept(): void
    + gibNaerwertangaben(): int
}

ISerializer <|.. Roboter
ICookable <|.. Roboter
Roboter <|-- Lieferroboter

@enduml
```
@plantUML.eval(png)

