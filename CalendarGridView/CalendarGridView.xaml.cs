using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace CalendarGridView
{
    public sealed partial class CalendarGridView : UserControl
    {
        double _maxItemWidth = 300;

        public CalendarGridView()
        {
            this.InitializeComponent();
            this.Loaded += CalendarGridView_Loaded;

            calendarView.Loaded += CalendarView_Loaded;
            calendarView.SizeChanged += CalendarView_SizeChanged;
        }

        private void CalendarView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ChangeItemSize(e.NewSize.Width);
        }

        private void CalendarView_Loaded(object sender, RoutedEventArgs e)
        {
            ChangeItemSize(calendarView.ActualWidth);
        }


        private void CalendarGridView_Loaded(object sender, RoutedEventArgs e)
        {
            var scroller = calendarView.GetFirstDescendantOfType<ScrollViewer>();
            scroller.ChangeView(null, scroller.ExtentHeight / 2, null);
        }

        private void ChangeItemSize(double panelWidth)
        {
            _maxItemWidth = Math.Floor((panelWidth - ((ItemsWrapGrid)calendarView.ItemsPanelRoot).Margin.Left - ((ItemsWrapGrid)calendarView.ItemsPanelRoot).Margin.Right) / ColumnCount);
            ((ItemsWrapGrid)calendarView.ItemsPanelRoot).ItemWidth = _maxItemWidth;
            ((ItemsWrapGrid)calendarView.ItemsPanelRoot).ItemHeight = _maxItemWidth;
        }


        public object ItemsSource
        {
            get { return (object)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemsSource.  This enables animation, styling, binding, etc...
        public static DependencyProperty ItemsSourceProperty { get; } =
            DependencyProperty.Register("ItemsSource", typeof(object), typeof(CalendarGridView), new PropertyMetadata(default));



        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemTemplate.  This enables animation, styling, binding, etc...
        public static DependencyProperty ItemTemplateProperty {get;} =
            DependencyProperty.Register("ItemTemplate", typeof(DataTemplate), typeof(CalendarGridView), new PropertyMetadata(default));




        public int ColumnCount
        {
            get { return (int)GetValue(ColumnCountProperty); }
            set { SetValue(ColumnCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ColumnCount.  This enables animation, styling, binding, etc...
        public static DependencyProperty ColumnCountProperty { get; } =
            DependencyProperty.Register("ColumnCount", typeof(int), typeof(CalendarGridView), new PropertyMetadata(7));




    }
}
