using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Subscriptions;
using MediatR;

namespace GymManagement.Application.Subscriptions.Commands.Queries;

public class GetSubscriptionQueryHandler(ISubscriptionRepository subscriptionRepository)
    : IRequestHandler<GetSubscriptionQuery, ErrorOr<Subscription>>
{
    public readonly ISubscriptionRepository _subscriptionRepository = subscriptionRepository;

    public async Task<ErrorOr<Subscription>> Handle(GetSubscriptionQuery query, CancellationToken cancellationToken)
    {
        var subscription = await _subscriptionRepository.GetByIdAsync(query.SubscriptionId);

        return subscription is null
            ? Error.NotFound(description: "Subscription Not Found")
            : subscription;
    }
}