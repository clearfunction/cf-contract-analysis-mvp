import React from "react";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import ContractAnalysis from "./pages/ContractAnalysis";
import DocumentAnalysis from "./pages/DocumentAnalysis";
import Home from "./pages/Home";
import OpenAiAnalysis from "./pages/OpenAiAnalysis";

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/analyze-contract" element={<ContractAnalysis />} />
        <Route path="/analyze-document" element={<DocumentAnalysis />} />
        <Route path="/ask-gpt" element={<OpenAiAnalysis />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
