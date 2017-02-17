using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace CYINT.XPlatformVideoPlayer
{
    public class VideoScreenView : View
    {        
       public static readonly BindableProperty VideoPlayerProperty =
            BindableProperty.Create("VideoPlayer", typeof (IXPlatformVideoPlayer), typeof (IXPlatformVideoPlayer));    

        protected IXPlatformVideoPlayer _videoPlayer
        {
            get { return (IXPlatformVideoPlayer)GetValue (VideoPlayerProperty); }
            set { SetValue (VideoPlayerProperty, value); }
        }

        public VideoScreenView()
        {                
        }

        public IXPlatformVideoPlayer GetVideoPlayer()
        {
             return _videoPlayer;
        }

        public void SetVideoPlayer(IXPlatformVideoPlayer videoPlayer)
        {
            _videoPlayer = videoPlayer;      
        }
    }
}
