﻿using Backend.Interfaces;
using Backend.Models;
using System.Linq;
using System.Text.RegularExpressions;

namespace Backend.DAL
{
    public class CategoryManager : ICategoryManager
    {
        public bool CheckExpensesOfBudget(Budget budget)
        {
            budget.Expenses.ForEach(x => CheckIfCategorynameIsValid(x));
            budget.Expenses.ForEach(x => CheckCategoryTotalAmountIsCalculatedCorrectly(x));

            return true;
        }
        public bool CheckIncomeOfBudget(Budget budget)
        {            
            CheckIfCategorynameIsValid(budget.Income);
            CheckCategoryTotalAmountIsCalculatedCorrectly(budget.Income);

            return true;
        }

        private void CheckCategoryTotalAmountIsCalculatedCorrectly(Category category)
        {
            float categoryCost = 0;

            foreach (var item in category.Items)
            {
                categoryCost += item.Amount;
            }

            if (category.TotalAmount != categoryCost)
            {
                throw new InvalidOperationException("Calculations invalid at category cost " + category.Name + " calculations Categorycost: " + categoryCost + " and " + category.TotalAmount + " is not the same.");
            }
        }

        /// <summary>
        /// THIS IS FOR TESTING CheckCategoryTotalAmountIsCalculatedCorrectly(Budget budget)
        /// </summary>
        /// <param name="Budget"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// 
        public bool TEST_CheckCategoryTotalAmountIsCalculatedCorrectly(Category category)
        {
            float categoryCost = 0;
            // do we need or is this set in frontEnd?
            foreach (var item in category.Items)
            {
                categoryCost += item.Amount;
            }

            if (category.TotalAmount != categoryCost)
            {
                throw new InvalidOperationException("Calculations invalid at category cost " + category.Name + " calculations Categorycost: " + categoryCost + " and " + category.TotalAmount + " is not the same.");
            }

            return true;
        }

        private void CheckIfCategorynameIsValid(Category category)
        {
            List<string> invalidSqlExpressions = new List<string>() { "Delete", "Insert", "Into", "Alter", "Drop Table", "Select", "Create Database", "Truncate" };

            if (string.IsNullOrWhiteSpace(category.Name))
            {
                throw new ArgumentException("Name cannot be null, empty, or whitespace." + category.Id.ToString());
            }

            if (category.Name.Length > 50)
            {
                throw new ArgumentException("Name cannot be longer then 50 characters.");
            }

            if (!char.IsLetterOrDigit(category.Name[0]))
            {
                throw new ArgumentException("Name cannot start with a special character.");
            }


            foreach (string sql in invalidSqlExpressions.Where(x => category.Name.ToLower().Contains(x.ToLower())))
            {
                
                 throw new ArgumentException("Name cannot contain any sql keywords! " + category.Id + " " + category.Name);
                
            }

            // Regex: Each word must start with an alphanumeric character, underscore, or dash.
            Regex validNameRegex = new Regex(@"^[a-zåäöA-ZÅÄÖ0-9-_]+( [a-zåäöA-ZÅÄÖ0-9-_]+)*$", RegexOptions.None, TimeSpan.FromMilliseconds(2000));
            if (!validNameRegex.IsMatch(category.Name))
            {
                throw new ArgumentException("Name contains invalid characters. : " + category.Name);
            }
        }

        /// <summary>
        /// THIS IS FOR TESTING CheckIfCategorynameIsValid(Budget budget)
        /// </summary>
        /// <param name="Budget"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public bool TEST_CheckIfCategorynameIsValid(Category category)
        {
            List<string> invalidSqlExpressions = new List<string>() { "Delete", "Insert", "Into", "Alter", "Drop Table", "Select", "Create Database", "Truncate" };

            if (string.IsNullOrWhiteSpace(category.Name))
            {
                throw new ArgumentException("Name cannot be null, empty, or whitespace." + category.Id.ToString());
            }

            if (category.Name.Length > 50)
            {
                throw new ArgumentException("Name cannot be longer then 50 characters.");
            }

            if (!char.IsLetterOrDigit(category.Name[0]))
            {
                throw new ArgumentException("Name cannot start with a special character.");
            }

            foreach (string sql in invalidSqlExpressions.Where(x => category.Name.ToLower().Contains(x.ToLower())))
            {

                throw new ArgumentException("Name cannot contain any sql keywords! " + category.Id + " " + category.Name);

            }

            // Regex: Each word must start with an alphanumeric character, underscore, or dash.
            // This allows for whitespace to be inside the string, but not have leading/trailing due to Trim();
            Regex validNameRegex = new Regex(@"^[a-zåäöA-ZÅÄÖ0-9-_]+( [a-zåäöA-ZÅÄÖ0-9-_]+)*$", RegexOptions.None, TimeSpan.FromMilliseconds(2000));
            if (!validNameRegex.IsMatch(category.Name))
            {
                throw new ArgumentException("Name contains invalid characters. : " + category.Name);
            }


            return true;
        }
    }
}
