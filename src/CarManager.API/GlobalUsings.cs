﻿// Global using directives

global using System.ComponentModel.DataAnnotations;
global using CarManager.API.Requests.Authentication;
global using CarManager.Application;
global using CarManager.Application.User.Commands.CreateUser;
global using CarManager.Domain.Entities;
global using CarManager.Infrastructure;
global using CarManager.Shared.Abstractions.Common;
global using CarManager.Shared.Abstractions.Security;
global using CarManager.Shared.Infrastructure.Api;
global using CarManager.Shared.Infrastructure.Swagger;
global using Hellang.Middleware.ProblemDetails;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.OpenApi.Models;