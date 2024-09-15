using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AuthorAuthenticationMVC.Models;
using FluentNHibernate.Mapping;

namespace AuthorAuthenticationMVC.Mappings
{
    public class BookMap:ClassMap<Books>
    {
        public BookMap() {

            Table("Books");
            Id(b => b.Id).GeneratedBy.Identity();
            Map(b => b.Name);
            Map(b => b.Genre);
            Map(b => b.Description);
            References(o => o.Author).Column("AuthorId").Cascade.None().Nullable();
        
        }    
    }
}