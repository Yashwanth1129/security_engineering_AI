# Technical Documentation: AI Chat Application with JWT Authentication

## 📋 Project Description

### **Application Purpose**
This project implements a **Retrieval-Augmented Generation (RAG)** chat application with **JWT-based authentication** that allows users to interact with AI models while securely accessing document knowledge bases. The system processes PDF documents, creates semantic embeddings, and provides context-aware AI responses.

### **Problem Statement**
Traditional AI chatbots lack access to specific document knowledge and don't provide secure user authentication. This project addresses both challenges by implementing:
- **Document Intelligence**: AI that can answer questions based on uploaded PDFs
- **Secure Authentication**: JWT-based user authentication and authorization
- **Real-time Interaction**: Interactive chat interface with AI models

## 🏗️ System Architecture

### **High-Level Architecture**
```
┌─────────────────┐    ┌─────────────────┐    ┌─────────────────┐
│   Frontend      │    │   Backend       │    │   AI Services   │
│   (Blazor)      │◄──►│   (.NET API)    │◄──►│   (Ollama)      │
│                 │    │                 │    │                 │
│ • Login UI       │    │ • JWT Auth      │    │ • Llama 3.2     │
│ • Chat Interface │    │ • API Controllers│    │ • all-minilm    │
│ • Auth State     │    │ • Vector Search │    │ • Embeddings    │
└─────────────────┘    └─────────────────┘    └─────────────────┘
                                │
                                ▼
                       ┌─────────────────┐
                       │   Data Layer    │
                       │                 │
                       │ • SQLite DB     │
                       │ • Vector Store  │
                       │ • PDF Storage   │
                       └─────────────────┘
```

### **Technology Stack**

#### **Frontend:**
- **Blazor Server** - Interactive web UI
- **Tailwind CSS** - Modern styling
- **JavaScript Interop** - Client-side functionality

#### **Backend:**
- **.NET 9.0** - Modern C# framework
- **ASP.NET Core** - Web API framework
- **JWT Authentication** - Token-based security
- **Dependency Injection** - Service management

#### **AI/ML:**
- **Ollama** - Local LLM deployment
- **Llama 3.2** - Chat language model
- **all-minilm** - Embedding model
- **Vector Search** - Semantic retrieval

#### **Data Storage:**
- **SQLite** - Vector database
- **PDF Processing** - Document ingestion
- **Embedding Storage** - Vector representations

## 🔐 Security Implementation

### **Authentication Flow**
1. **User Login**: Credentials validated against user store
2. **JWT Generation**: Secure token created with user claims
3. **Token Storage**: JWT stored in browser localStorage
4. **Request Validation**: Token validated on each API call
5. **Session Management**: Automatic logout on token expiry

### **Security Features**
- **Password Hashing**: SHA256 encryption
- **JWT Tokens**: Stateless authentication
- **Role-Based Access**: Admin/User permissions
- **Token Expiry**: 24-hour session timeout
- **Secure Storage**: Browser localStorage management

### **Code Example - JWT Service**
```csharp
public string GenerateToken(User user)
{
    var claims = new List<Claim>
    {
        new(ClaimTypes.Name, user.Username),
        new(ClaimTypes.Role, user.Role)
    };
    
    var tokenDescriptor = new SecurityTokenDescriptor
    {
        Subject = new ClaimsIdentity(claims),
        Expires = DateTime.UtcNow.AddHours(24),
        SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
    };
    
    return tokenHandler.CreateToken(tokenDescriptor);
}
```

## 🤖 AI/ML Implementation

### **RAG Pipeline**
1. **Document Ingestion**: PDFs processed and chunked
2. **Embedding Generation**: Text converted to vectors
3. **Vector Storage**: Embeddings stored in SQLite
4. **Query Processing**: User questions converted to embeddings
5. **Semantic Search**: Similar chunks retrieved
6. **Context Assembly**: Relevant context provided to LLM
7. **Response Generation**: AI generates contextual answers

### **Vector Search Implementation**
```csharp
public async Task<IReadOnlyList<IngestedChunk>> SearchAsync(string text, string? documentIdFilter, int maxResults)
{
    var nearest = vectorCollection.SearchAsync(text, maxResults, new VectorSearchOptions<IngestedChunk>
    {
        Filter = documentIdFilter is { Length: > 0 } ? record => record.DocumentId == documentIdFilter : null,
    });
    
    return await nearest.Select(result => result.Record).ToListAsync();
}
```

## 📊 Database Schema

### **Vector Collections**
- **IngestedChunk**: Document text chunks with embeddings
- **IngestedDocument**: Document metadata and versions

### **User Management**
- **User**: Username, password hash, role
- **Authentication**: JWT token validation

## 🔧 API Endpoints

### **Authentication Endpoints**
- `POST /api/auth/login` - User authentication
- `POST /api/auth/validate` - Token validation

### **Chat Endpoints**
- `GET /` - Main chat interface (protected)
- `POST /api/chat` - AI chat processing (via Ollama)

## 🎯 Key Features

### **1. Document Processing**
- **PDF Parsing**: Extracts text from PDF documents
- **Text Chunking**: Breaks documents into searchable segments
- **Embedding Generation**: Creates vector representations
- **Metadata Storage**: Tracks document information

### **2. AI Chat Interface**
- **Real-time Chat**: Interactive conversation
- **Context Awareness**: Uses document knowledge
- **Citation Support**: Shows source documents
- **Suggestion System**: Provides query suggestions

### **3. Security Features**
- **User Authentication**: Login/logout functionality
- **JWT Tokens**: Secure session management
- **Protected Routes**: Authentication-required pages
- **Role Management**: Admin/User permissions

## 📈 Performance Considerations

### **Optimization Strategies**
- **Vector Indexing**: Efficient similarity search
- **Caching**: JWT token validation caching
- **Async Operations**: Non-blocking I/O operations
- **Connection Pooling**: Database connection management

### **Scalability Features**
- **Stateless Authentication**: JWT-based sessions
- **Vector Database**: Efficient embedding storage
- **Component Architecture**: Reusable UI components
- **Service Injection**: Modular service design

## 🧪 Testing Strategy

### **Unit Testing**
- **Service Testing**: Authentication service validation
- **JWT Testing**: Token generation and validation
- **Vector Search**: Embedding retrieval accuracy

### **Integration Testing**
- **API Testing**: Endpoint functionality
- **Authentication Flow**: Login/logout processes
- **AI Integration**: Chat response accuracy

## 📝 Deployment Considerations

### **Production Requirements**
- **HTTPS**: Secure communication
- **Database**: Production SQLite or PostgreSQL
- **Ollama Server**: Dedicated AI model server
- **Environment Variables**: Secure configuration

### **Security Hardening**
- **JWT Secret**: Strong secret key generation
- **Password Policies**: Enhanced security rules
- **Rate Limiting**: API request throttling
- **Audit Logging**: User activity tracking

## 🎓 Learning Outcomes

This project demonstrates proficiency in:

1. **Full-Stack Development**: Frontend and backend integration
2. **AI/ML Implementation**: Practical machine learning application
3. **Security Engineering**: Authentication and authorization
4. **Modern Web Technologies**: Latest .NET and Blazor features
5. **System Architecture**: Scalable and maintainable design
6. **Database Design**: Vector storage and retrieval
7. **API Development**: RESTful service design

## 📊 Project Statistics

- **Total Lines of Code**: 500+ lines
- **Components**: 10+ Blazor components
- **Services**: 6+ service classes
- **API Endpoints**: 3+ RESTful endpoints
- **Security Features**: JWT, hashing, validation
- **AI Models**: 2+ integrated models
- **Database Tables**: 2+ vector collections

---

**This technical documentation provides comprehensive coverage of the AI chat application's architecture, implementation, and security features suitable for academic assignments and professional portfolios.**






