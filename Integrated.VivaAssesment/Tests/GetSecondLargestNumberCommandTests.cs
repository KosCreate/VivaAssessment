using Application.Commands;
using Application.Dtos.Requests;

namespace Tests;

public class GetSecondLargestNumberCommandTests
{
    private readonly GetSecondLargetNumberCommandHandler _handler;

    public GetSecondLargestNumberCommandTests()
    {
        _handler = new GetSecondLargetNumberCommandHandler();
    }

    [Fact]
    public async Task Handle_WithEmptyArray_ThrowsException()
    {
        var request = new RequestObj { RequestArrayObj = [] };
        var command = new GetSecondLargestNumberCommand(request);

        await Assert.ThrowsAsync<InvalidOperationException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_WithOnlyOneDistinctNumber_ThrowsException()
    {
        var request = new RequestObj { RequestArrayObj = [5, 5, 5] };
        var command = new GetSecondLargestNumberCommand(request);

        await Assert.ThrowsAsync<InvalidOperationException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_WithValidArray_ReturnsSecondLargestValue()
    {
        var request = new RequestObj { RequestArrayObj = [5, 10, 3, 8, 2] };
        var command = new GetSecondLargestNumberCommand(request);

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(8, result.SecondLargestValue);
    }
}
