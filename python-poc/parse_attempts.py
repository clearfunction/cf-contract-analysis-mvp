import PyPDF2


def try_parse_pdf(path_to_file):
    pdfFileObj = open(path_to_file, "rb")

    pdfReader = PyPDF2.PdfReader(pdfFileObj)
    print(len(pdfReader.pages))

    pageObj = pdfReader.pages[0]
    print(pageObj.extract_text())

    pdfFileObj.close()


if __name__ == "__main__":
    try_parse_pdf("/Users/kaden/dev/cf/sample-contract-files/AmendmentToContract.pdf")
