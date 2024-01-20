namespace Fintranet.TaxCalculation.Test.Test.Domain.Test;
public class BaseEntityTest
{
    private BaseEntity entity = new BaseEntity();
    [Fact]
    public void Constructor_PropertiesInitializedCorrectly()
    {
      
        // Assert
        Assert.NotEqual(Guid.Empty, entity.Id);
        Assert.True(entity.CreatedAt <= DateTime.Now);
        Assert.True(entity.UpdatedAt <= DateTime.Now);
        Assert.False(entity.IsDeleted);
    }

    [Fact]
    public void Delete_MethodSetsIsDeletedTrueAndUpdatesUpdatedAt()
    {
      
        DateTime originalUpdatedAt = entity.UpdatedAt;

        // Act
        entity.Delete();

        // Assert
        Assert.True(entity.IsDeleted);
        Assert.True(entity.UpdatedAt > originalUpdatedAt);
    }
}
