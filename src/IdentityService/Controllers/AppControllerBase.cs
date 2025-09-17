﻿using IdentityService.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using IdentityService.Models;
using Microsoft.Extensions.Options;
using IdentityService.DbContext;

namespace IdentityService.Controllers;

public class AppControllerBase : ControllerBase
{
    protected readonly IConfiguration _configuration = default!;
    protected readonly IOptions<AppSettings> _appSettings = default!;
    protected readonly SmsService _smsService = default!;
    protected readonly ApplicationDbContext _context  = default!;


    public AppControllerBase(
        IConfiguration configuration,
        IOptions<AppSettings> appSettings,
        SmsService smsService,
        ApplicationDbContext context)
    {
        _configuration = configuration;
        _appSettings = appSettings;
        _smsService = smsService;
        _context = context;
    }
}
