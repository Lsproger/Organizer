#pragma checksum "..\..\..\Windows\MainWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "1630C11BE659643748A5BA5FFF5DEFE2"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Windows.Controls;
using Microsoft.Windows.Controls.Ribbon;
using Organizer;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Organizer
{


    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector
    {

#line default
#line hidden


#line 36 "..\..\..\Windows\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox _lessonsBox;

#line default
#line hidden


#line 39 "..\..\..\Windows\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox _progressList;

#line default
#line hidden


#line 92 "..\..\..\Windows\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox _notesList;

#line default
#line hidden


#line 128 "..\..\..\Windows\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox _lessons;

#line default
#line hidden


#line 154 "..\..\..\Windows\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox _week;

#line default
#line hidden


#line 174 "..\..\..\Windows\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox _messageToDB;

#line default
#line hidden


#line 178 "..\..\..\Windows\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox _messages;

#line default
#line hidden


#line 244 "..\..\..\Windows\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock _userName;

#line default
#line hidden


#line 245 "..\..\..\Windows\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock _userID;

#line default
#line hidden


#line 247 "..\..\..\Windows\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock _otherInfo;

#line default
#line hidden

        private bool _contentLoaded;

        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (_contentLoaded)
            {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Organizer;component/windows/mainwindow.xaml", System.UriKind.Relative);

#line 1 "..\..\..\Windows\MainWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);

#line default
#line hidden
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.MainWindow1 = ((Organizer.MainWindow)(target));
                    return;
                case 2:
                    this._lessonsBox = ((System.Windows.Controls.ComboBox)(target));
                    return;
                case 3:

#line 37 "..\..\..\Windows\MainWindow.xaml"
                    ((Microsoft.Windows.Controls.Ribbon.RibbonButton)(target)).Click += new System.Windows.RoutedEventHandler(this._addProgress_Click);

#line default
#line hidden
                    return;
                case 4:
                    this._progressList = ((System.Windows.Controls.ListBox)(target));
                    return;
                case 9:
                    this._notesList = ((System.Windows.Controls.ListBox)(target));
                    return;
                case 10:
                    this._lessons = ((System.Windows.Controls.ListBox)(target));
                    return;
                case 11:

#line 148 "..\..\..\Windows\MainWindow.xaml"
                    ((System.Windows.Controls.TextBox)(target)).LostFocus += new System.Windows.RoutedEventHandler(this.TextBox_LostFocus);

#line default
#line hidden
                    return;
                case 12:

#line 150 "..\..\..\Windows\MainWindow.xaml"
                    ((System.Windows.Controls.TextBox)(target)).LostFocus += new System.Windows.RoutedEventHandler(this.TextBox_LostFocus);

#line default
#line hidden
                    return;
                case 13:

#line 152 "..\..\..\Windows\MainWindow.xaml"
                    ((System.Windows.Controls.TextBox)(target)).LostFocus += new System.Windows.RoutedEventHandler(this.TextBox_LostFocus);

#line default
#line hidden
                    return;
                case 14:
                    this._week = ((System.Windows.Controls.ComboBox)(target));
                    return;
                case 15:
                    this._messageToDB = ((System.Windows.Controls.TextBox)(target));
                    return;
                case 16:

#line 176 "..\..\..\Windows\MainWindow.xaml"
                    ((Microsoft.Windows.Controls.Ribbon.RibbonButton)(target)).Click += new System.Windows.RoutedEventHandler(this.SendMessage_Click);

#line default
#line hidden
                    return;
                case 17:
                    this._messages = ((System.Windows.Controls.ListBox)(target));
                    return;
                case 19:
                    this._userName = ((System.Windows.Controls.TextBlock)(target));
                    return;
                case 20:
                    this._userID = ((System.Windows.Controls.TextBlock)(target));
                    return;
                case 21:
                    this._otherInfo = ((System.Windows.Controls.TextBlock)(target));
                    return;
            }
            this._contentLoaded = true;
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 5:

#line 49 "..\..\..\Windows\MainWindow.xaml"
                    ((Microsoft.Windows.Controls.Ribbon.RibbonButton)(target)).Click += new System.Windows.RoutedEventHandler(this.NeededTasksPlus_Click);

#line default
#line hidden
                    break;
                case 6:

#line 50 "..\..\..\Windows\MainWindow.xaml"
                    ((Microsoft.Windows.Controls.Ribbon.RibbonButton)(target)).Click += new System.Windows.RoutedEventHandler(this.NeededTasksMinus_Click);

#line default
#line hidden
                    break;
                case 7:

#line 57 "..\..\..\Windows\MainWindow.xaml"
                    ((Microsoft.Windows.Controls.Ribbon.RibbonButton)(target)).Click += new System.Windows.RoutedEventHandler(this.CompletedTasksPlus_Click);

#line default
#line hidden
                    break;
                case 8:

#line 58 "..\..\..\Windows\MainWindow.xaml"
                    ((Microsoft.Windows.Controls.Ribbon.RibbonButton)(target)).Click += new System.Windows.RoutedEventHandler(this.CompletedTasksMinus_Click);

#line default
#line hidden
                    break;
                case 18:

#line 194 "..\..\..\Windows\MainWindow.xaml"
                    ((Microsoft.Windows.Controls.Ribbon.RibbonButton)(target)).Click += new System.Windows.RoutedEventHandler(this._deleteMsg);

#line default
#line hidden
                    break;
            }
        }

        internal System.Windows.Window MainWindow1;
        internal System.Windows.Controls.Calendar _calendar;
    }
}

