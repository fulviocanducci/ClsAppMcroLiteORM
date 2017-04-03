using CslAppMicroLiteORM.Models;
using MicroLite;
using MicroLite.Builder;
using MicroLite.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CslAppMicroLiteORM
{
    class Program
    {
        static void Main(string[] args)
        {
            Configure
                 .Extensions()
                 .WithAttributeBasedMapping();

            ISessionFactory SessionFactory = Configure
                    .Fluently()
                    .ForMsSql2012Connection("DB")
                    .CreateSessionFactory();

            ISession Session = SessionFactory.OpenSession();

            PagedResult<People> pagedPeople = Session.Paged<People>(
                new SqlQuery("SELECT * FROM People ORDER BY Name ASC"), PagingOptions.ForPage(1, 3));
        }
    }
}

//using (IConnect connect = new Connect())
            //{
            //    //People p = new People { Name = "Renato" };
            //    //connect.Insert(p);          

            //    //var c = connect.List<People>(new SqlQuery("SELECT * FROM People"));
            //    //var d = connect.Page<People>(new SqlQuery("SELECT * FROM People"), 1, 3);

            //    //var p = connect.Find<People>(4);
            //    //p.Name = "Maria Aparecida Dias Cintra";
            //    //bool result = connect.Update(p);

            //    var abc = SqlBuilder
            //        .Delete()
            //        .From(typeof(People))
            //        .Where("Id").IsNotNull()
            //        .AndWhere("Id").IsEqualTo(1)
            //        .ToSqlQuery();
            //}
