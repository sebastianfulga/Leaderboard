using LeaderboardApp.Models;

namespace LeaderboardApp.Services;

public interface ILeaderboardService
{
    Task<IReadOnlyList<LeaderboardEntry>> GetRankingsAsync(CancellationToken cancellationToken = default);
}
