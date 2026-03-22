var builder = DistributedApplication.CreateBuilder(args);

builder.AddDockerComposeEnvironment("compose");

var cache = builder.AddRedis("cache")
    .PublishAsDockerComposeService((resource, service) =>
    {
        service.Name = "cache";
    });

var pgUsername = builder.AddParameter("postgres-username", secret: true);
var pgPassword = builder.AddParameter("postgres-password", secret: true);

var jwtSecret = builder.AddParameter("jwt-secret", secret: true);
var jwtIssuer = builder.AddParameter("jwt-issuer");
var jwtAudience = builder.AddParameter("jwt-audience");
var googleClientId = builder.AddParameter("google-client-id", secret: true);
var googleClientSecret = builder.AddParameter("google-client-secret", secret: true);
var googleRedirectUri = builder.AddParameter("google-redirect-uri");

var cache = builder.AddRedis("redis");
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
    .WithEnvironment("Jwt__SecretKey", jwtSecret)
    .WithEnvironment("Jwt__Issuer", jwtIssuer)
    .WithEnvironment("Jwt__Audience", jwtAudience)
    .WithEnvironment("Google__ClientId", googleClientId)
    .WithEnvironment("Google__ClientSecret", googleClientSecret)
    .WithEnvironment("Google__RedirectUri", googleRedirectUri)
    .PublishAsDockerComposeService((resource, service) =>
    {
        service.Name = "server";
    });

// #AddContainer(resourceName, imageName)
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