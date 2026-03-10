using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp5
{
    internal class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        int count = 0;

        public int Count
        {
            get { return count;
            }
        set
            {
                count = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Count"));
            }
        
        }


        public ButtonCommand Click
        {
            get
            {
                return new ButtonCommand((param) =>
                {
                    Count += Convert.ToInt32(param.ToString());
                }, (param) =>
                {
                    return
                    param.ToString()[0]=='1' ||
                    Convert.ToInt32(param.ToString()) * 10
                    <= count;
                }
                
                
                );
            }
        }
    }
}
