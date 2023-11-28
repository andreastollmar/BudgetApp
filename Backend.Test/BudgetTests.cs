using Backend.DAL;
using Backend.Models;

namespace Backend.Test
{
    public class BudgetTests
    {
        [Fact]
        public void BudgetsGetInitializedWhenCalledOnBudgetManager() // rename?
        {
            // Arrange
            var itemManager = new ItemManager();
            var categoryManager = new CategoryManager();
            var sut = new BudgetManager(categoryManager, itemManager);           

            // Act
            var smallBudget = sut.SmallBudget;
            var mediumBudget = sut.MediumBudget;
            var largeBudget = sut.LargeBudget;

            

            // Assert
            Assert.NotNull(smallBudget);
            Assert.NotNull(mediumBudget);
            Assert.NotNull(largeBudget);
        }

        [Fact]
        public void LargeBudgetGets10CategoriesAsDefault() 
        {
            // Arrange
            var expected = 10;
            var itemManager = new ItemManager();
            var categoryManager = new CategoryManager();
            var sut = new BudgetManager(categoryManager, itemManager);

            //Act
            var largerBudget = sut.LargeBudget;

            // Assert
            Assert.Equal(expected, largerBudget.Expenses.Count());
        }

        [Fact]
        public void MediumBudgetGets7CategoriesAsDefault()
        {
            // Arrange
            var expected = 7;
            var itemManager = new ItemManager();
            var categoryManager = new CategoryManager();
            var sut = new BudgetManager(categoryManager, itemManager);

            // Act
            var actual = sut.MediumBudget;

            // Assert
            Assert.Equal(expected, actual.Expenses.Count());
        }

        [Fact]
        public void SmallBudgetGets5CategoriesAsDefault()
        {
            // Arrange
            var expected = 5;
            var itemManager = new ItemManager();
            var categoryManager = new CategoryManager();
            var sut = new BudgetManager(categoryManager, itemManager);

            // Act
            var actual = sut.SmallBudget;

            // Assert
            Assert.Equal(expected, actual.Expenses.Count());
        }




    }
}