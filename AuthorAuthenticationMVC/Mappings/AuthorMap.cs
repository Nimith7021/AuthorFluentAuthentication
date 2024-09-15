using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AuthorAuthenticationMVC.Models;
using FluentNHibernate.Mapping;

namespace AuthorAuthenticationMVC.Mappings
{
    public class AuthorMap:ClassMap<Author>
    {
        public AuthorMap() {

            Table("Authors");
            Id(a => a.Id).GeneratedBy.GuidComb();
            Map(a => a.Name);
            Map(a => a.Email);
            Map(a => a.Age);
            Map(a => a.Password);
            HasOne(s=>s.AuthorDetails).PropertyRef(ad=>ad.Author).Cascade.All();
            HasMany(e => e.Books).Inverse().Cascade.All();
        
        }
    }
}