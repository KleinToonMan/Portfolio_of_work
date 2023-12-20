﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIL_DesktopApp.Services.RequestServices;

namespace WIL_DesktopApp.Models
{
    public class RequestRepository
    {
        private readonly IRequestProvider requestProvider;
        private readonly IRequestRemover requestRemover;
        private double markup;
        public RequestRepository(IRequestProvider requestProvider, IRequestRemover requestRemover)
        {
            this.requestProvider = requestProvider;
            this.requestRemover = requestRemover;
            this.markup = requestProvider.GetCurrentMarkup();
        }

        public IEnumerable<Request> GetRequests()
        {
            return requestProvider.GetAllRequests();
        }

        public Request GetFullRequest(Request req)
        {
            Request FullRequest = requestProvider.GetFullRequest(req);
            foreach(var item  in FullRequest.RequestItems)
            {
                item.Markup = markup;
            }
            return FullRequest;
        }

        public void RemoveRequestByID(Request req)
        {
            requestRemover.RemoveRequest(GetFullRequest(req));
        }
        public void RemoveRequest(Request request)
        {
            requestRemover.RemoveRequest(request);
        }
    }
}