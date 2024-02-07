using System.IO;
using System.Threading.Tasks;
using Azure.AI.FormRecognizer.DocumentAnalysis;
using CfContractAnalysisMvp.Api.Interfaces;
using CfContractAnalysisMvp.Api.Services;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using Xunit;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace CfContractAnalysisMvp.UnitTests.Services;

public class CfContractAnalysisMvpTests
{
    private readonly IConfiguration _configuration;
    private readonly IKeyTermService _keyTermService;

    public CfContractAnalysisMvpTests()
    {
        _configuration = Substitute.For<IConfiguration>();
        _keyTermService = Substitute.For<IKeyTermService>();
    }

    private IContractAnalysisService Sut => new ContractAnalysisService(_configuration, _keyTermService);

    [Fact]
    public async Task Should_fail_null_values()
    {
        string fileName = "../../../Files/testResult.json";
        string jsonString = await File.ReadAllTextAsync(fileName);
        AnalyzeResult testResult = JsonSerializer.Deserialize<AnalyzeResult>(jsonString)!;
    }

    // [Theory]
    // [InlineData("dood", true)]
    // [InlineData("dude", false)]
    // public void Should_detect_simple_palindromes(string? input, bool expectedResult)
    // {
    //     Sut.IsPalindrome(input).ShouldBe(expectedResult);
    // }
    //
    // [Theory]
    // [InlineData("Eva, Can I Stab Bats In A Cave?", true)]
    // [InlineData("Mr. Owl Ate My Metal Worm", true)]
    // [InlineData("A Santa Lived As a Devil At NASA", true)]
    // [InlineData("This app is AWESOME", false)]
    // [InlineData("THiS shouldn't pass EITHER", false)]
    // [InlineData("MO, this test is for YOU", false)]
    // public void Should_detect_complex_palindromes(string? input, bool expectedResult)
    // {
    //     Sut.IsPalindrome(input).ShouldBe(expectedResult);
    // }
}