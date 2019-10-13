using ExpressionGenerator;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Person> people = new List<Person>()
            {
                new Person(){Id=1,Name="ahmed",HiringDate=DateTime.Parse("2015/01/01")},
                new Person(){Id=2,Name="ALI",HiringDate=DateTime.Parse("2015/07/01")},
                new Person(){Id=3,Name="Mourad",HiringDate=DateTime.Parse("2014/08/08")},
                new Person(){Id=4,Name="Samir",HiringDate=DateTime.Parse("2017/08/01")},
                new Person(){Id=5,Name="ahmed",HiringDate=DateTime.Parse("2019/01/01")},
                new Person(){Id=6,Name="ahmed",HiringDate=DateTime.Parse("2018/01/08")},
                new Person(){Id=7,Name="ahmed",HiringDate=DateTime.Parse("2015/05/05")},
            };

            var filter = new Person() {Name="m", HiringDate = DateTime.Parse("2014/08/08") };
            var x= FilterBuilder.GetExpression<Person, Person>(filter);
            var test = people.Where(x.Compile()).ToList();
        }
    }
  
    public class Person
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public DateTime? HiringDate { get; set; }
    }   
}
