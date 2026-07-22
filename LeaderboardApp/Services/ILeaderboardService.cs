using LeaderboardApp.Models;

namespace LeaderboardApp.Services;

public interface ILeaderboardService
{
    Task<LeaderboardPage> GetRankingsAsync(
        LeaderboardQuery query,
        CancellationToken cancellationToken = default);
}
