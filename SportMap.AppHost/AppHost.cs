var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var postgres = builder.AddPostgres("postgres")
                      .WithDataVolume(isReadOnly: false);

var postgresdb = postgres.AddDatabase("sportmapdb");

var server = builder.AddProject<Projects.SportMap_Server>("server")
                    .WithReference(cache)
                    .WaitFor(cache)
                    .WithReference(postgres)
                    .WaitFor(postgres)
                    .WithHttpHealthCheck("/health")
                    .WithExternalHttpEndpoints();

var pnpmApp = builder.AddJavaScriptApp("webfrontend", "../frontend")
                     .WithHttpEndpoint(port: 3000, env: "PORT")
                     .WithReference(server)
                     .WaitFor(server)
                     .WithExternalHttpEndpoints()
                     .WithPnpm();

builder.Build().Run();