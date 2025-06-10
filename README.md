# Monoalphabetische Substitution

Dieses Projekt stellt eine einfache Konsolenanwendung dar, die eine monoalphabetische Substitution implementiert. Nachrichten lassen sich damit sowohl verschlüsseln als auch entschlüsseln.

## Kompilieren
Voraussetzung für das Bauen und Ausführen ist das .NET SDK (Version 9.0 oder neuer). Das Projekt wird aus dem Repository-Verzeichnis heraus kompiliert:

```bash
dotnet build src/Monoalphabetische.Substitution
```

## Ausführen
Die Anwendung starten Sie mit:

```bash
dotnet run --project src/Monoalphabetische.Substitution
```

Beim Start fragt das Programm nach der Nachricht, dem Schlüssel und dem Modus (Verschlüsselung oder Entschlüsselung) und zeigt das Ergebnis im Anschluss an.

