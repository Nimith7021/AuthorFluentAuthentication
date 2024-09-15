using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AuthorAuthenticationMVC.Models;
using FluentNHibernate.Mapping;

namespace AuthorAuthenticationMVC.Mappings
{
    public class AuthorDetailsMap:ClassMap<AuthorDetails>
    {
        public AuthorDetailsMap() {

            Table("AuthorDetails");
            Id(ad => ad.Id).GeneratedBy.Identity();
            Map(ad => ad.Street);
            Map(ad => ad.City);
            Map(ad => ad.State);
            References(s=>s.Author).Column("AuthorId").Cascade.None();
        }
    }
}