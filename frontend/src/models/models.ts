// export interface ContractAnalysisResult {
//   key: string;
//   content: string | null;
//   subResults: ContractAnalysisResult[] | null;
// }

export type ContractAnalysisResult =
  | { key: string; content: string; subResults: null }
  | {
      key: string;
      content: null;
      subResults: { key: string; content: null; subResults: { key: string; content: string; subResults: null }[] }[];
    };
