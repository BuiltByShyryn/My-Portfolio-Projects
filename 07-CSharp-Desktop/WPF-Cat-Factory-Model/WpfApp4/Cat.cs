using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp4
{
    internal class Cat
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Photo { get; set; }
        public Cat(string name, int age, string photo ) {
            Name = name;
            Age = age;
            Photo = photo;
        }

        public override string ToString()
        {
            return Name + " " + Age + " years";
        }
    }
}
