var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var pgUsername = builder.AddParameter("postgres-username", secret: true);
var pgPassword = builder.AddParameter("postgres-password", secret: true);

var pgDb = builder.AddPostgres("postgres", pgUsername, pgPassword)
    .WithDataVolume(isReadOnly: false)
    .WithPgAdmin(pgAdmin => pgAdmin.WithHostPort(5050))
    .AddDatabase("sportmapdb");

var server = builder.AddProject<Projects.SportMap_PL>("server")
    .WithReference(cache)
    .WaitFor(cache)
    .WithReference(pgDb)
    .WaitFor(pgDb)
    .WithHttpHealthCheck("/health")
    .WithExternalHttpEndpoints();

var pnpmApp = builder.AddJavaScriptApp("webfrontend", "../frontend")
    .WithHttpEndpoint(port: 3000, env: "PORT")
    .WithReference(server)
    .WaitFor(server)
    .WithExternalHttpEndpoints()
    .WithPnpm();

builder.Build().Run();