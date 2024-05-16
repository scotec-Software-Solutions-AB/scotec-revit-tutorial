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

[Transaction(TransactionMode.Manual)]
public class ShowInfoCommand : RevitCommand
{
    public ShowInfoCommand()
    {
        NoTransaction = true;
    }

    protected override string CommandName => StringResources.Command_Info_Name;

    protected override Result OnExecute(ExternalCommandData commandData, IServiceProvider services)
    {
        ShowAddInInfo(services);

        return Result.Succeeded;
    }

    private static void ShowAddInInfo(IServiceProvider services)
    {
        var window = services.GetService<AddInInfoWindow>();

        window.ShowDialog();
    }
}
