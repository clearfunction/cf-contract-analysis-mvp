using System.Text.RegularExpressions;
using CfContractAnalysisMvp.Api.Interfaces;
using CfContractAnalysisMvp.Api.Models.Document;

namespace CfContractAnalysisMvp.Api.Services;

public class KeyTermService : IKeyTermService
{
    public void FindKeyTerms(DocumentAnalysisResult analysisResult)
    {
        var termsToSearchFor = new Dictionary<string, int>()
        {
            { "buyer", 0 },
            { "seller", 1 },
            { "address", 2 },
            { "amount", 3 },
            { "date", 4 },
        };

        foreach (var kvp in analysisResult.KeyValuePairsList)
        {
            foreach (var keyTerm in termsToSearchFor.Keys)
            {
                if (kvp.Value is ":selected:" or ":unselected:" or "No Value") continue;
                
                var cleanedKey = Regex.Replace(kvp.Key, "[^A-Za-z]", "");
                if (cleanedKey.Length < keyTerm.Length) continue;
                
                var score = ComputeLevenshteinDistance(keyTerm, cleanedKey.ToLower());
                if (score <= 3)
                {
                    SaveKeyValuePair(analysisResult, kvp, termsToSearchFor[keyTerm]);
                }
            }
        }
    }

    private int ComputeLevenshteinDistance(string s, string t)
    {
        var n = s.Length;
        var m = t.Length;
        var d = new int[n + 1, m + 1];

        if (n == 0)
        {
            return m;
        }

        if (m == 0)
        {
            return n;
        }

        for (var i = 0; i <= n; d[i, 0] = i++)
        {
        }

        for (var j = 0; j <= m; d[0, j] = j++)
        {
        }

        for (var i = 1; i <= n; i++)
        {
            for (var j = 1; j <= m; j++)
            {
                var cost = (t[j - 1] == s[i - 1]) ? 0 : 1;
                d[i, j] = Math.Min(
                    Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                    d[i - 1, j - 1] + cost);
            }
        }
        
        return d[n, m];
    }

    private void SaveKeyValuePair(DocumentAnalysisResult analysisResult, DocumentKeyValuePair kvp, int index)
    {
        switch (index)
        {
            case 0:
                analysisResult.BuyerName.Add(kvp);
                break;
            case 1:
                analysisResult.SellerName.Add(kvp);
                break;
            case 2:
                analysisResult.PropertyAddress.Add(kvp);
                break;
            case 3:
                if (kvp.Value.Contains('$'))
                {
                    analysisResult.ContractAmount.Add(kvp);
                }
                break;
            case 4:
                analysisResult.ContractDate.Add(kvp);
                break;
        }
    }
}