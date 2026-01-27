namespace ExpenseTrackerApp.Domain.Entities;

public class Category
{
    public int CategoryId { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public string Name { get; set; } = string.Empty;

    public int? ParentCategoryId { get; set; } = null;
    public Category? ParentCategory { get; set; }
    
    public ICollection<Category> ChildrenCategories { get; set; } = new List<Category>();

}