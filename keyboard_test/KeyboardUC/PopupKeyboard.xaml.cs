using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace keyboard_test.KeyboardUC
{
    public sealed partial class PopupKeyboard : UserControl
    {
        private bool isShiftKey = false;

        public object SelectedItem
        {
            get => GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register(
                nameof(SelectedItem),
                typeof(object),
                typeof(PopupKeyboard),
                new PropertyMetadata(null, OnSelectItemCallBack));

        private static void OnSelectItemCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var position = e.NewValue as FrameworkElement;
            var popup = d as PopupKeyboard;
            popup.PopupPanel.IsOpen = true;

            var bounds = Window.Current.Bounds;
            double height = bounds.Height;
            double width = bounds.Width;
            popup.PopupPanel.HorizontalOffset = (width / 2) - 415;//position.ActualOffset.X;
            popup.PopupPanel.VerticalOffset = position.ActualOffset.Y - 300;
        }

        private string[] enKey = { "qwertyuiop", "asdfghjkl", "zxcvbnm," };
        private string[] enKeyShift = { "QWERTYUIOP", "ASDFGHJKL", "ZXCVBNM," };

        public PopupKeyboard()
        {
            this.InitializeComponent();

            LoadKey();

            // add button event
			// button tapp
            ListView1.AddHandler(TappedEvent, new TappedEventHandler(ListView1_ItemClick), true);
            ListView2.AddHandler(TappedEvent, new TappedEventHandler(ListView1_ItemClick), true);
            ListView3.AddHandler(TappedEvent, new TappedEventHandler(ListView1_ItemClick), true);
			
            CloseBtn.AddHandler(TappedEvent, new TappedEventHandler(OnCloseBtn), true);
            ShiftBtn.AddHandler(TappedEvent, new TappedEventHandler(OnShiftBtn), true);
            SpaceBarBtn.AddHandler(TappedEvent, new TappedEventHandler(OnSpaceBarBtn), true);
            EnterBtn.AddHandler(TappedEvent, new TappedEventHandler(OnEnterBtn), true);
            DeleteBtn.AddHandler(TappedEvent, new TappedEventHandler(OnDeleteBtn), true);
        }

        private void LoadKey()
        {
            char[] row1;
            char[] row2;
            char[] row3;

            isShiftKey = !isShiftKey;

            if (isShiftKey)
            {
                row1 = enKeyShift[0].ToCharArray();
                row2 = enKeyShift[1].ToCharArray();
                row3 = enKeyShift[2].ToCharArray();
            }
            else
            {
                row1 = enKey[0].ToCharArray();
                row2 = enKey[1].ToCharArray();
                row3 = enKey[2].ToCharArray();
            }

            // Data bind the list view with array items
            ListView1.ItemsSource = row1;
            ListView2.ItemsSource = row2;
            ListView3.ItemsSource = row3;


        }

        private void ListView1_ItemClick(object sender, TappedRoutedEventArgs e)
        {
            if (!(SelectedItem is TextBox)) return;

            ListBox lb = (ListBox)sender;

            if (lb?.SelectedItem != null)
            {
                var x = SelectedItem as TextBox;
                x.Text = x.Text + lb.SelectedItem.ToString();
                x.Focus(FocusState.Pointer);
                x.Select(x.Text.Length, 0);
            }

        }

        private void OnCloseBtn(object sender, TappedRoutedEventArgs e)
        {
            PopupPanel.IsOpen = false;
        }

        private void OnDeleteBtn(object sender, TappedRoutedEventArgs e)
        {
            if (!(SelectedItem is TextBox)) return;

            var x = SelectedItem as TextBox;

            if (!string.IsNullOrWhiteSpace(x?.Text))
            {
                var s = x.Text;
                x.Text = s.Remove(s.Length - 1);
                x.Focus(FocusState.Pointer);
                x.Select(s.Length, 0);
            }
        }

        private void OnShiftBtn(object sender, TappedRoutedEventArgs e)
        {
            if (!(SelectedItem is TextBox)) return;

            LoadKey();
        }

        private void OnEnterBtn(object sender, TappedRoutedEventArgs e)
        {
            FocusManager.TryMoveFocus(FocusNavigationDirection.Next);
            PopupPanel.IsOpen = false;
        }

        private void OnSpaceBarBtn(object sender, TappedRoutedEventArgs e)
        {
            if (!(SelectedItem is TextBox)) return;

            var x = SelectedItem as TextBox;

            if (!string.IsNullOrWhiteSpace(x?.Text))
            {
                var s = x.Text;
                x.Text = x.Text + " ";
                x.Focus(FocusState.Pointer);
                x.Select(s.Length, 0);
            }
        }
    }
}
