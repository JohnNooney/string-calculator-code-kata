namespace StringCalculatorApp.Tests;

public class StringCalculatorTest
{
    StringCalculator calculator = new StringCalculator();

    [Fact]
    public void shouldHandleEmptyString()
    {
        int sum = calculator.Add("");

        Assert.Equal(0, sum);
    }

    [Fact]
    public void shouldHandleSingleNumber()
    {
        int sum = calculator.Add("1");

        Assert.Equal(1, sum);
    }

    [Fact]
    public void shouldHandleMultipleNumber()
    {
        int sum = calculator.Add("12");

        Assert.Equal(3, sum);
    }
}