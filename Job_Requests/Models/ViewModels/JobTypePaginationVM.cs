namespace Job_Requests.Models.ViewModels
{
	public class JobTypePaginationVM
	{

		public IEnumerable<JobType> JobTypes { get; set; }

		public int CurrentPage { get; set; }
		public int TotalPages { get; set; }
		public int PageSize { get; set; }
		public int RecordCount { get; set; }
		public int TotalJobTypes {  get; set; }

	}
}
