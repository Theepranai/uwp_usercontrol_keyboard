using Microsoft.Toolkit.Mvvm.Input;
using System.ComponentModel;
using System.Diagnostics;

namespace keyboard_test.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private string name;
        private object textBoxValue;

        public IRelayCommand CheckValueCommand { get; private set; }


        public MainPageViewModel()
        {
            Name = "katon";

            CheckValueCommand = new RelayCommand(() =>
            {
                Debug.Write(Name);
            });
        }

        public object TextBoxValue { get => textBoxValue; set => SetProperty(ref textBoxValue, value); }

        public string Name { get => name; set => SetProperty(ref name, value); }
    }
}
