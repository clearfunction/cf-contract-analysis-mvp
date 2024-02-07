import React, { useState } from "react";

interface props {
  analysisResults: any;
}

const DocumentResults: React.FC<props> = ({ analysisResults }) => {
  const [dropDownExpanded, setDropDownExpanded] = useState<boolean>(false);

  return (
    <React.Fragment>
      <div className="container m-10">
        <p className="mb-5 text-center text-xl">Found Key Information:</p>
        <div className="relative p-10 rounded-xl overflow-auto bg-slate-800 text-center">
          <h2 className="text-2xl font-bold underline">Buyer Name</h2>
          {analysisResults.buyerName.length === 0 ? (
            <p className="text-xs mt-2 mb-5">No key-value results for Buyer Name were found.</p>
          ) : (
            <>
              <p className="text-xs mt-2 mb-5">Found the following key-value results for Buyer Name:</p>
              {analysisResults.buyerName?.map((result: any, index: number) => {
                return (
                  <p key={index} className="text-base">
                    <b>Key:</b> {result.key} <b>Value:</b> {result.value}
                  </p>
                );
              })}
            </>
          )}

          <h2 className="mt-10 text-2xl font-bold underline">Seller Name</h2>
          {analysisResults.sellerName.length === 0 ? (
            <p className="text-xs mt-2 mb-5">No key-value results for Seller Name were found.</p>
          ) : (
            <>
              <p className="text-xs mt-2 mb-5">Found the following key-value results for Seller Name:</p>
              {analysisResults.sellerName?.map((result: any, index: number) => {
                return (
                  <p key={index} className="text-base">
                    <b>Key:</b> {result.key} <b>Value:</b> {result.value}
                  </p>
                );
              })}
            </>
          )}

          <h2 className="mt-10 text-2xl font-bold underline">Property Address</h2>
          {analysisResults.propertyAddress.length === 0 ? (
            <p className="text-xs mt-2 mb-5">No key-value results for Property Address were found.</p>
          ) : (
            <>
              <p className="text-xs mt-2 mb-5">Found the following key-value results for Property Address:</p>
              {analysisResults.propertyAddress?.map((result: any, index: number) => {
                return (
                  <p key={index} className="text-base">
                    <b>Key:</b> {result.key} <b>Value:</b> {result.value}
                  </p>
                );
              })}
            </>
          )}

          <h2 className="mt-10 text-2xl font-bold underline">Contract Amount</h2>
          {analysisResults.contractAmount.length === 0 ? (
            <p className="text-xs mt-2 mb-5">No key-value results for Contract Amount were found.</p>
          ) : (
            <>
              <p className="text-xs mt-2 mb-5">Found the following key-value results for Contract Amount:</p>
              {analysisResults.contractAmount?.map((result: any, index: number) => {
                return (
                  <p key={index} className="text-base">
                    <b>Key:</b> {result.key} <b>Value:</b> {result.value}
                  </p>
                );
              })}
            </>
          )}

          <h2 className="mt-10 text-2xl font-bold underline">Contract Date</h2>
          {analysisResults.contractDate.length === 0 ? (
            <p className="text-xs mt-2 mb-5">No key-value results for Contract Date were found.</p>
          ) : (
            <>
              <p className="text-xs mt-2 mb-5">Found the following key-value results for Contract Date:</p>
              {analysisResults.contractDate?.map((result: any, index: number) => {
                return (
                  <p key={index} className="text-base">
                    <b>Key:</b> {result.key} <b>Value:</b> {result.value}
                  </p>
                );
              })}
            </>
          )}

          <button
            onClick={() => setDropDownExpanded(!dropDownExpanded)}
            className="bg-slate-300 rounded p-2 text-sm font-bold text-black my-8 hover:bg-slate-500"
          >
            {`${dropDownExpanded ? "Hide" : "Show"} All Found Key Value Pairs`}
          </button>

          {dropDownExpanded && (
            <table className="border-collapse table-fixed w-full text-md">
              <thead>
                <tr>
                  <th className="font-medium p-5 text-white text-center text-sm">Key</th>
                  <th className="font-medium p-5 text-white text-center text-sm">Value</th>
                </tr>
              </thead>
              <tbody className="bg-white dark:bg-slate-800">
                {analysisResults.keyValuePairsList?.map((result: any, index: number) => {
                  return (
                    <tr key={index} className="border border-slate-300">
                      <td className="border p-2 text-sm">{result.key}</td>
                      <td className="border p-2 text-sm">{result.value}</td>
                    </tr>
                  );
                })}
              </tbody>
            </table>
          )}
        </div>
      </div>
    </React.Fragment>
  );
};

export default DocumentResults;
