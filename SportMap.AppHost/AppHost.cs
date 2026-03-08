var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var server = builder.AddProject<Projects.SportMap_Server>("server")
                    .WithReference(cache)
                    .WaitFor(cache)
                    .WithHttpHealthCheck("/health")
                    .WithExternalHttpEndpoints();

var pnpmApp = builder.AddPnpmApp("webfrontend")
                     .WithReference(server)
                     .WaitFor(server)
                     .WithExternalHttpEndpoints();
    
//server.PublishWithContainerFiles(webfrontend, "wwwroot");

builder.Build().Run();
