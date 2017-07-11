using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyChatApp.Models
{
    public class ErrorHandlingPipelineModule : HubPipelineModule
    {
        protected override void OnIncomingError(ExceptionContext exceptionContext, IHubIncomingInvokerContext invokerContext)
        {
            // TODO 
            base.OnIncomingError(exceptionContext, invokerContext);
        }
    }
}