namespace LeaderboardApp.Models;

public sealed record LeaderboardPage(
    IReadOnlyList<LeaderboardEntry> Items,
    int TotalCount,
    int PageNumber,
    int PageSize)
{
    public int TotalPages => TotalCount == 0
        ? 0
        : (int)Math.Ceiling(TotalCount / (double)PageSize);
}
