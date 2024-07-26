using System.Collections;

using PlcBase.Shared.Utilities;

namespace PlcTest.Shared.Utilities;

public class PasswordUtilityTest
{
    [Theory]
    [ClassData(typeof(TestData))]
    public void GetPasswordHash_ReturnsHash(string password)
    {
        // Arrange
        string hash = PasswordUtility.GetPasswordHash(password);

        // Act
        bool isNullOrEmptyHash = string.IsNullOrEmpty(hash);

        // Assert
        Assert.False(isNullOrEmptyHash, "Hash should not be empty");
        Assert.NotEqual(password, hash);
    }

    [Theory]
    [ClassData(typeof(TestData))]
    public void IsValidPassword_CorrectPassword_ReturnsTrue(string password)
    {
        // Arrange
        string hash = PasswordUtility.GetPasswordHash(password);

        // Act
        bool isValid = PasswordUtility.IsValidPassword(password, hash);

        // Assert
        Assert.True(isValid, "Password should be valid for the hash");
    }

    [Theory]
    [ClassData(typeof(TestData))]
    public void IsValidPassword_IncorrectPassword_ReturnsFalse(string password)
    {
        // Arrange
        const string incorrectPassword = "WrongPassword";
        string hash = PasswordUtility.GetPasswordHash(password);

        // Act
        bool isValid = PasswordUtility.IsValidPassword(incorrectPassword, hash);

        // Assert
        Assert.False(isValid, "Incorrect password should not be valid for the hash");
    }
}

public class TestData : IEnumerable<object[]>
{
    private readonly List<object[]> _data =
        new()
        {
            new object[] { "TestPassword1" },
            new object[] { "TestPassword2" },
            new object[] { "TestPassword3" }
        };

    public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public class TestDataGenerator : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { "TestPassword1" };
        yield return new object[] { "TestPassword2" };
        yield return new object[] { "TestPassword3" };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
