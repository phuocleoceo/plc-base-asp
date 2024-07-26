using System.Collections;

using PlcBase.Shared.Utilities;

namespace PlcTest.Shared.Utilities;

public class PasswordUtilityTest
{
    [Theory]
    [ClassData(typeof(TestData))]
    public void GetPasswordHash_ReturnsHash(string password)
    {
        string hash = PasswordUtility.GetPasswordHash(password);

        Assert.False(string.IsNullOrEmpty(hash), "Hash should not be empty");
        Assert.NotEqual(password, hash);
    }

    [Theory]
    [ClassData(typeof(TestData))]
    public void IsValidPassword_CorrectPassword_ReturnsTrue(string password)
    {
        string hash = PasswordUtility.GetPasswordHash(password);

        bool isValid = PasswordUtility.IsValidPassword(password, hash);

        Assert.True(isValid, "Password should be valid for the hash");
    }

    [Theory]
    [ClassData(typeof(TestData))]
    public void IsValidPassword_IncorrectPassword_ReturnsFalse(string password)
    {
        const string incorrectPassword = "WrongPassword";
        string hash = PasswordUtility.GetPasswordHash(password);

        bool isValid = PasswordUtility.IsValidPassword(incorrectPassword, hash);

        Assert.False(isValid, "Incorrect password should not be valid for the hash");
    }

    [Theory]
    [ClassData(typeof(TestData))]
    public void GetPasswordHash_And_IsValidPassword_WorkTogether(string password)
    {
        string hash = PasswordUtility.GetPasswordHash(password);
        bool isValid = PasswordUtility.IsValidPassword(password, hash);

        Assert.True(isValid, "Password hashed and then validated should be valid");
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
