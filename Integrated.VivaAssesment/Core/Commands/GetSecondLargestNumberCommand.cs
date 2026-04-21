using Application.Dtos.Requests;
using Application.Dtos.Responses;
using MediatR;

namespace Application.Commands {
    public sealed record GetSecondLargestNumberCommand(RequestObj Request) : IRequest<SecondLargestResponse> { }

    public sealed class GetSecondLargetNumberCommandHandler : IRequestHandler<GetSecondLargestNumberCommand, SecondLargestResponse> {

        public Task<SecondLargestResponse> Handle(GetSecondLargestNumberCommand request, CancellationToken cancellationToken) {
            var distinctNumbers = request.Request.RequestArrayObj
                .Distinct()
                .OrderByDescending(x => x)
                .ToList();

            return Task.FromResult(new SecondLargestResponse() {
                SecondLargestValue = distinctNumbers.Skip(1).First()
            });
        }
    }
}
