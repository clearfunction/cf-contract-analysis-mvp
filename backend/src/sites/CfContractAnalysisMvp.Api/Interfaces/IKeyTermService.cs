using CfContractAnalysisMvp.Api.Models.Document;

namespace CfContractAnalysisMvp.Api.Interfaces;

public interface IKeyTermService
{
    public void FindKeyTerms(DocumentAnalysisResult analysisResult);
}