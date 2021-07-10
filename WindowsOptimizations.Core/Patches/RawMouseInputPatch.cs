﻿using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using WindowsOptimizations.Core.GlobalData;

namespace WindowsOptimizations.Core.Patches
{
    /// <summary>
    /// Various kernel patches for reducing mouse input latency.
    /// </summary>
    public static class RawMouseInputPatch
    {
        /// <summary>
        /// Disables pointer acceleration completely by changing specific registry values.
        /// </summary>
        /// <returns>[<see cref="Task"/>] An asynchronous operation.</returns>
        public static Task DisablePointerAcceleration()
        {
            Registry.SetValue(RegistryKeys.MouseKey, "MouseSpeed", "0");
            Registry.SetValue(RegistryKeys.MouseKey, "MouseThreshold1", "0");
            Registry.SetValue(RegistryKeys.MouseKey, "MouseThreshold2", "0");

            return Task.CompletedTask;
        }

        /// <summary>
        /// Sets a 1:1 pointer precision based on the windows scaling.
        /// </summary>
        /// <returns>[<see cref="Task"/>] An asynchronous operation.</returns>
        public static Task SetOneToOnePointerPrecision()
        {
            double lastLoadedDpi = double.Parse(Registry.GetValue(RegistryKeys.ThemeManagerKey, "LastLoadedDPI", 96).ToString());
            double scale = 96.00 / lastLoadedDpi;

            // 96.00 --> A constant value (It may be PPI).
            // 24.00 --> The constant difference between each step in scaling, (For example: 100.00% to 125.00%).
            switch (scale.ToString("0.00%"))
            {
                case "100.00%":
                    Registry.SetValue(RegistryKeys.MouseKey, "SmoothMouseXCurve", new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xC0, 0xCC, 0x0C, 0x0, 0x0, 0x0, 0x0, 0x0, 0x80, 0x99, 0x19, 0x0, 0x0, 0x0, 0x0, 0x0, 0x40, 0x66, 0x26, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x33, 0x33, 0x0, 0x0, 0x0, 0x0, 0x0 });
                    Registry.SetValue(RegistryKeys.MouseKey, "SmoothMouseYCurve", new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x38, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x70, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xA8, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xE0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 });
                    break;

                // 125.00%
                // case var value when value == (96.00 / (lastLoadedDpi + 24.00)).ToString("0.00%"):
                case var value when value == (scale + 24.00).ToString("0.00%"):
                    Registry.SetValue(RegistryKeys.MouseKey, "SmoothMouseXCurve", new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x10, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x20, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x30, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x40, 0x0, 0x0, 0x0, 0x0, 0x0 });
                    Registry.SetValue(RegistryKeys.MouseKey, "SmoothMouseYCurve", new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x38, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x70, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xA8, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xE0, 0x0, 0x0, 0x0, 0x0, 0x0 });
                    break;

                // 150.00%
                case var value when value == (96.00 / (lastLoadedDpi + (24.00 * 2.00))).ToString("0.00%"):
                    Registry.SetValue(RegistryKeys.MouseKey, "SmoothMouseXCurve", new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x30, 0x33, 0x13, 0x0, 0x0, 0x0, 0x0, 0x0, 0x60, 0x66, 0x26, 0x0, 0x0, 0x0, 0x0, 0x0, 0x90, 0x99, 0x39, 0x0, 0x0, 0x0, 0x0, 0x0, 0xC0, 0xCC, 0x4C, 0x0, 0x0, 0x0, 0x0, 0x0 });
                    Registry.SetValue(RegistryKeys.MouseKey, "SmoothMouseYCurve", new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x38, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x70, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xA8, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xE0, 0x0, 0x0, 0x0, 0x0, 0x0 });
                    break;

                // 175.00%
                case var value when value == (96.00 / (lastLoadedDpi + (24.00 * 3.00))).ToString("0.00%"):
                    Registry.SetValue(RegistryKeys.MouseKey, "SmoothMouseXCurve", new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x60, 0x66, 0x16, 0x0, 0x0, 0x0, 0x0, 0x0, 0xC0, 0xCC, 0x2C, 0x0, 0x0, 0x0, 0x0, 0x0, 0x20, 0x33, 0x43, 0x0, 0x0, 0x0, 0x0, 0x0, 0x80, 0x99, 0x59, 0x0, 0x0, 0x0, 0x0, 0x0 });
                    Registry.SetValue(RegistryKeys.MouseKey, "SmoothMouseYCurve", new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x38, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x70, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xA8, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xE0, 0x0, 0x0, 0x0, 0x0, 0x0 });
                    break;

                // 200.00%
                case var value when value == (96.00 / (lastLoadedDpi + (24.00 * 4.00))).ToString("0.00%"):
                    Registry.SetValue(RegistryKeys.MouseKey, "SmoothMouseXCurve", new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x90, 0x99, 0x19, 0x0, 0x0, 0x0, 0x0, 0x0, 0x20, 0x33, 0x33, 0x0, 0x0, 0x0, 0x0, 0x0, 0xB0, 0xCC, 0x4C, 0x0, 0x0, 0x0, 0x0, 0x0, 0x40, 0x66, 0x66, 0x0, 0x0, 0x0, 0x0, 0x0 });
                    Registry.SetValue(RegistryKeys.MouseKey, "SmoothMouseYCurve", new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x38, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x70, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xA8, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xE0, 0x0, 0x0, 0x0, 0x0, 0x0 });
                    break;

                // 225.00%
                case var value when value == (96.00 / (lastLoadedDpi + (24.00 * 5.00))).ToString("0.00%"):
                    Registry.SetValue(RegistryKeys.MouseKey, "SmoothMouseXCurve", new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xC0, 0xCC, 0x1C, 0x0, 0x0, 0x0, 0x0, 0x0, 0x80, 0x99, 0x39, 0x0, 0x0, 0x0, 0x0, 0x0, 0x40, 0x66, 0x56, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x33, 0x73, 0x0, 0x0, 0x0, 0x0, 0x0 });
                    Registry.SetValue(RegistryKeys.MouseKey, "SmoothMouseYCurve", new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x38, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x70, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xA8, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xE0, 0x0, 0x0, 0x0, 0x0, 0x0 });
                    break;

                // 250.00%
                case var value when value == (96.00 / (lastLoadedDpi + (24.00 * 6.00))).ToString("0.00%"):
                    Registry.SetValue(RegistryKeys.MouseKey, "SmoothMouseXCurve", new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x20, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x40, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x60, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x80, 0x0, 0x0, 0x0, 0x0, 0x0 });
                    Registry.SetValue(RegistryKeys.MouseKey, "SmoothMouseYCurve", new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x38, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x70, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xA8, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xE0, 0x0, 0x0, 0x0, 0x0, 0x0 });
                    break;

                // 300.00%
                case var value when value == (96.00 / (lastLoadedDpi + (24.00 * 8.00))).ToString("0.00%"):
                    Registry.SetValue(RegistryKeys.MouseKey, "SmoothMouseXCurve", new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x60, 0x66, 0x26, 0x0, 0x0, 0x0, 0x0, 0x0, 0xC0, 0xCC, 0x4C, 0x0, 0x0, 0x0, 0x0, 0x0, 0x20, 0x33, 0x73, 0x0, 0x0, 0x0, 0x0, 0x0, 0x80, 0x99, 0x99, 0x0, 0x0, 0x0, 0x0, 0x0 });
                    Registry.SetValue(RegistryKeys.MouseKey, "SmoothMouseYCurve", new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x38, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x70, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xA8, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xE0, 0x0, 0x0, 0x0, 0x0, 0x0 });
                    break;

                // 350.00%
                case var value when value == (96.00 / (lastLoadedDpi + (24.00 * 10.00))).ToString("0.00%"):
                    Registry.SetValue(RegistryKeys.MouseKey, "SmoothMouseXCurve", new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xC0, 0xCC, 0x2C, 0x0, 0x0, 0x0, 0x0, 0x0, 0x80, 0x99, 0x59, 0x0, 0x0, 0x0, 0x0, 0x0, 0x40, 0x66, 0x86, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x33, 0xB3, 0x0, 0x0, 0x0, 0x0, 0x0 });
                    Registry.SetValue(RegistryKeys.MouseKey, "SmoothMouseYCurve", new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x38, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x70, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xA8, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xE0, 0x0, 0x0, 0x0, 0x0, 0x0 });
                    break;

                default:
                    MessageBox.Show("Your Windows scaling setting is either higher than 350% or lower than 100%. Cannot set a 1:1 pointer precision because of that.", nameof(RawMouseInputPatch), MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// Changes the pointer sensitivity to its default setting.
        /// <para>The default value in the kernel is 10.</para>
        /// </summary>
        /// <returns>[<see cref="Task"/>] An asynchronous operation.</returns>
        public static Task SetPointerSensitivityToDefault()
        {
            Registry.SetValue(RegistryKeys.MouseKey, "MouseSensitivity", "10");
            return Task.CompletedTask;
        }
    }
}