using System.IO;
using System.Threading.Tasks;
using Azure.AI.FormRecognizer.DocumentAnalysis;
using CfContractAnalysisMvp.Api.Interfaces;
using CfContractAnalysisMvp.Api.Models.Contract;
using CfContractAnalysisMvp.Api.Services;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using Xunit;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace CfContractAnalysisMvp.UnitTests.Services;

public class CfContractAnalysisMvpTests
{
    private readonly IConfiguration _configuration;

    public CfContractAnalysisMvpTests()
    {
        _configuration = Substitute.For<IConfiguration>();
    }

    private IContractAnalysisService Sut => new ContractAnalysisService(_configuration);

    [Fact]
    public async Task Should_fail_null_values()
    {
        string fileName = "../../../Files/testResult.json";
        string jsonString = await File.ReadAllTextAsync(fileName);
        AnalyzeResult testResult = JsonSerializer.Deserialize<AnalyzeResult>(jsonString)!;
        
        // var formattedResult = new ContractAnalysisResult();
        // foreach (var doc in testResult.Documents)
        // {
        //     foreach (var fieldKeyValuePair in doc.Fields)
        //     {
        //         // CreateFieldResults(fieldKeyValuePair, formattedResult.Results);
        //         ContractFieldResult x = Sut.CreateFieldResultsTest(fieldKeyValuePair.Key, fieldKeyValuePair.Value);
        //     }
        // }
        
        // '/Users/kaden/dev/cf/cf-contract-analysis-mvp/backend/tests/CfContractAnalysisMvp.UnitTests/bin/Debug/Files/testResult.json'.
        
        // var result = await Sut.AnalyzeDocument(null);
        // result.ShouldBeEquivalentTo(new List<DocumentKeyValuePair>() );
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