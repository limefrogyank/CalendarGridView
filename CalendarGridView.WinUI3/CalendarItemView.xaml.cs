using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;


// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace McPhersonApps
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
