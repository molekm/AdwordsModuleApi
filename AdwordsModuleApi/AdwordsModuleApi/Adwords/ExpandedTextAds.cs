using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AdwordsModuleApi.Models;
using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201710;

namespace AdwordsModuleApi.Adwords
{
    public class ExpandedTextAds
    {
        public static AdGroupAdReturnValue CreateTextAds(AdWordsUser user, AdWordsContentLo adWordsContent)
        {
            using (AdGroupAdService adGroupAdService =
                (AdGroupAdService)user.GetService(AdWordsService.v201710.AdGroupAdService))
            {

                List<AdGroupAdOperation> operations = new List<AdGroupAdOperation>();

                for (int i = 0; i < adWordsContent.ContentProducts.Count; i++)
                {
                    // Create the expanded text ad.
                    ExpandedTextAd expandedTextAd = new ExpandedTextAd();
                    expandedTextAd.headlinePart1 = adWordsContent.ContentProducts[i].AdContent.HeadLinePart1;
                    expandedTextAd.headlinePart2 = adWordsContent.ContentProducts[i].AdContent.HeadLinePart2;
                    expandedTextAd.path1 = adWordsContent.ContentProducts[i].AdContent.Path1 != "" ? adWordsContent.ContentProducts[i].AdContent.Path1 : "";
                    expandedTextAd.path2 = adWordsContent.ContentProducts[i].AdContent.Path2 != "" ? adWordsContent.ContentProducts[i].AdContent.Path2 : "";
                    expandedTextAd.description = adWordsContent.ContentProducts[i].AdContent.Description;
                    expandedTextAd.finalUrls = adWordsContent.ContentProducts[i].FinalUrl;

                    AdGroupAd expandedTextAdGroupAd = new AdGroupAd();
                    expandedTextAdGroupAd.adGroupId = adWordsContent.AdGroupLo.adGroupId;
                    expandedTextAdGroupAd.ad = expandedTextAd;

                    // Optional: Set the status.
                    expandedTextAdGroupAd.status = AdGroupAdStatus.PAUSED;

                    // Create the operation.
                    AdGroupAdOperation operation = new AdGroupAdOperation();
                    operation.@operator = Operator.ADD;
                    operation.operand = expandedTextAdGroupAd;

                    operations.Add(operation);
                }

                AdGroupAdReturnValue retVal = null;

                try
                {
                    // Create the ads.
                    retVal = adGroupAdService.mutate(operations.ToArray());


                    adGroupAdService.Close();
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
                            PolicyViolationError policyError = (PolicyViolationError) apiError;

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
                catch (Exception ex)
                {
                    throw new System.ApplicationException("Failed to create expanded text ad.", ex);
                }
                return retVal;
            }
        }
    }
}