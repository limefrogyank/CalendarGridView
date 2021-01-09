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
using CalendarGridView;
using System.Threading.Tasks;
using System.Diagnostics;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CalendarGridView.Demo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public CalendarItemList<CalendarItemViewModel> CalendarItems { get; set; }

        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            
            CalendarItems = new CalendarItemList<CalendarItemViewModel>((dateRange, c) =>
            {

                var list = new List<CalendarItemViewModel>();
                foreach (var date in dateRange)
                {
                    list.Add(new CalendarItemViewModel(date));
                }
                return Task.FromResult(list.ToArray());
            });
        }

        private void CalendarItemView_Drop(object sender, DragEventArgs e)
        {

        }

        private void TextBlock_DragStarting(UIElement sender, DragStartingEventArgs args)
        {
            args.AllowedOperations = Windows.ApplicationModel.DataTransfer.DataPackageOperation.Copy | Windows.ApplicationModel.DataTransfer.DataPackageOperation.Move;
            args.Data.SetText("TeST");
            
        }

        private void CalendarItemView_DragOver(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = Windows.ApplicationModel.DataTransfer.DataPackageOperation.Copy | Windows.ApplicationModel.DataTransfer.DataPackageOperation.Move;
        }

        private void CalendarItemView_DragLeave(object sender, DragEventArgs e)
        {
            Debug.WriteLine("Drag leave");
        }
    }
}
