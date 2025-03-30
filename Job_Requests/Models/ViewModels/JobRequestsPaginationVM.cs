namespace Job_Requests.Models.ViewModels
{
	public class JobRequestsPaginationVM
	{

		public List<JobRequest> JobRequests { get; set; }
		public int CurrentPage { get; set; }
		public int TotalPages { get; set; }
		public int PageSize { get; set; }
		public int RecordCount { get; set; }
		public int TotalRequests { get; set; }

	}
}
