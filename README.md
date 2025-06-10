# Monoalphabetic Substitution

This repository contains the library **Monoalphabetische.Application** with the
core encryption logic and the console application **Monoalphabetische.Cli**. It
allows you to encrypt and decrypt messages using a simple substitution cipher.

## Build
To build and run the solution you need the .NET SDK (version 9.0 or newer). Run
the following command from the repository root:

```bash
dotnet build src/Monoalphabetische.sln
```

## Usage
Launch the program with:

```bash
dotnet run --project src/Monoalphabetische.Cli -- [--message <text>] [--key <number>] [--mode <E|D|G>] [--skip-input]
```

Providing `--message`, `--key` and `--mode` skips the interactive prompts. The
option `--skip-input` disables all prompts entirely. Modes `E` (Encrypt) and `D`
(Decrypt) require a key. In mode `G` the key is determined by frequency analysis
of the encrypted text and may therefore be omitted.

The command line options are parsed using `CommandLineParser` and validated
with `FluentValidation`.
