import os
from dotenv import load_dotenv
from azure.core.credentials import AzureKeyCredential
from azure.ai.formrecognizer import DocumentAnalysisClient
from openai import OpenAI

from prompt import system_prompt

load_dotenv()
azure_key = os.getenv("AZURE_KEY")
azure_service_endpoint = os.getenv("AZURE_SERVICE_ENDPOINT")
open_ai_org_id = os.getenv("OPEN_AI_ORG_ID")

document_analysis_client = DocumentAnalysisClient(
    endpoint=azure_service_endpoint, credential=AzureKeyCredential(azure_key)
)

client = OpenAI()


def analyze_contract(path_to_file):
    print("Uploading to Azure Document AI...")
    print("________________________________________")

    with open(path_to_file, "rb") as f:
        poller = document_analysis_client.begin_analyze_document(
            "prebuilt-read", document=f
        )

    result = poller.result()

    print("File read by Azure Document AI")
    print("Uploading file content to Open AI API...")
    print("________________________________________")

    messages = system_prompt

    messages.append({"role": "user", "content": "Upload Starting Now."})
    for paragraph in result.paragraphs:
        messages.append({"role": "user", "content": paragraph.content})

    messages.append({"role": "user", "content": "Please Start Summary Results."})

    response = client.chat.completions.create(model="gpt-3.5-turbo", messages=messages)

    print("Contract Summary Results:")
    print("________________________________________")

    print(response.choices[0].message.content)


if __name__ == "__main__":
    analyze_contract(
        "/Users/kaden/dev/cf/sample-contract-files/AmendmentToContract.pdf"
    )
    # analyze_contract(
    #     "/Users/kaden/dev/cf/sample-contract-files/FarmAndRanchContract.pdf"
    # )
    # analyze_contract(
    #     "/Users/kaden/dev/cf/sample-contract-files/Sample_Utah_Real_Estate_Contract.pdf"
    # )
    # analyze_contract("/Users/kaden/dev/cf/sample-contract-files/SimpleContract.pdf")
