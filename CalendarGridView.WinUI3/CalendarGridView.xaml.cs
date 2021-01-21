using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using System;


// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace McPhersonApps
{
    public sealed partial class CalendarGridView : UserControl
    {
        double _maxItemWidth = 300;
        private ScrollViewer scroller;

        public CalendarGridView()
        {
            this.InitializeComponent();
            this.Loaded += CalendarGridView_Loaded;

            calendarView.Loaded += CalendarView_Loaded;
            calendarView.SizeChanged += CalendarView_SizeChanged;
            calendarView.ContainerContentChanging += CalendarView_ContainerContentChanging;
        }

        private void CalendarView_ContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {

            if (AlternateMonthColors)
            {
                var calendarItem = args.Item as ICalendarItem;
                if (calendarItem != null)
                {
                    if (calendarItem.Date.Month % 2 == 0)
                    {
                        args.ItemContainer.Background = (Brush)Application.Current.Resources["CalendarViewCalendarItemBackground"];
                    }
                    else
                    {
                        args.ItemContainer.Background = (Brush)Application.Current.Resources["CalendarViewOutOfScopeBackground"];
                        //args.ItemContainer.Background = new SolidColorBrush(Colors.LightPink);
                    }
                }
            }

            //args.Handled = true;
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
            scroller = calendarView.GetFirstDescendantOfType<ScrollViewer>();
            scroller.ViewChanging += Scroller_ViewChanging;
            scroller.ChangeView(null, scroller.ExtentHeight / 2, null);

        }

        private void Scroller_ViewChanging(object sender, ScrollViewerViewChangingEventArgs e)
        {
            var verticalOffset = e.NextView.VerticalOffset;
            var itemHeight = ((ItemsWrapGrid)calendarView.ItemsPanelRoot).ItemHeight;
            var rowAtTop = (int)Math.Ceiling(verticalOffset / itemHeight);
            var itemIndexAtTop = rowAtTop * 7;
            var container = calendarView.ContainerFromIndex(itemIndexAtTop);
            if (container == null)
                return;
            var item = calendarView.ItemFromContainer(container);
            var calendarItem = item as ICalendarItem;
            if (calendarItem != null)
            {
                var nextMonth = new DateTime(calendarItem.Date.Month == 12 ? calendarItem.Date.Year + 1 : calendarItem.Date.Year, calendarItem.Date.Month == 12 ? 1 : calendarItem.Date.Month + 1, 1);
                if ((nextMonth - calendarItem.Date).TotalDays < 7)
                {
                    MonthInView = nextMonth.Month;
                }
                else
                {
                    MonthInView = calendarItem.Date.Month;
                }
            }
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
        public static DependencyProperty ItemTemplateProperty { get; } =
            DependencyProperty.Register("ItemTemplate", typeof(DataTemplate), typeof(CalendarGridView), new PropertyMetadata(default));




        public int ColumnCount
        {
            get { return (int)GetValue(ColumnCountProperty); }
            set { SetValue(ColumnCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ColumnCount.  This enables animation, styling, binding, etc...
        public static DependencyProperty ColumnCountProperty { get; } =
            DependencyProperty.Register("ColumnCount", typeof(int), typeof(CalendarGridView), new PropertyMetadata(7));




        public bool AlternateMonthColors
        {
            get { return (bool)GetValue(AlternateMonthColorsProperty); }
            set { SetValue(AlternateMonthColorsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AlternateMonthColors.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AlternateMonthColorsProperty =
            DependencyProperty.Register("AlternateMonthColors", typeof(bool), typeof(CalendarGridView), new PropertyMetadata(default));




        public int MonthInView
        {
            get { return (int)GetValue(MonthInViewProperty); }
            private set { SetValue(MonthInViewProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MonthInView.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MonthInViewProperty =
            DependencyProperty.Register("MonthInView", typeof(int), typeof(CalendarGridView), new PropertyMetadata(0));



        private void todayButton_Click(object sender, RoutedEventArgs e)
        {
            scroller.ChangeView(null, scroller.ExtentHeight / 2, null);
        }
    }
}
