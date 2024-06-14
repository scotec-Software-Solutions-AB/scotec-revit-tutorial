// Copyright © 2024 Olaf Meyer
// Copyright © 2024 scotec Software Solutions AB, www.scotec-software.com
// This file is licensed to you under the MIT license.

using System;
using System.IO;
using System.Linq;
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

public class RevitTutorialApp : RevitApp
{
    protected override Result OnStartup()
    {
        try
        {
            // var yourService = Services.GetService<YourService>();

            // Create tabs, panels ond buttons
            TabManager.CreateTab(Application, StringResources.Tab_Name);

            var panel = TabManager.GetPanel(Application, StringResources.Panel_Name, StringResources.Tab_Name);

            var button = (PushButton)panel.AddItem(CreateButtonData("Revit.Tutorial",
                StringResources.Command_Test_Text, StringResources.Command_Test_Description,
                typeof(TestCommandFactory)));

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

        // Register services by using Autofac modules.
        builder.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule<RegistrationModule>());

        builder.ConfigureServices(services =>
        {
            // You can also register services here.
            // However, using Autofac modules gives you more flexibility.
        });

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
            //AvailabilityClassName = typeof(TestCommandAvailability).FullName
        };
    }

    private static ImageSource CreateImageSource(string image)
    {
        var resourcePath = $"Revit.Tutorial.Resources.Images.{image}";

        var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath);
        if (stream == null)
        {
            return null;
        }

        var decoder = new PngBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);

        return decoder.Frames[0];
    }
}

public class RevitTutorialAppFactory : IExternalApplication
{
    public static AddinLoadContext Context { get; }
    private IExternalApplication _instance;
    private static Assembly s_assembly;

    static RevitTutorialAppFactory()
    {
        var path = Path.GetDirectoryName(typeof(RevitTutorialAppFactory).Assembly.Location)!;

        Context = new AddinLoadContext(path);
        s_assembly = Context.LoadFromAssemblyPath(typeof(RevitTutorialAppFactory).Assembly.Location);

    }


    public RevitTutorialAppFactory()
    {
        var types = s_assembly.GetTypes();
        var t = types.First(type => type.Name == "RevitTutorialApp");
        _instance = (IExternalApplication)Activator.CreateInstance(t);
    }

    public Result OnStartup(UIControlledApplication application)
    {
        return _instance.OnStartup(application);
    }

    public Result OnShutdown(UIControlledApplication application)
    {
        return _instance.OnShutdown(application);
    }
}


