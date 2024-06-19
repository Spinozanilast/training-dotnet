using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Subscriptions;
using MediatR;

namespace GymManagement.Application.Subscriptions.Commands.CreateSubscription;

public class CreateSubscriptionCommandHandler : IRequestHandler<CreateSubscriptionCommand, ErrorOr<Subscription>>
{
    private readonly IUnitWork _unitOfWork;
    private readonly ISubscriptionRepository _subcriptionsRepository;

    public CreateSubscriptionCommandHandler(ISubscriptionRepository subcriptionsRepository, IUnitWork unitOfWork)
    {
        _subcriptionsRepository = subcriptionsRepository;
        _unitOfWork = unitOfWork;

    }

    public async Task<ErrorOr<Subscription>> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var subscription = new Subscription(subscriptionType: request.SubscriptionType,
        adminId: request.adminId);

        await _subcriptionsRepository.AddSubscriptionAsync(subscription);
        await _unitOfWork.CommitChangesAsync();

        return subscription;
    }
}