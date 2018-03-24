using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace umbracoInitializer.ViewModels
{
    public class ContactForm
    {
        [Required(ErrorMessage = "Please enter a name!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter an email adress!")]
        [RegularExpression("[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?",
            ErrorMessage ="Please enter a valid email!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter a subject!")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Please write a message!")]
        public string Message { get; set; }
    }
}