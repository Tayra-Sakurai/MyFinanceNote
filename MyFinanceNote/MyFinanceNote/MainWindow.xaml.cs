using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using MyFinanceNote.Models;
using MyFinanceNote.ViewModels;
using System.Diagnostics;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MyFinanceNote
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private ChimpanzeeContext context;

        private TayrasViewModel? tayras;

        private TayraViewModel? tayra;

        public MainWindow()
        {
            InitializeComponent();

            context = new ChimpanzeeContext();

            tayras = new TayrasViewModel(context);
            tayra = new TayraViewModel(context);

            this.Activated += MainWindow_Activated;
            SuperList.SelectionChanged += SuperList_SelectionChanged;
        }

        private void SuperList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(SuperList.SelectedItem != null && tayra != null)
            {
                tayra.InitializeForExistingValue((Tayra)SuperList.SelectedItem);
                Debug.WriteLine(tayra);
            }
        }

        private async void MainWindow_Activated(object sender, WindowActivatedEventArgs args)
        {
            if (tayras  != null)
            {
                await tayras.LoadAsync();
            }
        }
    }
}
