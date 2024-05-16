// Copyright © 2024 Olaf Meyer
// Copyright © 2024 scotec Software Solutions AB, www.scotec-software.com
// This file is licensed to you under the MIT license.

using System;
using System.Reflection;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Autodesk.Revit.UI;
using Autofac;
using Microsoft.Extensions.Hosting;
using Revit.Tutorial.Commands;
using Revit.Tutorial.Modules;
using Revit.Tutorial.Resources;
using Scotec.Revit;

namespace Revit.Tutorial;

public class RevitInfoApp : RevitApp
{
    protected override Result OnStartup()
    {
        // OnStartUp() for this add-in is different to the OnStartUp() of all other add-ins.
        // The info add-in must always be loaded, event if the version is wrong. Otherwise we can not open the info dialog.
        // Therefore we don't check the return values of base.OnStartUp() and CanLoadAddIn().
        try
        {
            //var bundleValidator = Services.GetService<YourService>();

            if (!TabManager.HasTab(StringResources.Tab_Name))
            {
                TabManager.CreateTab(Application, StringResources.Tab_Name);
            }

            var panel = TabManager.HasPanel(Application, StringResources.Panel_Name, StringResources.Tab_Name)
                ? TabManager.GetPanel(Application, StringResources.Panel_Name, StringResources.Tab_Name)
                : TabManager.CreatePanel(Application, StringResources.Panel_Name, StringResources.Tab_Name);

            var button = (PushButton)panel.AddItem(CreateButtonData("Scotec.Revit.Tutorial",
                StringResources.Command_Info_Text, StringResources.Command_Info_Description,
                typeof(ShowInfoCommand)));
            button.Enabled = true;
        }
        catch (Exception)
        {
            return Result.Failed;
        }

        return Result.Succeeded;
    }

    protected override void OnConfigure(IHostBuilder builder)
    {
        base.OnConfigure(builder);

        builder.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder
                                                                         .RegisterModule<ViewModelModule>()
                                                                         .RegisterModule<ViewModule>());
    }

    protected override Result OnShutdown()
    {
        return Result.Succeeded;
    }

    private static PushButtonData CreateButtonData(string name, string text, string description, Type commandType)
    {
        return new PushButtonData(name, text,
            Assembly.GetExecutingAssembly().Location, commandType.FullName)
        {
            Image = CreateImageSource("Information_16.png"),
            LargeImage = CreateImageSource("Information_32.png"),
            ToolTip = description,
            AvailabilityClassName = typeof(ShowInfoCommandAvailability).FullName
        };
    }

    private static ImageSource CreateImageSource(string image)
    {
        var resourcePath = $"BI.Revit.Info.Resources.Images.{image}";

        var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath);
        if (stream == null)
        {
            return null;
        }

        var decoder = new PngBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);

        return decoder.Frames[0];
    }
}
