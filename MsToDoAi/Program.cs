using Azure.Identity;
using Microsoft.Graph;

var scopes = new[] { "https://graph.microsoft.com/.default" };

var clientId = "YOUR_CLIENT_ID";
var tenantId = "YOUR_TENANT_ID";
var clientSecret = "YOUR_CLIENT_SECRET";

var options = new ClientSecretCredentialOptions
{
    AuthorityHost = AzureAuthorityHosts.AzurePublicCloud,
};

// https://learn.microsoft.com/dotnet/api/azure.identity.clientsecretcredential
var clientSecretCredential = new ClientSecretCredential(tenantId, clientId, clientSecret, options);

var graphClient = new GraphServiceClient(clientSecretCredential, scopes);

var drive = await graphClient.Me.Drive.GetAsync();

Console.WriteLine(drive.Id);
