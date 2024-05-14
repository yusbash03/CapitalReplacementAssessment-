using CapitalReplacementTask.Repo.Candidate;
using CapitalReplacementTask.Repo.Question;
using CapitalReplacementTask.Repo.QuestionType;
using Microsoft.Azure.Cosmos;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;


// Add services to the container.
    builder.Services.AddSingleton((provider) =>
    {
        var uri = config["CosmosDBSettings:Uri"];
        var primaryKey = config["CosmosDBSettings:PrimaryKey"];
        var dbName = config["CosmosDBSettings:DBName"];

        var cosmosClientOption = new CosmosClientOptions
        {
            ApplicationName = dbName
        };

        var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
        });

        var cosmosClient = new CosmosClient(uri, primaryKey, cosmosClientOption);

        return cosmosClient;
    });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Registered Services
builder.Services.AddScoped<IQuestionsRepo, QuestionsRepo>();
builder.Services.AddScoped<IQuestionTypesRepo, QuestionTypesRepo>();
builder.Services.AddScoped<ICandidatesRepo, CandidatesRepo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
