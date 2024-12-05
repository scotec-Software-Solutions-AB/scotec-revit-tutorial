// Copyright © 2024 Olaf Meyer
// Copyright © 2024 scotec Software Solutions AB, www.scotec-software.com
// This file is licensed to you under the MIT license.

using System;
using System.Runtime.Loader;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace Revit.Tutorial.ViewModels;

internal class TestViewModel
{
    public ICommand CloseCommand { get; private set; }

    public void SetCloseDelegate(Action closeDelegate)
    {
        CloseCommand = new RelayCommand(closeDelegate);
    }

    public string DefaultContextName => AssemblyLoadContext.Default.ToString();
    public string ContextName => AssemblyLoadContext.GetLoadContext(GetType().Assembly)?.ToString();
}
