namespace Fintranet.TaxCalculation.Infrastructure.Helpers
{
    public interface IPageQuery
    {
        string[] OrderBy { get; set; }

        int PageSize { get; set; }

        int PageIndex { get; set; }
    }
}
