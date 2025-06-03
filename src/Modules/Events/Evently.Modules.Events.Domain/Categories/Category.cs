using Evently.Modules.Events.Domain.Abstractions;
namespace Evently.Modules.Events.Domain.Categories;

public class Category : Entity
{
    private Category() {}
    
    public Guid Id { get; private set; }
    
    public string Name { get; private set; }
    
    public bool IsArchived { get; private set; }

    public static Category Create(string name)
    {
        var category = new Category
        {
            Id = Guid.NewGuid(),
            Name = name,
            IsArchived = false
        };
        
        // Raise domain event for category creation here

        return category;
    }
}
