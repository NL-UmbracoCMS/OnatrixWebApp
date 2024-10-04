using Microsoft.AspNetCore.Mvc;
using Onatrix.Models;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Website.Controllers;

namespace Onatrix.Controllers;

public class ContactSurfaceController(IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory, ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider, HttpClient httpClient) : SurfaceController(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
{
	private readonly HttpClient _httpClient = httpClient;
	public async Task<IActionResult> HandleBigSubmit(ContactFormModel contactFormModel)
	{
		if (!ModelState.IsValid)
		{
			ViewData["userName"] = contactFormModel.UserName;
			ViewData["emailBig"] = contactFormModel.Email;
			ViewData["message"] = contactFormModel.Message;
			ViewData["phone"] = contactFormModel.Phone;

			ViewData["error_userName"] = string.IsNullOrEmpty(contactFormModel.UserName);
			ViewData["error_emailBig"] = string.IsNullOrEmpty(contactFormModel.Email);
			ViewData["error_message"] = string.IsNullOrEmpty(contactFormModel.Message);
			ViewData["error_phone"] = string.IsNullOrEmpty(contactFormModel.Phone);

			return CurrentUmbracoPage();
		}

		try
		{
			var response = await _httpClient.PostAsJsonAsync("https://onatrixwebapi20241004120956.azurewebsites.net/api/ClientContact", contactFormModel);

			if (response.IsSuccessStatusCode)
			{
				TempData["SuccessBig"] = "Form submitted successfully";
				return RedirectToCurrentUmbracoPage();
			}
			else
			{
				ModelState.AddModelError("", "There was a problem submitting the form.");
			}
		}
		catch (HttpRequestException ex)
		{
			ModelState.AddModelError("", $"Error submitting form: {ex.Message}");
		}
		return CurrentUmbracoPage();
	}

	public async Task<IActionResult> HandleSubmit(ContactFormModel contactFormModel)
	{
		if (!ModelState.IsValid)
		{
			ViewData["email"] = contactFormModel.Email;
			ViewData["error_email"] = string.IsNullOrEmpty(contactFormModel.Email);

			return CurrentUmbracoPage();
		}

		try
		{
			var response = await _httpClient.PostAsJsonAsync("https://onatrixwebapi20241004120956.azurewebsites.net/api/ClientContact", contactFormModel);

			if (response.IsSuccessStatusCode)
			{
				TempData["Success"] = "Form submitted successfully";
				return RedirectToCurrentUmbracoPage();
			}
			else
			{
				ModelState.AddModelError("", "There was a problem submitting the form.");
			}
		}
		catch (HttpRequestException ex)
		{
			ModelState.AddModelError("", $"Error submitting form: {ex.Message}");
		}
		return CurrentUmbracoPage();
	}
}
