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
        public event DragEventHandler Drop;
        public event DragEventHandler DragOver;
        public event DragEventHandler DragLeave;

        public CalendarItemView()
        {
            this.InitializeComponent();
            grid.Drop += (s, e) =>
            {
                this.Drop?.Invoke(s, e);
            };

            grid.DragOver += (s, e) =>
            {
                this.DragOver?.Invoke(s, e);
            };

            grid.DragLeave += (s, e) =>
            {
                this.DragLeave?.Invoke(s, e);
            };
        }

        
        public object CalendarItem
        {
            get { return (object)GetValue(CalendarItemProperty); }
            set { SetValue(CalendarItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CalendarItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CalendarItemProperty =
            DependencyProperty.Register("CalendarItem", typeof(object), typeof(CalendarItemView), new PropertyMetadata(default));
                
        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemTemplateProperty =
            DependencyProperty.Register("ItemTemplate", typeof(DataTemplate), typeof(CalendarItemView), new PropertyMetadata(default));




        public bool AllowDrop
        {
            get { return (bool)GetValue(AllowDropProperty); }
            set { SetValue(AllowDropProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AllowDrop.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AllowDropProperty =
            DependencyProperty.Register("AllowDrop", typeof(bool), typeof(CalendarItemView), new PropertyMetadata(default));



    }
}
