var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var server = builder.AddProject<Projects.SportMap_Server>("server")
                    .WithReference(cache)
                    .WaitFor(cache)
                    .WithHttpHealthCheck("/health")
                    .WithExternalHttpEndpoints();

var pnpmApp = builder.AddJavaScriptApp("webfrontend", "../frontend")
                     .WithHttpEndpoint(port: 3000, env: "PORT")
                     .WithReference(server)
                     .WaitFor(server)
                     .WithExternalHttpEndpoints()
                     .WithPnpm();
//server.PublishWithContainerFiles(webfrontend, "wwwroot");

builder.Build().Run();