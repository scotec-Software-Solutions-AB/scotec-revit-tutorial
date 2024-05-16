// Copyright © 2024 Olaf Meyer
// Copyright © 2024 scotec Software Solutions AB, www.scotec-software.com
// This file is licensed to you under the MIT license.

using Autodesk.Revit.UI;
using Revit.Tutorial.Resources;
using Revit.Tutorial.ViewModels;
using Scotec.Revit;

namespace Revit.Tutorial.Views;

/// <summary>
///     Interaction logic for AddInInfosWindow.xaml
/// </summary>
internal partial class AddInInfoWindow : RevitWindow
{
    public AddInInfoWindow(UIApplication revitApplication, AddInInfoViewModel viewModel) : base(revitApplication)
    {
        HideMinimizeAndMaximizeButtons();
        InitializeComponent();
        Title = StringResources.Info_Title;

        viewModel.SetCloseDelegate(Close);
        DataContext = viewModel;
    }
}
