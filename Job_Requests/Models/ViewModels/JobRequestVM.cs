using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Job_Requests.Models.ViewModels
{
    public class JobRequestVM
    {
        public JobRequest JobRequests { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> Departments { get; set; }
        [ValidateNever]
		public IEnumerable<SelectListItem> ReceivingDepartments { get; set; }
		[ValidateNever]
		public IEnumerable<SelectListItem> JobTypes { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> JobPriorityLevels { get; set; }

    }
}
