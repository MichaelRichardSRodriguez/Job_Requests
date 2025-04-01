using Microsoft.AspNetCore.Identity.UI.Services;

namespace Job_Requests.DataAccess.Services
{
	public class EmailSender : IEmailSender
	{
		public Task SendEmailAsync(string email, string subject, string htmlMessage)
		{

			//Generate your Email Sending Codes in Here


			//Temporary command
			return Task.CompletedTask;


		}
	}
}
