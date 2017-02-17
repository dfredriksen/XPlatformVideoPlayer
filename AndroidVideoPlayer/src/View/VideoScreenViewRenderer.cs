using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Xamarin.Forms;
using CYINT.XPlatformVideoPlayer;
using Xamarin.Forms.Platform.Android;
using Android.Media;
using Android.Graphics;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(VideoScreenView), typeof(VideoScreenViewRenderer))]
namespace CYINT.XPlatformVideoPlayer
{    
    public class VideoScreenViewRenderer : ViewRenderer<VideoScreenView,PCLSurfaceView>, ISurfaceHolderCallback
    {
        protected VideoScreenView _pclView; 
        protected PCLSurfaceView _surfaceView;
        protected ISurfaceHolder Holder;


        protected override void OnElementChanged(ElementChangedEventArgs<VideoScreenView> e)
        {       
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {     
                _pclView = Element;
                Holder = null;                    
                _surfaceView = new PCLSurfaceView(Forms.Context);
                _surfaceView.Holder.AddCallback(this);
                SetNativeControl(_surfaceView);
            }

        }

        public void SurfaceChanged(ISurfaceHolder holder, Android.Graphics.Format format, int width, int height)
        {            
            Holder = holder;
            GiveHolderToPlayer();
        }
         
        public void SurfaceCreated(ISurfaceHolder holder)
        {
            Holder = holder;
            GiveHolderToPlayer();
        }

        public void SurfaceDestroyed(ISurfaceHolder holder)
        { 
            Holder = null; 
            GiveHolderToPlayer();      
        }

        public void GiveHolderToPlayer()
        {
            if(_pclView.GetVideoPlayer() != null)
                ((AndroidVideoPlayerImplementation)_pclView.GetVideoPlayer()).SetHolder(Holder);
        }


    }
}