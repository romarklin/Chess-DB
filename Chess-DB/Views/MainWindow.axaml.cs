using System;
using Avalonia.Controls;
using Chess_DB.ViewModels;

namespace Chess_DB.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
    }
}