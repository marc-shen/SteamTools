using DynamicData;
using System.Application.Services;
using System.Application.UI.ViewModels;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace System.Application.Settings
{
    public sealed class UISettings : Abstractions.UISettings<UISettings>
    {
        static UISettings()
        {
            //Theme.ValueChanged += Theme_ValueChanged;
            AppGridSize.ValueChanged += AppGridSize_ValueChanged;
            EnableDesktopBackground.ValueChanged += EnableDesktopBackground_ValueChanged;
        }

        private static void EnableDesktopBackground_ValueChanged(object? sender, ValueChangedEventArgs<bool> e)
        {
            if (OperatingSystem2.IsWindows)
            {
                if (e.NewValue)
                {
                    IDesktopApplication.Instance.SetDesktopBackgroundWindow();
                }
                else
                {
                    DI.Get<ISystemWindowApiService>().ResetWallerpaper();
                }
            }
        }

        private static void AppGridSize_ValueChanged(object? sender, ValueChangedEventArgs<int> e)
        {
            SteamConnectService.Current.SteamApps.Refresh();
        }

        //private static void Theme_ValueChanged(object sender, ValueChangedEventArgs<short> e)
        //{
        //    if (e.NewValue != e.OldValue)
        //    {
        //        var value = (AppTheme)e.NewValue;
        //        if (value.IsDefined())
        //        {
        //            AppHelper.Current.Theme = value;
        //        }
        //    }
        //}

        /// <summary>
        /// 字体
        /// </summary>
        public static SerializableProperty<string> FontName { get; }
            = GetProperty(defaultValue: "Default", autoSave: true);

        /// <summary>
        /// 窗口毛玻璃背景透明度
        /// </summary>
        public static SerializableProperty<double> AcrylicOpacity { get; }
            = GetProperty(defaultValue: .8, autoSave: true);

        /// <summary>
        /// 库存游戏封面大小
        /// </summary>
        public static SerializableProperty<int> AppGridSize { get; }
            = GetProperty(defaultValue: 150, autoSave: true);

        /// <summary>
        /// Steam账号备注替换名称显示
        /// </summary>
        public static SerializableProperty<bool> SteamAccountRemarkReplaceName { get; }
            = GetProperty(defaultValue: false, autoSave: true);

        /// <summary>
        /// 启用圆角界面
        /// </summary>
        public static SerializableProperty<bool> EnableFilletUI { get; }
            = GetProperty(defaultValue: false, autoSave: true);

        /// <summary>
        /// 启用动态桌面背景
        /// </summary>
        public static SerializableProperty<bool> EnableDesktopBackground { get; }
            = GetProperty(defaultValue: false, autoSave: true);

        /// <summary>
        /// 启用自定义背景图片
        /// </summary>
        public static SerializableProperty<bool> EnableCustomBackgroundImage { get; }
            = GetProperty(defaultValue: false, autoSave: true);

        /// <summary>
        /// 背景图片路径
        /// </summary>
        public static SerializableProperty<string> BackgroundImagePath { get; }
            = GetProperty(defaultValue: "avares://System.Application.SteamTools.Client.Desktop.Avalonia/Application/UI/Assets/AppResources/Placeholders/0.png", autoSave: true);

        /// <summary>
        /// 主题颜色(十六进制字符串)
        /// </summary>
        public static SerializableProperty<string> ThemeAccent { get; }
            = GetProperty(defaultValue: "#FF0078D7", autoSave: true);

        /// <summary>
        /// 主题颜色从系统获取
        /// </summary>
        public static SerializableProperty<bool> GetUserThemeAccent { get; }
            = GetProperty(defaultValue: true, autoSave: true);
    }
}