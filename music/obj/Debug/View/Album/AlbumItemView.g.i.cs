﻿#pragma checksum "..\..\..\..\View\Album\AlbumItemView.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "04D7BCDA65783F282F78578866217EB95E5F35BEE5639A720562F85FA4925D1F"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
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
using System.Windows.Shell;
using music.View.Album;


namespace music.View.Album {
    
    
    /// <summary>
    /// AlbumItemView
    /// </summary>
    public partial class AlbumItemView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 8 "..\..\..\..\View\Album\AlbumItemView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal music.View.Album.AlbumItemView controlTopic;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\..\View\Album\AlbumItemView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image topicImage;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\..\View\Album\AlbumItemView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tbTopicName;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/music;component/view/album/albumitemview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\View\Album\AlbumItemView.xaml"
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
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.controlTopic = ((music.View.Album.AlbumItemView)(target));
            
            #line 9 "..\..\..\..\View\Album\AlbumItemView.xaml"
            this.controlTopic.PreviewMouseDown += new System.Windows.Input.MouseButtonEventHandler(this.controlTopic_PreviewMouseDown);
            
            #line default
            #line hidden
            
            #line 9 "..\..\..\..\View\Album\AlbumItemView.xaml"
            this.controlTopic.MouseMove += new System.Windows.Input.MouseEventHandler(this.controlTopic_MouseMove);
            
            #line default
            #line hidden
            
            #line 9 "..\..\..\..\View\Album\AlbumItemView.xaml"
            this.controlTopic.MouseLeave += new System.Windows.Input.MouseEventHandler(this.controlTopic_MouseLeave);
            
            #line default
            #line hidden
            return;
            case 2:
            this.topicImage = ((System.Windows.Controls.Image)(target));
            return;
            case 3:
            this.tbTopicName = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
