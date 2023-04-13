﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Models
{
	public class OrderHeader
	{
		public int Id { get; set; }

		public string ApplicationUserId { get; set; }

		[ForeignKey(nameof(ApplicationUserId))]
		[ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }

		[Required]
		public DateTime OrderDate { get; set; }
        public DateTime ShippingDate { get; set; }
        public double OrderTotal { get; set; }
        public string? OrderStatus { get; set; }
        public string? PaymentStatus { get; set; }
        public string? TrackingNumber { get; set; }
		public string? Carrier { get; set; }
        public DateTime PaymentDueDate { get; set; }
        public DateTime PaymentDate { get; set; }
        public string? SessionId { get; set; }
        public string? PaymentIntentId { get; set; }

		[Required]
		public string Name { get; set; }
		
		[Required]
        [Phone]
		[Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
		[Display(Name = "Street Address")]
		public string StreetAddress { get; set; }

		[Required]
		public string City { get; set; }

		[Required]
		public string Province { get; set; }

		[Required]
		[Display(Name = "Postal Code")]
		public string PostalCode { get; set; }
	}
}