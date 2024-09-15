using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using AuthorAuthenticationMVC.Models;

namespace AuthorAuthenticationMVC.Models
{
    public class Author
    {
        public virtual Guid Id { get; set; }

        public virtual string Name { get; set; }

        public virtual int Age { get; set; }
        public virtual string Email { get; set; }

        [DataType(DataType.Password)]
        public virtual string Password { get; set; }
       

        public virtual IList<Books> Books { get; set; } = new List<Books>();

        public virtual AuthorDetails AuthorDetails { get; set; } = new AuthorDetails();


    }
}