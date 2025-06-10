using Monoalphabetische.Application;
using Xunit;

namespace Monoalphabetische.Application.Tests;

public class SubstitutionServiceTests
{
    [Fact]
    public void Encrypt_ReturnsExpectedCipher()
    {
        var service = new SubstitutionService();
        var result = service.Encrypt(1, "HELLO");
        Assert.Equal("IFMMP", result.EncryptedMessage);
    }

    [Fact]
    public void Decrypt_ReturnsOriginalText()
    {
        var service = new SubstitutionService();
        var result = service.Decrypt(1, "IFMMP");
        Assert.Equal("HELLO", result.DecryptedMessage);
    }

    [Fact]
    public void DecryptWithGuessedKey_WorksForShortMessage()
    {
        var service = new SubstitutionService();
        var encrypted = service.Encrypt(1, "HELLO");
        var result = service.DecryptWithGuessedKey(encrypted.EncryptedMessage!);
        Assert.Equal("HELLO", result.DecryptedMessage);
        Assert.Equal(1, result.Key);
    }
}
