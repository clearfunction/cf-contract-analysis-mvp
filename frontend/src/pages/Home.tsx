import React from "react";

function Home() {
  return (
    <div className="m-auto antialiased text-center">
      <header className="bg-slate-200 min-h-screen flex flex-col items-center justify-center text-white">
        <h1 className="text-4xl font-black text-black">CF Sample Contract Analysis MVP</h1>
        <a
          className="rounded-md bg-sky-500/100 hover:bg-sky-500/50 p-5 my-10 text-xl w-3/4 md:w-1/3 max-w-xl"
          href="/analyze-contract"
          rel="noopener noreferrer"
        >
          Prebuilt Contract Model
        </a>
        <a
          className="rounded-md bg-sky-500/100 hover:bg-sky-500/50 p-5 mb-10 text-xl w-3/4 md:w-1/3 max-w-xl"
          href="/analyze-document"
          rel="noopener noreferrer"
        >
          Prebuilt Document Model
        </a>
        <a
          className="rounded-md bg-sky-500/100 hover:bg-sky-500/50 p-5 text-xl w-3/4 md:w-1/3 max-w-xl"
          href="/ask-gpt"
          rel="noopener noreferrer"
        >
          OpenAI Model
        </a>
      </header>
    </div>
  );
}

export default Home;
