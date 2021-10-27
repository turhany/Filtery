namespace Filtery.Models.Filter
{
    public enum FilterOperation
    {
        Equal = 0,
        NotEqual = 1,
        Contains = 2,
        GreaterThan = 3,
        LessThan = 4,
        GreaterThanOrEqual = 5,
        LessThanOrEqual = 6,
        StartsWith = 7,
        EndsWith = 8
    }
}