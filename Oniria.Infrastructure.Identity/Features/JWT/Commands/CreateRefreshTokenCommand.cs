using MediatR;
using Oniria.Core.Application.Features.Base;
using Oniria.Core.Dtos.JWT;
using System.Security.Cryptography;

namespace Oniria.Infrastructure.Identity.Features.JWT.Commands
{
    public class CreateRefreshTokenCommand : IRequest<OperationResult<RefreshToken>> { }

    public class CreateRefreshTokenCommandHandler : IRequestHandler<CreateRefreshTokenCommand, OperationResult<RefreshToken>>
    {
        public Task<OperationResult<RefreshToken>> Handle(CreateRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var result = OperationResult.Create<RefreshToken>();

            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new Byte[40];
            rngCryptoServiceProvider.GetBytes(randomBytes);

            result.Data = new RefreshToken()
            {
                Token = BitConverter.ToString(randomBytes).Replace("-", ""),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
            };

            return Task.FromResult(result);
        }
    }
}
