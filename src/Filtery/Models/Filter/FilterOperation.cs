namespace Filtery.Models.Filter
{
    public enum FilterOperation
    {
        Equal = 0,
        NotEqual = 1,
        Contains = 2,
        GreaterThan = 3,
        LowerThan = 4,
        GreaterThanAndEqual = 5,
        LowerThanAndEqual = 6,
        Include = 7,
        Between = 8
    }
}