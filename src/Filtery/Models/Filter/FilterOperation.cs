namespace Filtery.Models.Filter
{
    public enum FilterOperation
    {
        Equal = 0,
        NotEqual = 1,
        Contains = 2,
        GreaterThan = 3,
        LowerThan = 4,
        GreaterThanOrEqual = 5,
        LowerThanOrEqual = 6,
        Include = 7,
        Between = 8,
        StartsWith = 9,
        EndsWith = 10
    }
}