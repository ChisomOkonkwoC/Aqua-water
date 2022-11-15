using System;
using System.ComponentModel.DataAnnotations;
using VestEngine.Application.Utilities;

namespace VestEngine.Application.Dtos.Request
{
    public class NewCustomerRequestDto
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }
		[Required]
		[DataType(DataType.Text)]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
		[RegularExpression(@"^[A-Z]{1}[a-z]{2,}$", ErrorMessage = "FirstName should begin with a capital letter, followed by small letters")]
		public string FirstName { get; set; }

		[Required]
		[DataType(DataType.Text)]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
		[RegularExpression(@"^[A-Z]{1}[a-z]{2,}$", ErrorMessage = "LastName should begin with a capital letter, followed by small letters")]
		public string LastName { get; set; }
		[Required]
		[Range(1, 2, ErrorMessage = "Value for gender must be between 1 for male and 2 for female")]
		public int Gender { get; set; }
		[Required]
		[MinLength(11)]
		[RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$", ErrorMessage = "Please enter valid phone number.")]
		public string PhoneNumber { get; set; }

		[Required]
		[DataType(DataType.Text)]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
		[RegularExpression(@"^[a-zA-Z0-9, ]{0,}$", ErrorMessage = "Address should contain only letters and should start with a capital letter")]
		public string Address { get; set; }

		[Required]
		public DateTime DateOfBirth { get; set; }
		[Required]
		public Guid SupportedCountryId { get; set; }

		[Required]
		[DataType(DataType.Text)]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
		[RegularExpression(@"^[A-Z]{1}[a-zA-Z ]{3,}$", ErrorMessage = "Town should contain only letters and should start with a capital letter")]
		public string Town { get; set; }

		//Employement Details
		[Required]
		[DataType(DataType.Text)]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
		[RegularExpression(@"^[A-Z]{1}[a-zA-Z ]{3,}$", ErrorMessage = "Company Of Employement should contain only letters and should start with a capital letter")]
		public string CompanyOfEmployement { get; set; }

		[Required]
		[DataType(DataType.Text)]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
		[RegularExpression(@"^[A-Z]{1}[a-zA-Z0-9 ]{3,}$", ErrorMessage = "Position should contain only letters and should start with a capital letter")]
		public string Position { get; set; }
		[Required]
		[RegularExpression(@"^[a-zA-Z0-9{3,15}$", ErrorMessage = "StaffId must be between 3 to 15 digits")]
		public string StaffId { get; set; }
		[Range(Double.MinValue, Double.MaxValue)]
		public decimal GrossMonthlySalary { get; set; }
		[Range(Double.MinValue, Double.MaxValue)]
		public decimal NetMonthlySalary { get; set; }
		[Required]
		[Range(1,31, ErrorMessage="Date Of SalaryPayment must be between 1 to 31")]
		public int DateOfSalaryPayment { get; set; }
		[Required]
		public DateTime DateOfEmployment { get; set; }
		[Required]
		[EmailAddress]
		public string HrEmailAddress { get; set; }

		[DataType(DataType.Text)]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 0)]
		[RegularExpression(@"^[a-zA-Z0-9, ]{0,}$", ErrorMessage = "Company Address should contain only letters and should start with a capital letter")]
		public string CompanyAddress { get; set; }


		//Reference One
		[DataType(DataType.Text)]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 0)]
		[RegularExpression(@"^[A-Z]{0,1}[a-z]{0,}$", ErrorMessage = "Reference one firstname should begin with a capital letter, followed by small letters")]
		public string ReferenceOneFristName { get; set; }
		[DataType(DataType.Text)]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 0)]
		[RegularExpression(@"^[A-Z]{0,1}[a-z]{0,}$", ErrorMessage = "Reference one middlename begin with a capital letter, followed by small letters")]
		public string ReferenceOneMiddleName { get; set; }
		[DataType(DataType.Text)]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 0)]
		[RegularExpression(@"^[A-Z]{0,1}[a-z]{0,}$", ErrorMessage = "Reference one lastname should begin with a capital letter, followed by small letters")]
		public string ReferenceOneLastName { get; set; }

		[EmailValidation(ErrorMessage = "Invalid email format")]
		public string ReferenceOneEmail { get; set; }

		[RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$", ErrorMessage = "Please enter valid phone number.")]
		public string ReferenceOnePhoneNumber { get; set; }

		[DataType(DataType.Text)]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 0)]
		[RegularExpression(@"^[a-zA-Z0-9, ]{0,}$", ErrorMessage = "Reference one Address should contain only letters and should start with a capital letter")]
		public string ReferenceOneAddress { get; set; }

		[Range(0, 2, ErrorMessage = "Value for gender must be between 1 for male and 2 for female")]
		public int ReferenceOneGender { get; set; }
		[RegularExpression(@"^[0-9a-zA-z]{10,15}$", ErrorMessage = "Reference one staff Id must be between 10 to 15 digits")]
		public string ReferenceOneStaffID { get; set; }

		//Reference Two
		[DataType(DataType.Text)]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 0)]
		[RegularExpression(@"^[A-Z]{0,1}[a-z]{0,}$", ErrorMessage = "Reference two firstname should begin with a capital letter, followed by small letters")]
		public string ReferenceTwoFristName { get; set; }
		[DataType(DataType.Text)]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 0)]
		[RegularExpression(@"^[A-Z]{0,1}[a-z]{0,}$", ErrorMessage = "Reference two middlename should begin with a capital letter, followed by small letters")]
		public string ReferenceTwoMiddleName { get; set; }
		[DataType(DataType.Text)]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 0)]
		[RegularExpression(@"^[A-Z]{0,1}[a-z]{0,}$", ErrorMessage = "Reference one lastname should begin with a capital letter, followed by small letters")]
		public string ReferenceTwoLastName { get; set; }
		[EmailValidation(ErrorMessage = "Invalid email format")]
		public string ReferenceTwoEmail { get; set; }
		[RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$", ErrorMessage = "Please enter valid phone number.")]
		public string ReferenceTwoPhoneNumber { get; set; }
		[DataType(DataType.Text)]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 0)]
		[RegularExpression(@"^[a-zA-Z0-9 ]{0,}$", ErrorMessage = "Reference two address should contain only letters and should start with a capital letter")]
		public string ReferenceTwoAddress { get; set; }
		[Range(0, 2, ErrorMessage = "Value for gender must be between 1 for male and 2 for female")]
		public int ReferenceTwoGender { get; set; }
		[RegularExpression(@"^[0-9a-zA-Z]{10,15}$", ErrorMessage = "Reference two staffId must be between 10 to 15 digits")]
		public string ReferenceTwoStaffID { get; set; }

		//Next Of Kin
		[Required]
		[DataType(DataType.Text)]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
		[RegularExpression(@"^[A-Z]{1}[a-z]{2,}$", ErrorMessage = "Next of kin firstname should begin with a capital letter, followed by small letters")]
		public string NextOfKinFristName { get; set; }

		[DataType(DataType.Text)]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 0)]
		[RegularExpression(@"^[A-Z]{0,1}[a-z]{1,}$", ErrorMessage = "Next of kin middlename should begin with a capital letter, followed by small letters")]
		public string NextOfKinMiddleName { get; set; }

		[Required]
		[DataType(DataType.Text)]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
		[RegularExpression(@"^[A-Z]{1}[a-z]{2,}$", ErrorMessage = "Next of kin lastname should begin with a capital letter, followed by small letters")]
		public string NextOfKinLastName { get; set; }

		[Required]
		[MinLengthAttribute(11)]
		[RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$", ErrorMessage = "Please enter valid phone number.")]
		public string NextOfKinPhoneNumber { get; set; }

		[Required]
		[DataType(DataType.Text)]
		[RegularExpression(@"^[a-zA-Z0-9, ]{0,}$", ErrorMessage = "Next of kin address should contain only letters and should start with a capital letter")]
		public string NextOfKinAddress { get; set; }

		
		[DataType(DataType.Text)]
		/*[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
		[RegularExpression(@"^[A-Z]{0,1}[a-zA-Z0-9]{0,}$", ErrorMessage = "Social media name should not cannot ")]*/
		public string SocialMedia { get; set; }
	}
}
