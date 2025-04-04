# Tars - AI Chatbot using Azure OpenAI (ASP.NET Core MVC)

Tars is an AI-powered chatbot built with **ASP.NET Core MVC**, designed to provide intelligent responses to users. It connects with **Azure OpenAI** to generate conversational replies and understands company-specific queries using a stored PDF knowledge base.

## 🔍 Features

- ✨ **Smart Chat UI**: Clean and interactive chat interface using MVC Razor views.
- 💡 **Context-Aware Responses**: Recognizes company-related queries (like leave, HR, rules) and answers from a PDF.
- 🔍 **Azure OpenAI Integration**: Uses OpenAI's Chat Completion API for natural responses.
- 🔹 **Typing Indicator**: Includes a bouncing dot animation while bot is generating the response.
- ✍️ **PDF Parsing**: Parses a `text.pdf` document in `wwwroot` using PdfPig.

## 🚀 Tech Stack

- **ASP.NET Core MVC**
- **Azure OpenAI Service**
- **PdfPig** for PDF processing
- **JavaScript** for interactivity
- **Bootstrap/CSS** for styling

## 📆 How It Works

1. User types a message in the frontend.
2. Request is sent to `TallyController`'s `/api/Tally/GetChatbotResponse` endpoint.
3. If message is company-related, PDF is scanned and processed.
4. If not, a generic Azure OpenAI response is fetched.
5. Final response is rendered in the chat view with typing animation.

## 📁 Folder Structure

```
Chatbott/
├── Controllers/
│   └── TallyController.cs
├── Service/
│   └── AiAnalysisService.cs
├── Models/
│   └── QueryModel.cs
├── Views/
│   └── Chatbot UI
├── wwwroot/
│   └── text.pdf
├── Program.cs
└── README.md
```

## ⚙️ Setup

1. Add your Azure OpenAI credentials in `appsettings.json`:

```json
"AzureOpenAI": {
  "Endpoint": "<your-endpoint>",
  "ApiKey": "<your-api-key>",
  "DeploymentName": "<your-deployment-name>"
}
```

2. Place your `text.pdf` (containing company rules) in the `wwwroot` folder.
3. Run the project using Visual Studio or `dotnet run`.
4. Open Swagger or your MVC chatbot UI to interact.

## 🌟 Credits
- Developed by Vishwajeet
- Powered by Azure OpenAI & .NET Core MVC

---

Feel free to fork and contribute. Star the repo if you found it useful! ✨
