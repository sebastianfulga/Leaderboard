namespace LeaderboardApp.Models;

public sealed record LeaderboardQuery(
    int PageNumber,
    int PageSize,
    string? SearchTerm = null);
