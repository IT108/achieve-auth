﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace AuthServer.Models
{
	public class RegisterRequestViewModel
	{
		[Required]
		[StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
		[Display(Name = "Name")]
		public string Name { get; set; }

		[Required]
		[Display(Name = "Surname")]
		public string Surname { get; set; }

		[Required]
		[EmailAddress]
		[Display(Name = "Email")]
		public string Email { get; set; }

		[Required]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 8)]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Password { get; set; }

		[Required]
		[StringLength(100, ErrorMessage ="The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
		[Display(Name = "Domain")]
		public string Domain { get; set; }

		[Required]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
		[Display(Name = "DomainUsername")]
		public string DomainUsername { get; set; }
		
		[Required]
		[Display(Name = "Groups")]
		public string[] Groups { get; set; }

		[Required]
		[Display(Name = "Interests")]
		public string[] Interests { get; set; }
	}
}
