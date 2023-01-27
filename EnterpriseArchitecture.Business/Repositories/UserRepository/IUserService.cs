﻿using EnterpriseArchitecture.Core.Utilities.Result.Abstract;
using EnterpriseArchitecture.DataTransformationObjects.Concrete.Auth;
using EnterpriseArchitecture.DataTransformationObjects.Concrete.User;

namespace EnterpriseArchitecture.Business.Repositories.UserRepository;

public interface IUserService
{
    void Add(RegisterDto registerDto);
    IDataResult<UserWithAllFields?> GetByEmail(string email); 
    List<UserWithIdDto> GetAllUsersWithId();
    List<UserDto> GetAllUsers();
    UserDto FindById(Guid id);
    void RemoveById(Guid id);
    void Update(UpdateUserDto updateUserDto);
}