var builder = DistributedApplication.CreateBuilder(args);

builder.AddDockerComposeEnvironment("compose");

var cache = builder.AddRedis("cache")
    .PublishAsDockerComposeService((resource, service) =>
    {
        service.Name = "cache";
    });

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
    .WithExternalHttpEndpoints()
    .PublishAsDockerComposeService((resource, service) =>
    {
        service.Name = "server";
    });

#AddContainer(resourceName, imageName)
builder.AddContainer("webfrontend", "webfrontend")
    .WithDockerfile("../frontend")
    .WithHttpEndpoint(port: 3000, targetPort: 3000, env: "PORT")
    .WithReference(server)
    .WaitFor(server)
    .WithExternalHttpEndpoints()
    .PublishAsDockerComposeService((resource, service) =>
    {
        service.Name = "webfrontend";
    });

builder.Build().Run();