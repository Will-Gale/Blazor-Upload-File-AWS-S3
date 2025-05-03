using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Blazor_Upload_File_AWS_S3.Components;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using Sharedlayer.Services.Files;
using System.Runtime;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Options instance for S3 Settings
builder.Services.Configure<S3Settings>(builder.Configuration.GetSection("S3Settings"));
builder.Services.AddSingleton<IAmazonS3>(sp =>
{
    var s3settings = sp.GetRequiredService<IOptions<S3Settings>>().Value;
    var credentials = new BasicAWSCredentials(s3settings.AccessKey, s3settings.SecretKey);
    var config = new AmazonS3Config
    {
        RegionEndpoint = RegionEndpoint.GetBySystemName(s3settings.Region),
        AuthenticationRegion = s3settings.Region,
        SignatureVersion = "4",
        ForcePathStyle = true,
        UseHttp = true,
        MaxErrorRetry = 3
    };
    return new AmazonS3Client(credentials, config);
});

builder.Services.Configure<HubOptions>(options =>
{
    options.MaximumReceiveMessageSize = 50 * 1024 * 1024; // 50MB
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
