using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIL_DesktopApp.DataModels;
using WIL_DesktopApp.DataModels.DbContexts;
using Attribute = WIL_DesktopApp.Models.Attribute;
using AttributeDTO = WIL_DesktopApp.DataModels.Attribute;
using RequestItem = WIL_DesktopApp.Models.RequestItem;
using RequestItemDTO = WIL_DesktopApp.DataModels.RequestItem;
using WIL_DesktopApp.Models;
using System.Windows;
using Org.BouncyCastle.Utilities;

namespace WIL_DesktopApp.Services.RequestServices
{
    public class RequestProvider : IRequestProvider
    {
        private readonly IKryptonDbContextFactory _kryptonDbContextFactory;

        public RequestProvider(IKryptonDbContextFactory kryptonDbContextFactory)
        {
            _kryptonDbContextFactory = kryptonDbContextFactory;
        }

        /// <summary>
        /// Gets light versions of all requests, as all request items within request do not have attributes
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Request> GetAllRequests()
        {
            using (KryptonDbContext context = _kryptonDbContextFactory.CreateKryptonDbContext())
            {
                var query = (from quoteRequests in context.QuoteRequests
                             join requestItems in context.RequestItems on quoteRequests.QuoteRequestId equals requestItems.QuoteRequestId
                             join attributeSelections in context.AttributeSelections on requestItems.RequestItemId equals attributeSelections.RequestItemId
                             join attributes in context.Attributes on attributeSelections.AttributeId equals attributes.AttributeId
                             select new
                             {
                                 quoteRequests.Email,
                                 ImgUrl = quoteRequests.ImgUrl ==null ? "" : quoteRequests.ImgUrl,
                                 Notes = quoteRequests.Notes == null ? "" : quoteRequests.Notes,
                                 quoteRequests.QuoteRequestId,
                                 requestItems.EstimateGiven,
                                 requestItems.Quantity,
                                 requestItems.RequestItemId,
                                 Name = requestItems.AttributeSelections.First().Attribute.Name
                             }).Distinct().OrderBy(s => s.QuoteRequestId).ThenBy(s => s.RequestItemId);

                List<Request> requests = new List<Request>();
                try 
                {
                    //Tries to query the database
                    var results = query.ToList();
                    //When the connection succeeds this code will be executed
                    List<RequestItem> items = new List<RequestItem>();
                    int currentRequest = 0;
                    for (int i = 0; i < results.Count; i++)
                    {
                        var result = results[i];

                        if (results[currentRequest].QuoteRequestId.Equals(result.QuoteRequestId))
                        {
                            RequestItem item = new RequestItem(result.RequestItemId, result.EstimateGiven, result.Name, result.Quantity);
                            items.Add(item);
                        }
                        else
                        {
                            Request request = new Request(results[currentRequest].QuoteRequestId, results[currentRequest].ImgUrl, results[currentRequest].Email, items, results[currentRequest].Notes);
                            requests.Add(request);

                            items = new List<RequestItem>();

                            RequestItem item = new RequestItem(result.RequestItemId, result.EstimateGiven, result.Name, result.Quantity);
                            items.Add(item);


                            currentRequest = i;
                        }
                        if (i == results.Count - 1)
                        {
                            Request request = new Request(result.QuoteRequestId, result.ImgUrl, result.Email, items, result.Notes);
                            requests.Add(request);
                        }
                    }
                }
                //If the query could not be executed an error message will prompt the user
                catch(Exception ex)
                {
                    MessageBox.Show("No database connection. "+ ex.Message,"Error",MessageBoxButton.OK,MessageBoxImage.Error);
                }

                return requests;
            }

        }

        public Task<IEnumerable<Request>> GetAllRequestsAsync()
        {
            throw new NotImplementedException();
        }

        public double GetCurrentMarkup()
        {
            using (KryptonDbContext context = _kryptonDbContextFactory.CreateKryptonDbContext())
            {
                double markup = 0;

                var query = (from settings in context.SystemSettings
                             select new
                             {
                                 settings.Name,
                                 settings.Value
                             }).Where(x => x.Name == "quotemarkup").SingleOrDefault();

                if (query != null)
                {
                    markup = (double)(Double.Parse(query.Value.Replace('.',',')) / 100);
                }
                return markup;
            }
        }

        /// <summary>
        /// Returns a full request given a lite request
        /// </summary>
        /// <param name="liteReq"></param>
        /// <returns></returns>
        public Request GetFullRequest(Request liteReq)
        {
            using (KryptonDbContext context = _kryptonDbContextFactory.CreateKryptonDbContext())
            {
                int requestId = liteReq.RequestId;

                var queryItems = (from requestItems in context.RequestItems.Where(r => r.QuoteRequestId == requestId)
                             join attributeSelections in context.AttributeSelections on requestItems.RequestItemId equals attributeSelections.RequestItemId
                             join attributes in context.Attributes on attributeSelections.AttributeId equals attributes.AttributeId
                             join materials in context.Materials on attributes.MaterialId equals materials.MaterialId into materialGroups
                             from material in materialGroups.DefaultIfEmpty()
                             join valueSelections in context.ValueSelections on attributeSelections.AttributeSelectionId equals valueSelections.AttributeSelectionId into valueSelectionGroups
                             from valueSelection in valueSelectionGroups.DefaultIfEmpty()
                             join valueType in context.ValueTypes on valueSelection.ValueTypeId equals valueType.ValueTypeId into valueTypeGroups
                             from valueType in valueTypeGroups.DefaultIfEmpty()
                             select new
                             {
                                 requestItems.RequestItemId,
                                 requestItems.Quantity,
                                 requestItems.EstimateGiven,
                                 AttributeSelectionId = attributeSelections.AttributeId,
                                 attributes.AttributeId,
                                 AttributeName = attributes.Name,
                                 attributes.PriceModifier,
                                 attributes.UseGlobalValue,
                                 MaterialPrice = material == null ? 0 : material.Price,
                                 ValueAmount = valueSelection == null ? 0 : valueSelection.Value,
                                 ValueName = valueType == null ? string.Empty : valueType.Name

                             }
                             ).ToList().GroupBy(r => new {r.RequestItemId, r.AttributeSelectionId}).GroupBy(r => r.Key.RequestItemId);

                var RequestItemResults = queryItems.ToList();

                List<RequestItem> RequestItems = new List<RequestItem>();

                foreach (var requestItem in RequestItemResults)
                {
                    List<Attribute> attributes = new List<Attribute>();
                    foreach(var attribute in requestItem)
                    {
                        Dictionary<string, double> values = new Dictionary<string, double>();
                        foreach(var value in attribute)
                        {
                            if(value.ValueAmount > 0)
                            values.Add(value.ValueName, value.ValueAmount);
                        }
                        var currentAttributeResult = attribute.First();
                        Attribute attributeInsert = new Attribute(currentAttributeResult.AttributeName, currentAttributeResult.AttributeId, currentAttributeResult.MaterialPrice, currentAttributeResult.PriceModifier, values);
                        attributes.Add(attributeInsert);
                    }
                    var currentRequestItemResult = requestItem.First().First();
                    RequestItem requestItemInsert = new RequestItem(currentRequestItemResult.RequestItemId, currentRequestItemResult.EstimateGiven, attributes, currentRequestItemResult.Quantity);
                    RequestItems.Add(requestItemInsert);
                }

                return new Request(liteReq.RequestId, liteReq.FileURL, liteReq.Email, RequestItems, liteReq.Notes);
            }
        }

        public Task<Request> GetFullRequestAsync(Request req)
        {
            throw new NotImplementedException();
        }
    }
}