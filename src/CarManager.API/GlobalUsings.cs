// Global using directives

global using System.Collections.Immutable;
global using System.ComponentModel.DataAnnotations;
global using CarManager.API.Requests.Authentication;
global using CarManager.API.Requests.Cars;
global using CarManager.Application;
global using CarManager.Application.Car.Commands.RegisterCar;
global using CarManager.Application.Car.Commands.UploadImageCar;
global using CarManager.Application.User.Commands.RefreshToken;
global using CarManager.Application.User.Commands.SignIn;
global using CarManager.Application.User.Commands.SignOut;
global using CarManager.Application.User.Commands.SignUp;
global using CarManager.Application.User.Queries.UserInformation;
global using CarManager.Domain.Core;
global using CarManager.Domain.Entities;
global using CarManager.Domain.Types;
global using CarManager.Infrastructure;
global using CarManager.Shared.Abstractions.Common;
global using Hellang.Middleware.ProblemDetails;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.OpenApi.Models;
global using Swashbuckle.AspNetCore.Annotations;