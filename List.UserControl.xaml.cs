using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TestProject.Models;
using TestProject.ViewModels;

namespace TestProject.Controls
{



    public partial class List : UserControl
    {
        public List()
        {
            InitializeComponent();

            var viewModel = DependencyInjector.Instance.Resolve<MainWindowViewModel>();       
            this.DataContext = viewModel;
        }

        //public object Listed
        //{
        //    get { return (object)GetValue(ListedProperty); }
        //    set { SetValue(ListedProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for Listed.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty ListedProperty =
        //    DependencyProperty.Register("Listed", typeof(object), typeof(List), new PropertyMetadata(0));
    }

    //public static readonly DependencyProperty GetPeopleProperty =
    //DependencyProperty.Register("GetPeople", typeof(Person), typeof(string));
}
