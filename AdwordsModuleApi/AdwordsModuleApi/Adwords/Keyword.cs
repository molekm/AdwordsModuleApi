using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls.WebParts;
using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads;
using Google.Api.Ads.AdWords.v201710;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using ParallelDots;

namespace AdwordsModuleApi.Adwords
{
    public class AdwordsKeyword
    {
        public static AdGroupCriterionReturnValue AdKeyWordsToAdGroup(AdWordsUser user, long adGroupId, string[] keyWords)
        {
            using (AdGroupCriterionService adGroupCriterionService =
                (AdGroupCriterionService)user.GetService(
                    AdWordsService.v201710.AdGroupCriterionService))
            {
                AdGroupCriterionReturnValue retVal = new AdGroupCriterionReturnValue();
                List<AdGroupCriterionOperation> operations = new List<AdGroupCriterionOperation>();

                foreach (string keywordText in keyWords)
                {
                    // Create the keyword.
                    Keyword keyword = new Keyword();
                    keyword.text = keywordText;
                    keyword.matchType = KeywordMatchType.BROAD;

                    // Create the biddable ad group criterion.
                    BiddableAdGroupCriterion keywordCriterion = new BiddableAdGroupCriterion();
                    keywordCriterion.adGroupId = adGroupId;
                    keywordCriterion.criterion = keyword;

                    // Optional: Set the user status.
                    keywordCriterion.userStatus = UserStatus.ENABLED;

                    // Optional: Set the keyword destination url.
                    //keywordCriterion.finalUrls = new UrlList()
                    //{
                    //    urls = new string[] {
                    //    "http://example.com/mars/cruise/?kw=" + HttpUtility.UrlEncode(keywordText)
                    //}
                    //};

                    // Create the operations.
                    AdGroupCriterionOperation operation = new AdGroupCriterionOperation();
                    operation.@operator = Operator.ADD;
                    operation.operand = keywordCriterion;

                    operations.Add(operation);
                }
                try
                {
                    // Create the keywords.
                    retVal = adGroupCriterionService.mutate(
                        operations.ToArray());

                    
                }
                catch (AdWordsApiException e)
                {
                    ApiException innerException = e.ApiException as ApiException;
                    if (innerException == null)
                    {
                        throw new Exception("Failed to retrieve ApiError. See inner exception for more " +
                                            "details.", e);
                    }

                    // Examine each ApiError received from the server.
                    foreach (ApiError apiError in innerException.errors)
                    {
                        int index = apiError.GetOperationIndex();
                        if (index == -1)
                        {
                            // This API error is not associated with an operand, so we cannot
                            // recover from this error by removing one or more operations.
                            // Rethrow the exception for manual inspection.
                            throw;
                        }

                        // Handle policy violation errors.
                        if (apiError is PolicyViolationError)
                        {
                            PolicyViolationError policyError = (PolicyViolationError)apiError;

                            if (policyError.isExemptable)
                            {
                                // If the policy violation error is exemptable, add an exemption
                                // request.
                                List<ExemptionRequest> exemptionRequests = new List<ExemptionRequest>();

                            }
                            else
                            {
                                // Policy violation error is not exemptable, remove this
                                // operation from the list of operations.

                            }
                        }
                        else
                        {
                            // This is not a policy violation error, remove this operation
                            // from the list of operations.

                        }
                    }
                }
                catch (Exception e)
                {
                    throw new System.ApplicationException("Failed to create keywords.", e);
                }
                return retVal;
            }
        }
    }
}