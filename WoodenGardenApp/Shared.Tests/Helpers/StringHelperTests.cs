using FluentAssertions;
using NUnit.Framework;
using WoodenGardenApp.Shared.Helpers;

namespace Shared.Tests.Helpers;

[TestFixture]
public class StringHelperTests
{
    [Test]
    public void IsNullOrWhiteSpace_TextIsNull_ReturnTrue()
    {
        //Arrange
        string? text = null;
        
        //Act
        var result = text.IsNullOrWhiteSpace();
        
        //Assert
        result.Should().BeTrue();
    }

    [Test]
    public void IsNullOrWhiteSpace_TextIsEmpty_ReturnTrue()
    {
        //Arrange
        var text = string.Empty;
        
        //Act
        var result = text.IsNullOrWhiteSpace();
        
        //Assert
        result.Should().BeTrue();
    }

    [Test]
    public void IsNullOrWhiteSpace_TextIsProvided_ReturnFalse()
    {
        //Arrange
        const string text = "test";

        //Act
        var result = text.IsNullOrWhiteSpace();

        //Assert
        result.Should().BeFalse();
    }

    [Test]
    public void Format_FormatAndArgumentsNull_ReturnEmptyString()
    {
        //Arrange
        string? format = null;
        object[]? arguments = null;
        
        //Act
        var result = format!.Format(arguments!);
        
        //Assert
        result.Should().BeEmpty();
    }
    
    [Test]
    public void Format_FormatNullArgumentsProvided_ReturnEmptyString()
    {
        //Arrange
        string? format = null;
        object[] arguments = {"arg1", "arg2"};
        
        //Act
        var result = format!.Format(arguments);
        
        //Assert
        result.Should().BeEmpty();
    }
    
    [Test]
    public void Format_FormatProvidedArgumentsNull_ReturnFormat()
    {
        //Arrange
        const string? format = "format";
        object[]? arguments = null;
        
        //Act
        var result = format.Format(arguments);
        
        //Assert
        result.Should().Be(format);
    }
    
    [Test]
    public void Format_FormatProvidedArgumentsProvided_ReturnFormattedString()
    {
        //Arrange
        const string? format = "{0} {1}";
        object[] arguments = {"arg1", "arg2"};

        const string? expectedResult = "arg1 arg2";
        
        //Act
        var result = format.Format(arguments);
        
        //Assert
        result.Should().Be(expectedResult);
    }
}