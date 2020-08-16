using Entities.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Routing;
using static Entities.Helper.CustomValidation;

namespace KontaktHome
{
    public class MvcApplication : System.Web.HttpApplication
    {
		public object TempData { get; private set; }

		protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
			DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(RequiredIfAttribute), typeof(RequiredAttributeAdapter));
			ModelBinders.Binders.DefaultBinder = new TrimModelBinder();
		}
		
		//protected void Application_BeginRequest(Object sender, EventArgs e)
		//{
		//	HttpRuntimeSection runTime = (HttpRuntimeSection)WebConfigurationManager.GetSection("system.web/httpRuntime");
		//	//Approx 100 Kb(for page content) size has been deducted because the maxRequestLength proprty is the page size, not only the file upload size
		//	int maxRequestLength = (runTime.MaxRequestLength - 100) * 1024;

		//	//This code is used to check the request length of the page and if the request length is greater than 
		//	//MaxRequestLength then retrun to the same page with extra query string value action=exception

		//	HttpContext context = ((HttpApplication)sender).Context;
		//	if (context.Request.ContentLength > maxRequestLength)
		//	{
		//		ExceptionContext filterContext = new ExceptionContext();
		//		filterContext.Controller.TempData["LastError"] = "Size error";

		//		filterContext.ExceptionHandled = true;
		//		filterContext.Result = new RedirectResult("/Home/HasError");
		//		//IServiceProvider provider = (IServiceProvider)context;
		//		//HttpWorkerRequest workerRequest = (HttpWorkerRequest)provider.GetService(typeof(HttpWorkerRequest));
		//		//// Check if body contains data
		//		//if (workerRequest.HasEntityBody())
		//		//{
		//		//	// get the total body length
		//		//	int requestLength = workerRequest.GetTotalEntityBodyLength();
		//		//	// Get the initial bytes loaded
		//		//	int initialBytes = 0;
		//		//	if (workerRequest.GetPreloadedEntityBody() != null)
		//		//		initialBytes = workerRequest.GetPreloadedEntityBody().Length;
		//		//	if (!workerRequest.IsEntireEntityBodyIsPreloaded())
		//		//	{
		//		//		byte[] buffer = new byte[512000];
		//		//		// Set the received bytes to initial bytes before start reading
		//		//		int receivedBytes = initialBytes;
		//		//		while (requestLength - receivedBytes >= initialBytes)
		//		//		{
		//		//			// Read another set of bytes
		//		//			initialBytes = workerRequest.ReadEntityBody(buffer, buffer.Length);
		//		//			// Update the received bytes
		//		//			receivedBytes += initialBytes;
		//		//		}
		//		//		initialBytes = workerRequest.ReadEntityBody(buffer, requestLength - receivedBytes);
		//		//	}
		//		//}
		//		//// Redirect the user to the same page with querystring action=exception. 
		//		//context.Response.Redirect("/Home/Index");

		//	}
		//}
		protected void Application_Error(Object sender, EventArgs e) 
		{
			var ex = Server.GetLastError();
			if (ex.Message.Contains("Maximum request length exceeded"))
			{
				Response.Redirect("/Home/HasError");
			}
		}
	}

}
