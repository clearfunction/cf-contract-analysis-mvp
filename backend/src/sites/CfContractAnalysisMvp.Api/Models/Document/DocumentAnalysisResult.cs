namespace CfContractAnalysisMvp.Api.Models.Document;

public class DocumentAnalysisResult
{
    public DocumentAnalysisResult()
    {
        BuyerName = new List<DocumentKeyValuePair>();
        SellerName = new List<DocumentKeyValuePair>();
        PropertyAddress = new List<DocumentKeyValuePair>();
        ContractAmount = new List<DocumentKeyValuePair>();
        ContractDate = new List<DocumentKeyValuePair>();
        KeyValuePairsList = new List<DocumentKeyValuePair>();
    }

    public List<DocumentKeyValuePair> BuyerName { get; set; }

    public List<DocumentKeyValuePair> SellerName { get; set; }

    public List<DocumentKeyValuePair> PropertyAddress { get; set; }

    public List<DocumentKeyValuePair> ContractAmount { get; set; }

    public List<DocumentKeyValuePair> ContractDate { get; set; }
    
    public List<DocumentKeyValuePair> KeyValuePairsList { get; set; }
}