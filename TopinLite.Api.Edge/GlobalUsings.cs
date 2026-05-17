global using MediatR;

global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Mvc;
global using TopinLite.Domain.TopinApi;

global using TopinLite.Services.Commands;

global using System.Text.Json.Nodes;
global using TopinLite.Api.Edge.Security;
global using TopinLite.Api.Edge.ServiceExtentions;
global using NatsRpcFoundation.Abstractions;
global using NatsRpcFoundation.Contracts;
global using NatsRpcFoundation.Extensions;
global using System.Security.Claims;

global using TopinLite.Domain.Messaging;
