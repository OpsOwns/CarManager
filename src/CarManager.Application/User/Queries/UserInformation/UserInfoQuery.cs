using CarManager.Application.Abstractions.Cqrs.Queries;
using CarManager.Application.User.Responses;

namespace CarManager.Application.User.Queries.UserInformation;

public record UserInfoQuery : IQuery<UserInfoResponse>;