﻿using Application.Dtos;
using Application.Dtos.Submissions.Identity;
using BuildingBlocks.Application.Clients;

namespace Application.Clients.Interfaces;

public interface IIdentityClient : IRestSharpClient
{
    Task<UserInfoDto> GetUserInfoAsync();
}