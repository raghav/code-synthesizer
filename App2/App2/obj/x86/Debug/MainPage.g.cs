﻿#pragma checksum "D:\Competitions\Rajasthan Hackathon\App2\App2\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "B5C2F77E888E61F22F2B8F5A4A357415"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace App2
{
    partial class MainPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                {
                    this.AppName = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 2:
                {
                    this.FileName = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 3:
                {
                    this.BrowseFiles = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 13 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.BrowseFiles).Click += this.BrowseFiles_Click;
                    #line default
                }
                break;
            case 4:
                {
                    this.WebRadio = (global::Windows.UI.Xaml.Controls.RadioButton)(target);
                }
                break;
            case 5:
                {
                    this.AndroidRadio = (global::Windows.UI.Xaml.Controls.RadioButton)(target);
                }
                break;
            case 6:
                {
                    this.WindowsRadio = (global::Windows.UI.Xaml.Controls.RadioButton)(target);
                }
                break;
            case 7:
                {
                    this.IosRadio = (global::Windows.UI.Xaml.Controls.RadioButton)(target);
                }
                break;
            case 8:
                {
                    this.SubmitBtn = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 18 "..\..\..\MainPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.SubmitBtn).Click += this.SubmitBtn_Click;
                    #line default
                }
                break;
            case 9:
                {
                    this.ErrorBlock = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}
