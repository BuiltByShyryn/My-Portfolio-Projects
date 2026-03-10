using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp4
{
    internal class CatFactory
    {
        public List<Cat> catList { get; private set; }
        public CatFactory() {
            catList = new List<Cat>();
            catList.Add(new Cat("Balck", 12, "https://peninsulavet.com.au/wp-content/uploads/2020/10/CAT-CHAT-3-1200x800.jpg"));
            catList.Add(new Cat("Blue", 13, "https://peninsulavet.com.au/wp-content/uploads/2020/10/CAT-CHAT-3-1200x800.jpg"));
            catList.Add(new Cat("Green", 14, "https://peninsulavet.com.au/wp-content/uploads/2020/10/CAT-CHAT-3-1200x800.jpg"));
            catList.Add(new Cat("Red", 5, "https://peninsulavet.com.au/wp-content/uploads/2020/10/CAT-CHAT-3-1200x800.jpg"));
            catList.Add(new Cat("White", 2, "https://peninsulavet.com.au/wp-content/uploads/2020/10/CAT-CHAT-3-1200x800.jpg"));
        }
    }
}
