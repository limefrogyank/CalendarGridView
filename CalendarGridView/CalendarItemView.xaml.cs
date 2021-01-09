using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace CalendarGridView
{
    public sealed partial class CalendarItemView : UserControl
    {
        public CalendarItemView()
        {
            this.InitializeComponent();
        }



        public object CalendarItem
        {
            get { return (object)GetValue(CalendarItemProperty); }
            set { SetValue(CalendarItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CalendarItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CalendarItemProperty =
            DependencyProperty.Register("CalendarItem", typeof(object), typeof(CalendarItemView), new PropertyMetadata(default));




        //public string DateFormat
        //{
        //    get { return (string)GetValue(DateFormatProperty); }
        //    set { SetValue(DateFormatProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for DateFormat.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty DateFormatProperty =
        //    DependencyProperty.Register("DateFormat", typeof(string), typeof(CalendarItemView), new PropertyMetadata(null));




        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemTemplateProperty =
            DependencyProperty.Register("ItemTemplate", typeof(DataTemplate), typeof(CalendarItemView), new PropertyMetadata(default));


    }
}
