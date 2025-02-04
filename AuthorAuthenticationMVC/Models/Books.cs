﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AuthorAuthenticationMVC.Models;

namespace AuthorAuthenticationMVC.Models
{
    public class Books
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Genre { get; set; }

        public virtual string Description { get; set; }

        public virtual Author Author { get; set; }
    }

}