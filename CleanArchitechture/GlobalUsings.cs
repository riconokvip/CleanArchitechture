/* -- Domain -- */
global using CleanArchitechture.Domain.Entities;


/* -- Application -- */
global using CleanArchitechture.Application.DbContexts;


/* -- Cores -- */
global using Cores.BaseModels;
global using Cores.Enums;
global using Cores.Configs;
global using Cores.Exceptions;


// Base
global using Microsoft.EntityFrameworkCore;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Mvc;
global using Newtonsoft.Json;
// Authentication
global using CleanArchitechture.JwtAuthentications;
// Middleware
global using CleanArchitechture.Middlewares;