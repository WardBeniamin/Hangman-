# Hänga Gubbe - C# Console App

Ett enkelt konsolspel där användaren gissar bokstäver för att lista ut ett ord innan försöken tar slut.

## Funktioner

- **OOP (Objektorienterad Programmering)**: Strukturerad med klasser och interface.
- **Felhantering**: Hanterar ogiltig inmatning med `try-catch`-liknande mekanismer.
- **JSON-hantering**: Sparar spelhistorik i en `game_history.json`-fil.
- **Spectre.Console Styling**: Användning av Spectre.Console för färgglad konsolutskrift.
- **Användarvänlig navigering**: Konsolen visar ledtrådar och återstående försök.
- **LINQ**: Kontroll av om hela ordet är gissat.

## Förutsättningar

För att köra projektet krävs:
- .NET SDK installerad
- [Spectre.Console](https://spectresystems.github.io/spectre.console/) bibliotek (installera med `dotnet add package Spectre.Console`)

## Så här kör du

1. Klona detta repo till din dator:
   ```bash
   git clone <repo-url>
   cd <repo-folder>
