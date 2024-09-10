namespace ChallengeN5.Query.Domain.Architecture.Extension;

public static class PagingExtensions
{
    public static IQueryable<T> Page<T>(this IQueryable<T> query, int pageIndex, int pageSize)
    {
        //TODO MUST BE CREATE A HANDLE EXCEPTION FOR VALIDATE TO ARGUMENTS
        return query.Skip(pageIndex * pageSize).Take(pageSize);
    }

    public static IEnumerable<T> Page<T>(this IEnumerable<T> query, int pageIndex, int pageSize)
    {
        //TODO MUST BE CREATE A HANDLE EXCEPTION FOR VALIDATE TO ARGUMENTS

        return query.Skip(pageIndex * pageSize).Take(pageSize);
    }
}