using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xamarin_PCL78
{
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();

            this.btn1.Clicked += btn1_Clicked;
        }

        void btn1_Clicked(object sender, EventArgs e)
        {
            var o = new Test.Class1();
            text1.Text = o.GetData();
        }
    }
}
