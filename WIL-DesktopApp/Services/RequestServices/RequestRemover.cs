using System;
using System.Threading.Tasks;
using System.Linq;
using WIL_DesktopApp.DataModels.DbContexts;
using WIL_DesktopApp.DataModels;
using WIL_DesktopApp.Models;

namespace WIL_DesktopApp.Services.RequestServices
{
    public class RequestRemover : IRequestRemover
    {
        private readonly IKryptonDbContextFactory _kryptonDbContextFactory;

        public RequestRemover(IKryptonDbContextFactory kryptonDbContextFactory)
        {
            _kryptonDbContextFactory = kryptonDbContextFactory;
        }

        public void RemoveRequest(Request request)
        {
            using(KryptonDbContext context = _kryptonDbContextFactory.CreateKryptonDbContext())
            {
                int reqId = request.RequestId;
                var requestQuery = (from quoteRequests in context.QuoteRequests.Where(q => q.QuoteRequestId == request.RequestId)
                                   select quoteRequests).SingleOrDefault();

                if(requestQuery != null)
                {
                    context.QuoteRequests.Remove(requestQuery);
                    context.SaveChanges();
                }
            }
        }

        public Task RemoveRequestAsync(Request request)
        {
            throw new NotImplementedException();
        }
    }
}
