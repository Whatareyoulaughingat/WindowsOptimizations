﻿using Microsoft.Win32;
using System.Windows;

namespace WindowsOptimizations.Core.Patches
{
    /// <summary>
    /// Various kernel patches for reducing mouse input latency.
    /// </summary>
    public class RawMouseInputPatch
    {
        /// <summary>
        /// Disables pointer acceleration completely by changing specific registry values.
        /// </summary>
        /// <returns>[<see cref="RawMouseInputPatch"/>] The same class for allowing method chaining.</returns>
        public RawMouseInputPatch DisablePointerAcceleration()
        {
            Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Mouse", "MouseSpeed", "0");
            Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Mouse", "MouseThreshold1", "0");
            Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Mouse", "MouseThreshold2", "0");

            return this;
        }

        /// <summary>
        /// Sets a 1:1 pointer precision based on the windows scaling.
        /// </summary>
        /// <returns>[<see cref="RawMouseInputPatch"/>] The same class for allowing method chaining.</returns>
        public RawMouseInputPatch SetOneToOnePointerPrecision()
        {
            int dpi = int.Parse(Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\ThemeManager", "LastLoadedDPI", 96).ToString());
            int scale = 96 / dpi;

            switch (scale.ToString("0.00%"))
            {
                case "100,00%":
                    Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Mouse", "SmoothMouseXCurve", new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xC0, 0xCC, 0x0C, 0x0, 0x0, 0x0, 0x0, 0x0, 0x80, 0x99, 0x19, 0x0, 0x0, 0x0, 0x0, 0x0, 0x40, 0x66, 0x26, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x33, 0x33, 0x0, 0x0, 0x0, 0x0, 0x0 });
                    Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Mouse", "SmoothMouseYCurve", new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x38, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x70, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xA8, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xE0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 });
                    break;

                case "125,00%":
                    Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Mouse", "SmoothMouseXCurve", new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x10, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x20, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x30, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x40, 0x0, 0x0, 0x0, 0x0, 0x0 });
                    Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Mouse", "SmoothMouseYCurve", new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x38, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x70, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xA8, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xE0, 0x0, 0x0, 0x0, 0x0, 0x0 });
                    break;

                case "150,00%":
                    Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Mouse", "SmoothMouseXCurve", new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x30, 0x33, 0x13, 0x0, 0x0, 0x0, 0x0, 0x0, 0x60, 0x66, 0x26, 0x0, 0x0, 0x0, 0x0, 0x0, 0x90, 0x99, 0x39, 0x0, 0x0, 0x0, 0x0, 0x0, 0xC0, 0xCC, 0x4C, 0x0, 0x0, 0x0, 0x0, 0x0 });
                    Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Mouse", "SmoothMouseYCurve", new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x38, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x70, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xA8, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xE0, 0x0, 0x0, 0x0, 0x0, 0x0 });
                    break;

                case "175,00%":
                    Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Mouse", "SmoothMouseXCurve", new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x60, 0x66, 0x16, 0x0, 0x0, 0x0, 0x0, 0x0, 0xC0, 0xCC, 0x2C, 0x0, 0x0, 0x0, 0x0, 0x0, 0x20, 0x33, 0x43, 0x0, 0x0, 0x0, 0x0, 0x0, 0x80, 0x99, 0x59, 0x0, 0x0, 0x0, 0x0, 0x0 });
                    Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Mouse", "SmoothMouseYCurve", new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x38, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x70, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xA8, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xE0, 0x0, 0x0, 0x0, 0x0, 0x0 });
                    break;

                case "200,00%":
                    Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Mouse", "SmoothMouseXCurve", new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x90, 0x99, 0x19, 0x0, 0x0, 0x0, 0x0, 0x0, 0x20, 0x33, 0x33, 0x0, 0x0, 0x0, 0x0, 0x0, 0xB0, 0xCC, 0x4C, 0x0, 0x0, 0x0, 0x0, 0x0, 0x40, 0x66, 0x66, 0x0, 0x0, 0x0, 0x0, 0x0 });
                    Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Mouse", "SmoothMouseYCurve", new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x38, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x70, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xA8, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xE0, 0x0, 0x0, 0x0, 0x0, 0x0 });
                    break;

                case "225,00%":
                    Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Mouse", "SmoothMouseXCurve", new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xC0, 0xCC, 0x1C, 0x0, 0x0, 0x0, 0x0, 0x0, 0x80, 0x99, 0x39, 0x0, 0x0, 0x0, 0x0, 0x0, 0x40, 0x66, 0x56, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x33, 0x73, 0x0, 0x0, 0x0, 0x0, 0x0 });
                    Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Mouse", "SmoothMouseYCurve", new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x38, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x70, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xA8, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xE0, 0x0, 0x0, 0x0, 0x0, 0x0 });
                    break;

                case "250,00%":
                    Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Mouse", "SmoothMouseXCurve", new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x20, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x40, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x60, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x80, 0x0, 0x0, 0x0, 0x0, 0x0 });
                    Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Mouse", "SmoothMouseYCurve", new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x38, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x70, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xA8, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xE0, 0x0, 0x0, 0x0, 0x0, 0x0 });
                    break;

                case "300,00%":
                    Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Mouse", "SmoothMouseXCurve", new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x60, 0x66, 0x26, 0x0, 0x0, 0x0, 0x0, 0x0, 0xC0, 0xCC, 0x4C, 0x0, 0x0, 0x0, 0x0, 0x0, 0x20, 0x33, 0x73, 0x0, 0x0, 0x0, 0x0, 0x0, 0x80, 0x99, 0x99, 0x0, 0x0, 0x0, 0x0, 0x0 });
                    Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Mouse", "SmoothMouseYCurve", new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x38, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x70, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xA8, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xE0, 0x0, 0x0, 0x0, 0x0, 0x0 });
                    break;

                case "350,00%":
                    Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Mouse", "SmoothMouseXCurve", new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xC0, 0xCC, 0x2C, 0x0, 0x0, 0x0, 0x0, 0x0, 0x80, 0x99, 0x59, 0x0, 0x0, 0x0, 0x0, 0x0, 0x40, 0x66, 0x86, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x33, 0xB3, 0x0, 0x0, 0x0, 0x0, 0x0 });
                    Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Mouse", "SmoothMouseYCurve", new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x38, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x70, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xA8, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xE0, 0x0, 0x0, 0x0, 0x0, 0x0 });
                    break;

                default:
                    MessageBox.Show("Your Windows scaling setting is either way too high or too low or invalid.", "1:1 pointer precision applier", MessageBoxButton.OK);
                    break;
            }

            return this;
        }

        /// <summary>
        /// Changes the pointer sensitivity to its default setting.
        /// <para>The default value in the kernel is 10.</para>
        /// </summary>
        /// <returns>[<see cref="RawMouseInputPatch"/>] The same class for allowing method chaining.</returns>
        public RawMouseInputPatch SetPointerSensitivityToDefault()
        {
            Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Mouse", "MouseSensitivity", "10");
            return this;
        }
    }
}