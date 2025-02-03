namespace SoftwareCatalog.Tests;

public class UnitTest1
{
    [Fact]
    public void CanAddTenAndTwentyInDotNet()
    {
        // "Given"
        int a = 10, b = 20, answer;

        // When
        answer = a + b; // System Under Test (SUT)

        // Then 
        Assert.Equal(30, answer);
    }

    [Theory]
    [InlineData(10, 20, 30)]
    [InlineData(2, 3, 5)]
    [InlineData(10, 2, 12)]
    public void CanAddAnyTwoIntegers(int a, int b, int expected)
    {
        var answer = a + b;

        Assert.Equal(expected, answer);
    }
}