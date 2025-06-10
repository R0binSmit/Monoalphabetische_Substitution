using Monoalphabetische.Application;
using Xunit;

namespace Monoalphabetische.Application.Tests;

public class MessageHelperTests
{
    [Fact]
    public void IsValid_ReturnsTrue_ForValidMessage()
    {
        var message = new Message { DecryptedMessage = "HELLO", Key = 1 };
        Assert.True(message.IsValid);
    }

    [Fact]
    public void IsValid_ReturnsFalse_ForUnsupportedCharacter()
    {
        var message = new Message { DecryptedMessage = "hello!", Key = 1 };
        Assert.False(message.IsValid);
    }

    [Fact]
    public void IsValid_ReturnsFalse_ForTooLargeKey()
    {
        var message = new Message { DecryptedMessage = "HELLO", Key = MessageHelper.Alphabeth.Length };
        Assert.False(message.IsValid);
    }
}
