﻿using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;

namespace PeopleApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp() =>
        MauiApp.CreateBuilder()
            .UseMauiApp<App>()
            .Build();
}