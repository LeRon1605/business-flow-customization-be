﻿using Application.Dtos.Identity;
using BuildingBlocks.Application.Clients;

namespace Application.Clients.Interfaces;

public interface IIdentityClient : IRestSharpClient
{
    Task<UserInfoDto> GetUserInfoAsync();
}