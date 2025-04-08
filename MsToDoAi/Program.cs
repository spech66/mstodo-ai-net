using Azure.Identity;
using Microsoft.Graph;
using Microsoft.Graph.Models.ODataErrors;

var scopes = new[] { "https://graph.microsoft.com/.default" };

var clientId = "YOUR_CLIENT_ID";
var tenantId = "YOUR_TENANT_ID";
var clientSecret = "YOUR_CLIENT_SECRET";
var userPrincipleName = "YOUR_UPN";

var options = new ClientSecretCredentialOptions
{
    AuthorityHost = AzureAuthorityHosts.AzurePublicCloud,
};

// https://learn.microsoft.com/dotnet/api/azure.identity.clientsecretcredential
var clientSecretCredential = new ClientSecretCredential(tenantId, clientId, clientSecret, options);

var graphClient = new GraphServiceClient(clientSecretCredential, scopes);

try
{
    var user = await graphClient.Users[userPrincipleName].GetAsync();
    Console.WriteLine(user?.DisplayName);

    var tasks = await graphClient.Users[userPrincipleName].Todo.Lists.GetAsync();
    Console.WriteLine(tasks?.Value);
}
catch (ODataError ex)
{
    Console.WriteLine($"ODataError: {ex} {ex.Error?.Code}");
    return;
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex}");
    return;
}

