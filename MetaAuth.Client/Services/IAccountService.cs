﻿using MetaAuth.Client.Entities;
using MetaAuth.SharedEntities;
using MetaAuth.SharedEntities.AzureCosmosDb;

namespace MetaAuth.Client.Services;

public interface IAccountService
{
    Task<SignUpData?> GetSignUpData(string requestId);
    Task<BaseResponse> FinishSignUp(SignUpModel signUpModel);
}