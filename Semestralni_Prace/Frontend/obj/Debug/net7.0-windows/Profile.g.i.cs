// Updated by XamlIntelliSenseFileGenerator 24.11.2023 21:45:26
#pragma checksum "..\..\..\Profile.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1B356C186F38759C625ABDC92C7ECF7FB988591A"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using SemPrace.Frontend;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace SemPrace.Frontend
{


    /// <summary>
    /// Profile
    /// </summary>
    public partial class Profile : System.Windows.Window, System.Windows.Markup.IComponentConnector
    {


#line 10 "..\..\..\Profile.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel profile;

#line default
#line hidden


#line 22 "..\..\..\Profile.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel userProfile;

#line default
#line hidden


#line 26 "..\..\..\Profile.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock name;

#line default
#line hidden


#line 30 "..\..\..\Profile.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock age;

#line default
#line hidden


#line 34 "..\..\..\Profile.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock address;

#line default
#line hidden


#line 38 "..\..\..\Profile.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock email;

#line default
#line hidden


#line 40 "..\..\..\Profile.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button saveChange;

#line default
#line hidden


#line 43 "..\..\..\Profile.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image picture;

#line default
#line hidden


#line 45 "..\..\..\Profile.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button pictureChange;

#line default
#line hidden

        private bool _contentLoaded;

        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.1.0")]
        public void InitializeComponent()
        {
            if (_contentLoaded)
            {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/SemPrace.Frontend;component/profile.xaml", System.UriKind.Relative);

#line 1 "..\..\..\Profile.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);

#line default
#line hidden
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.1.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.profile = ((System.Windows.Controls.StackPanel)(target));
                    return;
                case 2:
                    this.pridatZaznam = ((System.Windows.Controls.Button)(target));

#line 14 "..\..\..\Profile.xaml"
                    this.pridatZaznam.Click += new System.Windows.RoutedEventHandler(this.Pridani);

#line default
#line hidden
                    return;
                case 3:
                    this.listPacientu = ((System.Windows.Controls.Button)(target));
                    return;
                case 4:
                    this.anamneza = ((System.Windows.Controls.Button)(target));
                    return;
                case 5:
                    this.vakcinace = ((System.Windows.Controls.Button)(target));
                    return;
                case 6:
                    this.odhlaseni = ((System.Windows.Controls.Button)(target));

#line 18 "..\..\..\Profile.xaml"
                    this.odhlaseni.Click += new System.Windows.RoutedEventHandler(this.Odhlaseni);

#line default
#line hidden
                    return;
                case 7:
                    this.userProfile = ((System.Windows.Controls.StackPanel)(target));
                    return;
                case 8:
                    this.name = ((System.Windows.Controls.TextBlock)(target));
                    return;
                case 9:
                    this.age = ((System.Windows.Controls.TextBlock)(target));
                    return;
                case 10:
                    this.address = ((System.Windows.Controls.TextBlock)(target));
                    return;
                case 11:
                    this.email = ((System.Windows.Controls.TextBlock)(target));
                    return;
                case 12:
                    this.saveChange = ((System.Windows.Controls.Button)(target));
                    return;
                case 13:
                    this.picture = ((System.Windows.Controls.Image)(target));
                    return;
                case 14:
                    this.pictureChange = ((System.Windows.Controls.Button)(target));
                    return;
            }
            this._contentLoaded = true;
        }
    }
}

