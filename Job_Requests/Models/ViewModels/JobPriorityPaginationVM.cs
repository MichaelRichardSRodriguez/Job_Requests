namespace Job_Requests.Models.ViewModels
{
    public class JobPriorityPaginationVM
    {

		public IEnumerable<JobPriority> JobPriorities { get; set; }
		public int CurrentPage { get; set; }
		public int TotalPages { get; set; }
		public int PageSize { get; set; }
		public int RecordCount { get; set; }
		public int TotalRequests { get; set; }

	}
}
