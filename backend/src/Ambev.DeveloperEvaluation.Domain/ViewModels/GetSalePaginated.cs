namespace Ambev.DeveloperEvaluation.Domain.ViewModels;

public class GetSalePaginated
{
    public GetSalePaginated(int page, int pageSize, Guid? id = null, string saleNumber = "", string customer = "", string branch = "", string orderBy = "")
    {
        Page = page;
        PageSize = pageSize;
        Id = id;
        SaleNumber = saleNumber;
        Customer = customer;
        Branch = branch;
        OrderBy = orderBy;
    }

    public int Page { get; set; }
    public int PageSize { get; set; }
    public Guid? Id { get; set; }
    public string SaleNumber { get; set; } = string.Empty;
    public string Customer { get; set; } = string.Empty;
    public string Branch { get; set; } = string.Empty;
    public string OrderBy { get; set; } = string.Empty;
}