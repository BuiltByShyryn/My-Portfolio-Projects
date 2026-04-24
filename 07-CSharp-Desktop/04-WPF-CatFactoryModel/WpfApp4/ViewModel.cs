using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp4
{
    internal class ViewModel :INotifyPropertyChanged

    {
        public event PropertyChangedEventHandler PropertyChanged;
        CatFactory catFactory;
        List<Cat> catList;

        Cat selectedCat;
        public Cat SelectedCat
        {
            get { return selectedCat; }
            set
            {
                selectedCat = value;
                Notify("SelectedCat");
            }
        }
        public List<Cat> Cats
        {
            get { return catList; }
            set
            {
                catList = value;
                Notify("Cats");
            }
        }
        public ViewModel()
        {
            catFactory = new CatFactory();
            catList = new List<Cat>(catFactory.catList);



      }

        private void Notify(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));



        }
    }
}
