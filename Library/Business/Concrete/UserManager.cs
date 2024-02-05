using Business.Abstract;
using Business.Helpers;
using Business.ModelMapping.AutoMapper;
using Core.Dto;
using Core.Entities;
using Core.Results;
using Core.Utilities;
using Entities.Dto;
using Microsoft.EntityFrameworkCore;
using Repository.Abstract;
using System.Net;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<UserDto>> CreateUserAsync(UserDto userDto)
        {
                userDto.Password = HashingHelper.HashPassword(userDto.Password);

                User userEntity = ObjectMapper.Mapper.Map<User>(userDto);

                await _unitOfWork.User.InsertAsync(userEntity);
                await _unitOfWork.SaveChangesAsync();

                return Response<UserDto>.Success(ObjectMapper.Mapper.Map<UserDto>(userEntity), (int)HttpStatusCode.Created);
        }

        public async Task<Response<NoDataDto>> DeleteUserAsync(int id)
        {
            var dbUser = await _unitOfWork.User.GetByIdAsync(id);

            if (dbUser is null)
                return Response<NoDataDto>.Fail("User is not found", (int)HttpStatusCode.NotFound, true);

            _unitOfWork.User.Delete(dbUser);
            await _unitOfWork.SaveChangesAsync();

            return Response<NoDataDto>.Success((int)HttpStatusCode.NoContent);
        }

        public async Task<Response<UserViewDto>> GetUserByIdAsync(int id, bool? isActive = true)
        {
            var dbUser = await _unitOfWork.User.GetByIdAsync(id);

            if (dbUser is null || dbUser.IsActive.Equals(!isActive))
                return Response<UserViewDto>.Fail("User is not found", (int)HttpStatusCode.NotFound, true);

            return Response<UserViewDto>.Success(ObjectMapper.Mapper.Map<UserViewDto>(dbUser), (int)HttpStatusCode.OK);
        }

        public async Task<Response<List<UserViewDto>>> GetUsersAsync(PageQueryDto parameter)
        {
            var dbUsersQuery = _unitOfWork.User.GetAll()
                .Where(x => x.IsActive.Equals(parameter.IsActive));

            if (!string.IsNullOrEmpty(parameter.SearchKey))
            {
                dbUsersQuery = dbUsersQuery.Where(x => EF.Functions.ILike(x.FullName, "%" + parameter.SearchKey + "%"));
            }

            dbUsersQuery = string.IsNullOrEmpty(parameter.OrderBy)
                ? dbUsersQuery.OrderByDescending(x => x.CreatedDate)
                : dbUsersQuery.OrderBy(parameter.OrderBy);

            var dbUsersQueryLast = dbUsersQuery.Select(x => ObjectMapper.Mapper.Map<UserViewDto>(x));

            var dbUserList = await PagedList<UserViewDto>.ToPagedListAsync(dbUsersQueryLast.AsNoTracking(), parameter.PageId, parameter.PageSize);

            return Response<List<UserViewDto>>.Success(dbUserList, (int)HttpStatusCode.OK, dbUserList.CurrentPage, dbUserList.TotalCount);
        }

        public async Task<Response<UserDto>> UpdateUserAsync(UserDto userDto)
        {
            var dbUser = await _unitOfWork.User.GetByIdAsync(userDto.PkId);

            if (dbUser is null)
                return Response<UserDto>.Fail("User is not found", (int)HttpStatusCode.NotFound, true);

            userDto.Password = HashingHelper.HashPassword(userDto.Password);

            ObjectMapper.Mapper.Map(userDto, dbUser);

            await _unitOfWork.SaveChangesAsync();

            return Response<UserDto>.Success(ObjectMapper.Mapper.Map<UserDto>(dbUser), (int)HttpStatusCode.NoContent);
        }
    }
}
