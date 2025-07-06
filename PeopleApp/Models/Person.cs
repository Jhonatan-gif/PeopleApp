using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace PeopleApp.Models
{
    public class Person
    {
        [PrimaryKey, AutoIncrement] 
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
