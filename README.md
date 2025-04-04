🧠 Tars - AI Chatbot using Azure OpenAI (ASP.NET Core MVC)
Tars is an intelligent chatbot built with ASP.NET Core MVC that integrates Azure OpenAI to provide smart responses to user queries.

🔍 Features
Company-Aware Responses: If a query relates to company policies, rules, or HR topics, Tars extracts information from a stored PDF document and gives relevant answers.

General Q&A: For other queries, Tars leverages Azure OpenAI to generate natural language responses.

Typing Animation: Includes a dynamic typing animation to enhance the user experience.

PDF Knowledge Base: Uses PDF parsing (via PdfPig) to fetch and understand company data.

🧰 Tech Stack
ASP.NET Core MVC

Azure OpenAI Service

PdfPig (for PDF text extraction)

JavaScript (for chat interaction and UX)

Bootstrap (for responsive design)

🚀 How It Works
User enters a message via the web UI.

The message is posted to a Web API endpoint.

If the message is company-related, it fetches data from a PDF file stored on the server.

Otherwise, it generates a response using Azure OpenAI's Chat Completion API.

The response is shown back in the chat window.


