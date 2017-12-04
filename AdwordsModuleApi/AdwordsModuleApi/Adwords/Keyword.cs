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
        public List<KeyValuePair> GetKeyWords(AdWordsUser user, string[] words)
        {
            using (TargetingIdeaService targetingIdeaService =
                (TargetingIdeaService)user.GetService(AdWordsService.v201710.TargetingIdeaService))
            {
                // Create selector.
                TargetingIdeaSelector selector = new TargetingIdeaSelector();
                selector.requestType = RequestType.IDEAS;
                selector.ideaType = IdeaType.KEYWORD;
                selector.requestedAttributeTypes = new AttributeType[] {
                    AttributeType.KEYWORD_TEXT,
                    AttributeType.SEARCH_VOLUME,
                    AttributeType.AVERAGE_CPC,
                    AttributeType.COMPETITION,
                    AttributeType.CATEGORY_PRODUCTS_AND_SERVICES
                };


                List<SearchParameter> searchParameters = new List<SearchParameter>();

                // Create related to query search parameter.
                RelatedToQuerySearchParameter relatedToQuerySearchParameter =
                    new RelatedToQuerySearchParameter();
                relatedToQuerySearchParameter.queries = words;

                searchParameters.Add(relatedToQuerySearchParameter);

                // Add a language search parameter (optional).
                // The ID can be found in the documentation:
                //   https://developers.google.com/adwords/api/docs/appendix/languagecodes
                LanguageSearchParameter languageParameter = new LanguageSearchParameter();
                Language danish = new Language();
                danish.id = 1009;
                languageParameter.languages = new Language[] { danish };
                searchParameters.Add(languageParameter);

                // Add network search parameter (optional).
                NetworkSetting networkSetting = new NetworkSetting();
                networkSetting.targetGoogleSearch = true;
                networkSetting.targetSearchNetwork = false;
                networkSetting.targetContentNetwork = false;
                networkSetting.targetPartnerSearchNetwork = false;

                NetworkSearchParameter networkSearchParameter = new NetworkSearchParameter();
                networkSearchParameter.networkSetting = networkSetting;
                searchParameters.Add(networkSearchParameter);

                // Set the search parameters.
                selector.searchParameters = searchParameters.ToArray();

                // Set selector paging (required for targeting idea service).
                Paging paging = Paging.Default;
                paging.numberResults = 10000;

                selector.paging = paging;

                TargetingIdeaPage page = new TargetingIdeaPage();

                List<KeyValuePair> keyValuePairs = new List<KeyValuePair>();
                try
                {
                    int i = 0;
                    do
                    {
                        // Get related keywords.
                        page = targetingIdeaService.get(selector);

                        // Display related keywords.
                        if (page.entries != null && page.entries.Length > 0)
                        {
                            foreach (TargetingIdea targetingIdea in page.entries)
                            {
                                Dictionary<AttributeType, Google.Api.Ads.AdWords.v201710.Attribute> ideas =
                                    targetingIdea.data.ToDict();

                                string keyword = (ideas[AttributeType.KEYWORD_TEXT] as StringAttribute).value;
       
                                double competition = (ideas[AttributeType.COMPETITION] as DoubleAttribute).value;

                                i++;
                                string cutKeyword = keyword.Substring(0, keyword.Length - 8).Trim();
                                if(cutKeyword != "red herring")
                                    keyValuePairs.Add(new KeyValuePair() { Keyword = cutKeyword, Value = competition});
                            }
                        }
                        selector.paging.IncreaseOffset();
                    } while (selector.paging.startIndex < page.totalNumEntries);
                    return keyValuePairs.DistinctBy(key => key.Keyword).ToList();
                }
                catch (Exception e)
                {
                    throw new System.ApplicationException("Failed to retrieve related keywords.", e);
                }
            }
        }

        public AdGroupCriterionReturnValue AdKeyWordsToAdGroup(AdWordsUser user, long adGroupId)
        {
            using (AdGroupCriterionService adGroupCriterionService =
                (AdGroupCriterionService)user.GetService(
                    AdWordsService.v201710.AdGroupCriterionService))
            {
                AdGroupCriterionReturnValue retVal = new AdGroupCriterionReturnValue();
                List<AdGroupCriterionOperation> operations = new List<AdGroupCriterionOperation>();

                string[] keyWords = new string[] { "ostekage", "black friday", "malt"};

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

    public class KeyValuePair
    {
        public string Keyword { get; set; }
        public double Value { get; set; }

    }

}