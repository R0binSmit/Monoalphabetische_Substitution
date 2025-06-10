# Monoalphabetische Substitution

Die Anwendung besteht aus einer Bibliothek **Monoalphabetische.Application** mit der eigentlichen Verschlüsselungslogik und einem Konsolenprogramm **Monoalphabetische.Cli**. Nachrichten lassen sich damit sowohl verschlüsseln als auch entschlüsseln.

## Kompilieren
Voraussetzung für das Bauen und Ausführen ist das .NET SDK (Version 9.0 oder neuer). Das Projekt wird aus dem Repository-Verzeichnis heraus kompiliert:

```bash
dotnet build src/Monoalphabetische.sln
```

## Ausführen
Die Anwendung starten Sie mit:

```bash
dotnet run --project src/Monoalphabetische.Cli
```

Beim Start fragt das Programm nach der Nachricht, dem Schlüssel und dem Modus (Verschlüsselung oder Entschlüsselung) und zeigt das Ergebnis im Anschluss an.
