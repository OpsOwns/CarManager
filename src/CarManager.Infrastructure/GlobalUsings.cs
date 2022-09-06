// Global using directives

global using System.IdentityModel.Tokens.Jwt;
global using System.Security.Claims;
global using System.Security.Cryptography;
global using System.Text;
global using CarManager.Domain.Entities;
global using CarManager.Domain.Repositories;
global using CarManager.Domain.ValueObjects;
global using CarManager.Infrastructure.Repositories;
global using CarManager.Infrastructure.Security;
global using CarManager.Shared.Abstractions.Common;
global using CarManager.Shared.Abstractions.Primitives;
global using CarManager.Shared.Abstractions.Security;
global using CarManager.Shared.Abstractions.Utility;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Http;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.DependencyInjection.Extensions;
global using Microsoft.Extensions.Options;
global using Microsoft.IdentityModel.Tokens;
global using JsonWebToken = CarManager.Shared.Abstractions.Security.JsonWebToken;
global using JwtRegisteredClaimNames_ = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;