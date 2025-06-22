using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Monoalphabetische.Application;

namespace Monoalphabetische.Gui.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private readonly SubstitutionService _service = new();

    [ObservableProperty]
    private string _inputText = string.Empty;

    [ObservableProperty]
    private string? _keyText;

    [ObservableProperty]
    private string _outputText = string.Empty;

    [RelayCommand]
    private void Encrypt()
    {
        if (string.IsNullOrWhiteSpace(InputText))
            return;

        int key;
        if (!int.TryParse(KeyText, out key))
        {
            key = new Random().Next(0, MessageHelper.CharsetSize);
        }

        var result = _service.Encrypt(key, InputText);
        KeyText = result.Key.ToString();
        OutputText = result.EncryptedMessage ?? string.Empty;
    }

    [RelayCommand]
    private void Decrypt()
    {
        if (string.IsNullOrWhiteSpace(InputText))
            return;

        Message result;
        if (string.IsNullOrWhiteSpace(KeyText))
        {
            result = _service.DecryptWithGuessedKey(InputText);
            KeyText = result.Key.ToString();
        }
        else if (int.TryParse(KeyText, out var key))
        {
            result = _service.Decrypt(key, InputText);
        }
        else
        {
            return;
        }

        OutputText = result.DecryptedMessage ?? string.Empty;
    }
}
