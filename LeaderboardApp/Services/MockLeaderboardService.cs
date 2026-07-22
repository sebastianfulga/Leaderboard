using LeaderboardApp.Models;

namespace LeaderboardApp.Services;

public sealed class MockLeaderboardService : ILeaderboardService
{
    private static readonly string[] FirstNames =
    [
        "Jordan", "Maya", "Cameron", "Alex", "Riley", "Morgan", "Taylor", "Casey", "Avery", "Drew"
    ];

    private static readonly string[] LastNames =
    [
        "Stone", "Reyes", "Hayes", "Brooks", "Patel", "Carter", "Knight", "Diaz", "Foster", "Kim"
    ];

    private static readonly string[] Teams =
    [
        "North Club", "Metro Athletic", "Valley United", "Harbor Squad", "Peak Performance", "Central Academy"
    ];

    private static readonly IReadOnlyList<LeaderboardEntry> Rankings = Enumerable.Range(1, 1000)
        .Select(CreateEntry)
        .ToList();

    public Task<LeaderboardPage> GetRankingsAsync(
        LeaderboardQuery query,
        CancellationToken cancellationToken = default)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(query.PageNumber, 1);
        ArgumentOutOfRangeException.ThrowIfLessThan(query.PageSize, 1);
        cancellationToken.ThrowIfCancellationRequested();

        var filteredRankings = string.IsNullOrWhiteSpace(query.SearchTerm)
            ? Rankings
            : Rankings
                .Where(entry => entry.Name.Contains(
                    query.SearchTerm.Trim(),
                    StringComparison.OrdinalIgnoreCase))
                .ToList();

        var items = filteredRankings
            .Skip((query.PageNumber - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToList();

        return Task.FromResult(new LeaderboardPage(
            items,
            filteredRankings.Count,
            query.PageNumber,
            query.PageSize));
    }

    private static LeaderboardEntry CreateEntry(int rank)
    {
        var firstName = FirstNames[(rank - 1) % FirstNames.Length];
        var lastName = LastNames[((rank - 1) / FirstNames.Length) % LastNames.Length];
        var rating = Math.Max(62, 98.4 - rank * 0.036);
        var wins = Math.Max(8, 42 - rank % 29);
        var losses = 4 + rank % 17;

        return new LeaderboardEntry(
            rank,
            $"{firstName} {lastName}",
            Teams[(rank - 1) % Teams.Length],
            rating,
            12800 - rank * 9,
            $"{wins}-{losses}",
            rank % 5 - 2,
            string.Join("", Enumerable.Range(0, 5).Select(index => (rank + index) % 4 == 0 ? "L" : "W")));
    }
}
