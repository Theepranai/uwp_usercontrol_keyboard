using keyboard_test.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace keyboard_test
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        public MainPage()
        {
            this.InitializeComponent();
            this.DataContext =  App.Current.Services.GetService(typeof(MainPageViewModel)) as MainPageViewModel;

            Text1.AddHandler(TappedEvent, new TappedEventHandler(TextBox_Tapped), true);
            Text2.AddHandler(TappedEvent, new TappedEventHandler(TextBox_Tapped), true);
            Text3.AddHandler(TappedEvent, new TappedEventHandler(TextBox_Tapped), true);
            Text4.AddHandler(TappedEvent, new TappedEventHandler(TextBox_Tapped), true);
        }

        private void TextBox_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var x = sender as TextBox;
            (this.DataContext as MainPageViewModel).TextBoxValue = sender;
        }

    }
}
