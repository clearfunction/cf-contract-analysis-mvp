import React, { useState } from "react";
import axios, { AxiosResponse } from "axios";
import ContractResults from "../components/ContractResults";

const ContractAnalysis = () => {
  const [loading, setLoading] = useState<boolean>(false);
  const [errorMessage, setErrorMessage] = useState<string>("");
  const [file, setFile] = useState<any>();
  const [analysisResponse, setAnalysisResponse] = useState<AxiosResponse<any, any>>();

  const getFile = (event: any) => {
    const selectedFile = event.target.files[0];
    if (selectedFile.type !== "application/pdf") {
      setErrorMessage("File type must be PDF. Please upload a different file.");
      return;
    }

    setErrorMessage("");
    setFile(selectedFile);
  };

  const getAnalysis = (event: any) => {
    if (!file) {
      setErrorMessage("Please upload a PDF to run analysis.");
      return;
    }

    setLoading(true);
    setErrorMessage("");
    const formData = new FormData();
    formData.append("file", file);

    axios
      .post("https://cf-contract-analysis-mvp.azurewebsites.net/api/azure/analyze-contract", formData, {
        headers: {
          "Content-Type": "multipart/form-data",
          "Access-Control-Allow-Origin": "https://cf-contract-analysis-mvp.azurewebsites.net",
        },
      })
      .then((response) => {
        setAnalysisResponse(response);
        setLoading(false);
      })
      .catch((error) => {
        setErrorMessage(`${error.message}. Please try again later.`);
        setLoading(false);
      });
  };

  if (loading) {
    return (
      <div className="bg-gray-dark min-h-screen flex flex-col items-center text-2xl text-white">
        <h1 className="text-white my-auto">Loading</h1>
      </div>
    );
  }

  return (
    <div className="bg-gray-dark min-h-screen flex flex-col items-center text-2xl text-white">
      <a href="/" className="absolute top-6 left-6 text-sm font-bold underline">
        Return Home
      </a>
      <h1 className="text-3xl font-bold my-12 text-white">Contract Analysis</h1>
      <div className="flex flex-row items-center justify-center">
        <div className="mx-auto max-w-xs">
          <input
            onChange={getFile}
            type="file"
            className="block w-full bg-slate-300 rounded-md text-black text-sm file:mr-4 file:rounded-md file:border-0 file:bg-sky-500 file:py-2.5 file:px-4 file:text-sm file:font-semibold file:text-black cursor-pointer hover:file:bg-sky-700 focus:outline-none disabled:pointer-events-none disabled:opacity-60"
          />
        </div>
      </div>
      <button
        onClick={getAnalysis}
        className="bg-sky-500 rounded p-2 text-sm font-bold text-black my-8 hover:bg-sky-700"
      >
        Analyze Contract
      </button>
      {errorMessage !== "" && <p className="text-base text-red-400">{errorMessage}</p>}
      {analysisResponse && <ContractResults analysisResults={analysisResponse.data.results} />}
    </div>
  );
};

export default ContractAnalysis;
