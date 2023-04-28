using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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
        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }


        [Display(Name = "Shipping Date")]
        public DateTime ShippingDate { get; set; }


        [Display(Name = "Order Total")]
        public double OrderTotal { get; set; }


        [Display(Name = "Order Status")]
        public string? OrderStatus { get; set; }


        [Display(Name = "Payment Status")]
        public string? PaymentStatus { get; set; }


        [Display(Name = "Tracking")]
        public string? TrackingNumber { get; set; }

        public string? Carrier { get; set; }


        [Display(Name = "Payment Due Date")]
        public DateTime PaymentDueDate { get; set; }


        [Display(Name = "Payment Date")]
        public DateTime PaymentDate { get; set; }


        [Display(Name = "Session Id")]
        public string? SessionId { get; set; }


        [Display(Name = "Payment Intent Id")]
        public string? PaymentIntentId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone")]
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
