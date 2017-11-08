using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AdwordsModuleApi.Models;
using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201710;

namespace AdwordsModuleApi.Adwords
{
    public static class Budgets
    {

        public static Budget CreateBudget(AdWordsUser user, CampaignDto campaignDto)
        {
            using (BudgetService budgetService =
                (BudgetService)user.GetService(AdWordsService.v201710.BudgetService))
            {

                // Create the campaign budget.
                Budget budget = new Budget();
                budget.isExplicitlyShared = false;
                budget.name = campaignDto.BudgetDto.Name;
                budget.deliveryMethod = BudgetBudgetDeliveryMethod.STANDARD;
                budget.amount = new Money();
                budget.amount.microAmount = campaignDto.BudgetDto.MicroAmount;


                BudgetOperation budgetOperation = new BudgetOperation();
                budgetOperation.@operator = Operator.ADD;
                budgetOperation.operand = budget;

                try
                {
                    BudgetReturnValue budgetRetval = budgetService.mutate(
                        new BudgetOperation[] { budgetOperation });
                    return budgetRetval.value[0];
                }
                catch (Exception e)
                {
                    throw new System.ApplicationException("Failed to add budget.", e);
                }
            }
        }
    }
}