using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Application.Helpers;
using Oniria.Infrastructure.Identity.Entities;
using Oniria.Infrastructure.Identity.Features.User.Queries;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Oniria.Core.Application.Features.JWT.Commands
{
    public class CreateJWTTokenAsyncCommand : IRequest<OperationResult<JwtSecurityToken>>
    {
        public ApplicationUser User { get; set; }
    }

    public class CreateJWTTokenAsyncCommandHandler : IRequestHandler<CreateJWTTokenAsyncCommand, OperationResult<JwtSecurityToken>>
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly JWTSettings JWTSettings;
        private readonly IMediator mediator;

        public CreateJWTTokenAsyncCommandHandler(
            UserManager<ApplicationUser> userManager,
            IOptions<JWTSettings> JWTSettings,
            IMediator mediator
        )
        {
            this.userManager = userManager;
            this.JWTSettings = JWTSettings.Value;
            this.mediator = mediator;
        }

        public async Task<OperationResult<JwtSecurityToken>> Handle(CreateJWTTokenAsyncCommand request, CancellationToken cancellationToken)
        {
            var result = OperationResult<JwtSecurityToken>.Create();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, request.User.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, GeneratorHelper.GuidString()),
                new Claim(JwtRegisteredClaimNames.Email, request.User.Email),
                new Claim("userId", request.User.Id)
            }
            .Union(await userManager.GetClaimsAsync(request.User))
            .Union(
                (await mediator.Send(new GetAllUserRolesAsyncQuery { User = request.User })).Data!
                .Select(role =>
                    new Claim("roles", role.ToString())
                ).ToList()
            );

            var symmectricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTSettings.Key));

            var signinCredentials = new SigningCredentials(symmectricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: JWTSettings.Issuer,
                audience: JWTSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(JWTSettings.DurationInMinutes),
                signingCredentials: signinCredentials
            );

            result.Data = jwtSecurityToken;

            return result;
        }
    }
}
