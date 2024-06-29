// Copyright © 2024 Olaf Meyer
// Copyright © 2024 scotec Software Solutions AB, www.scotec-software.com
// This file is licensed to you under the MIT license.

using System;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
using Microsoft.Extensions.DependencyInjection;
using Revit.Tutorial.Resources;
using Revit.Tutorial.Views;
using Scotec.Revit;

namespace Revit.Tutorial.Commands;

[RevitCommandIsolation]
[Transaction(TransactionMode.Manual)]
public class TestCommand : RevitCommand
{
    public TestCommand()
    {
        NoTransaction = true;
    }

    protected override string CommandName => StringResources.Command_Test_Name;

    protected override Result OnExecute(ExternalCommandData commandData, IServiceProvider services)
    {
        ShowTestWindow(services);

        return Result.Succeeded;
    }

    private static void ShowTestWindow(IServiceProvider services)
    {
        var window = services.GetService<TestWindow>();

        window.ShowDialog();
    }
}
