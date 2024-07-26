using PlcBase.Shared.Utilities;

namespace PlcTest.Shared.Utilities;

public class JsonUtilityTest
{
    [Theory]
    [MemberData(
        nameof(JsonUtilityTestData.StringifyTestData),
        MemberType = typeof(JsonUtilityTestData)
    )]
    public void Stringify_ShouldReturnCorrectJsonString(object obj, string expectedJson)
    {
        // Act
        string result = JsonUtility.Stringify(obj);

        // Assert
        Assert.Equal(expectedJson, result);
    }

    [Theory]
    [MemberData(
        nameof(JsonUtilityTestData.ParseTestData),
        MemberType = typeof(JsonUtilityTestData)
    )]
    public void Parse_ShouldReturnCorrectObject(string json, string expectedName, int expectedAge)
    {
        // Act
        var result = JsonUtility.Parse<dynamic>(json);

        // Assert
        if (string.IsNullOrWhiteSpace(json))
        {
            Assert.Null(result);
        }
        else
        {
            Assert.Equal(expectedName, (string)result.Name);
            Assert.Equal(expectedAge, (int)result.Age);
        }
    }
}

public static class JsonUtilityTestData
{
    public static IEnumerable<object[]> StringifyTestData =>
        new List<object[]>
        {
            new object[] { new { Name = "Alice", Age = 30 }, "{\"Name\":\"Alice\",\"Age\":30}" },
            new object[] { new { Name = "Bob", Age = 25 }, "{\"Name\":\"Bob\",\"Age\":25}" },
            new object[] { null, "" }
        };

    public static IEnumerable<object[]> ParseTestData()
    {
        yield return new object[] { "{\"Name\":\"Alice\",\"Age\":30}", "Alice", 30 };
        yield return new object[] { "{\"Name\":\"Bob\",\"Age\":25}", "Bob", 25 };
        yield return new object[] { "", null, 0 };
    }
}
