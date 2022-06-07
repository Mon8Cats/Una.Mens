using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;
using SkIdentityServer;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();

//
builder.Services.AddIdentityServer(options => 
{
    options.Events.RaiseErrorEvents = true;
    options.Events.RaiseFailureEvents = true;
    options.Events.RaiseInformationEvents = true;
    options.Events.RaiseSuccessEvents = true;

    options.EmitStaticAudienceClaim = true;
})
    .AddTestUsers(TestUsers.Users)
    .AddInMemoryClients(Config.Clients)
    .AddInMemoryApiResources(Config.ApiResources)
    .AddInMemoryApiScopes(Config.ApiScopes)
    .AddInMemoryIdentityResources(Config.IdentityResources);


var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseIdentityServer();
app.UseAuthorization();

//app.MapGet("/", () => "Hello World!");
app.MapRazorPages().RequireAuthorization();

app.Run();


//https://localhost:5443/.well-known/openid-configuration