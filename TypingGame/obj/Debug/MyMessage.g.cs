﻿#pragma checksum "..\..\MyMessage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "88990FC8B8396D4C0A555664F7BD46F1"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:2.0.50727.3623
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

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


namespace TypingGame {
    
    
    /// <summary>
    /// MyMessage
    /// </summary>
    public partial class MyMessage : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 8 "..\..\MyMessage.xaml"
        internal System.Windows.Controls.Label LblDispScore;
        
        #line default
        #line hidden
        
        
        #line 9 "..\..\MyMessage.xaml"
        internal System.Windows.Controls.Image ImgMessage;
        
        #line default
        #line hidden
        
        
        #line 10 "..\..\MyMessage.xaml"
        internal System.Windows.Controls.Label LblDispMsg;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/TypingGame;component/mymessage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\MyMessage.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.LblDispScore = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.ImgMessage = ((System.Windows.Controls.Image)(target));
            return;
            case 3:
            this.LblDispMsg = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
