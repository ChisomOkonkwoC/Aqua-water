using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VestEngine.Application.Dtos.Request
{
	public class GenerateReportRequestDto
	{

		[DataType(DataType.Text)]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 0)]
		[RegularExpression(@"^[A-Z]{1}[a-zA-Z ]{0,}$", ErrorMessage = "State should contain only letters")]
		public string AccountName { get; set; }

		[DataType(DataType.Text)]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 0)]
		[RegularExpression(@"^[A-Z]{1}[a-zA-Z ]{0,}$", ErrorMessage = "State should contain only letters")]
		public string ProductName { get; set; }
		[Required]
		public int ProductType { get; set; }
		[Required]
		public DateTime EndDate { get; set; }
		[Required]
		public DateTime StartDate { get; set; }
	}
}
