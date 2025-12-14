using DataProtection.Requests;
using DataProtection.Responses;

namespace DataProtection.Services;

public interface IBiddingService
{
    Task<BidResponse> CreateBidAsync(CreateBidRequest request);
    Task<BidResponse?> GetBidAsync(Guid id);
    Task<List<BidResponse>> GetAllBidsAsync();
}
