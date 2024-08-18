using System;

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
    public void shouldHandleMultipleNumbers()
    {
        int sum = calculator.Add("12");

        Assert.Equal(3, sum);
    }

    [Fact]
    public void shouldHandleManyNumbers()
    {
        int sum = calculator.Add("110598132");

        Assert.Equal(30, sum);
    }

    [Fact]
    public void shouldHandleUnknownCharacters()
    {
        int sum = calculator.Add("1//? 5");

        Assert.Equal(6, sum);
    }

    [Fact]
    public void shouldHandleNewlineCharacters()
    {
        int sum = calculator.Add("1\n61");

        Assert.Equal(8, sum);
    }

    [Fact]
    public void shouldHandleDelimiters()
    {
        int sum = calculator.Add("//,\n1\n,6?h1");

        Assert.Equal(62, sum);
    }

    [Fact]
    public void shouldHandleNegativeNumbers()
    {

        Assert.Throws<ArgumentException>(() => calculator.Add("-1,2"))
            .Message.Equals("Negatives not allowed: -1");
    }

    [Fact]
    public void shouldHandleMultipleNegativeNumbers()
    {

        var exception = Assert.Throws<ArgumentException>(() => calculator.Add("-1,2-8/-5-1"));
        Assert.Equal("Negatives not allowed: -1,-8,-5,-1", exception.Message);
    }

    [Fact]
    public void shouldHandleMultipleNegativeNumbersWithDelimiters()
    {

        var exception = Assert.Throws<ArgumentException>(() => calculator.Add("//,\n-1,2,-8,-5,-1"));
        Assert.Equal("Negatives not allowed: -1,-8,-5,-1", exception.Message);
    }

    [Fact]
    public void shouldHandleMultipleMixedNegativeNumbersWithDelimiters()
    {

        var exception = Assert.Throws<ArgumentException>(() => calculator.Add("//,\n-1,2-8,-5,-1"));
        Assert.Equal("Negatives not allowed: -1,-5,-1", exception.Message);
    }

    [Fact]
    public void shouldHandleDelimitersOfAnyLength()
    {
        int sum = calculator.Add("//|||\n1|||2|||3");

        Assert.Equal(6, sum);
    }
}