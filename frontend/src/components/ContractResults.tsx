import React from "react";
import { ContractAnalysisResult } from "../models/models";

interface props {
  analysisResults: ContractAnalysisResult[];
}

const ContractResults: React.FC<props> = ({ analysisResults }) => {
  return (
    <React.Fragment>
      <div className="container m-10">
        <p className="mb-5 text-center text-xl">Found Key Information:</p>
        <div className="relative p-10 rounded-xl overflow-auto bg-slate-800 text-center">
          {analysisResults?.map((result) => {
            return (
              <div key={result.key}>
                <h2 className="p-5 text-2xl font-bold underline">{result.key}</h2>
                {result.content && <p className="text-base max-w-xl mx-auto">{result.content}</p>}
                {result.subResults && (
                  <>
                    {result.subResults.map((r) => {
                      return (
                        <div key={Math.random()}>
                          {r.key && r.content && (
                            <div>
                              <h2 className="p-5 text-xl font-bold underline">{r.key}</h2>
                              <p className="text-base max-w-xl mx-auto">{r.content}</p>
                            </div>
                          )}

                          {r.subResults && (
                            <>
                              {r.subResults.map((s) => {
                                return (
                                  <div key={Math.random()}>
                                    <h2 className="p-5 text-lg font-bold underline">{s.key}</h2>
                                    <p className="text-base max-w-xl mx-auto">{s.content}</p>
                                  </div>
                                );
                              })}
                            </>
                          )}
                        </div>
                      );
                    })}
                  </>
                )}
              </div>
            );
          })}
        </div>
      </div>
    </React.Fragment>
  );
};

export default ContractResults;
