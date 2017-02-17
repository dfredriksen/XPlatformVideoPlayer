 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Android.Media;
using Android.Content;
using Android.Runtime;
using System.Threading.Tasks;
using Android.Widget;
using CYINT.XPlatformMediaPlayer;
using Android.Views;

[assembly: Xamarin.Forms.Dependency (typeof (CYINT.XPlatformVideoPlayer.AndroidVideoPlayerImplementation))]
namespace CYINT.XPlatformVideoPlayer
{  
    public class AndroidVideoPlayerImplementation : CYINT.XPlatformMediaPlayer.AndroidMediaPlayerImplementation, IXPlatformVideoPlayer
    {
        public const int PLAYER_STATE_NOSCREEN = 5;     
        protected VideoPlayerObject _videoPlayerObject;
        protected ISurfaceHolder Holder;
        
        public AndroidVideoPlayerImplementation() : base()
        {        
            _videoPlayerObject = null;
            _mediaType = MEDIA_VIDEO;
        }


        public override void SetMediaPlayer(IXPlatformMediaObject mediaPlayerObject)
        {
            _videoPlayerObject = (VideoPlayerObject)mediaPlayerObject;
        }


        public override MediaPlayerObject GetSpecificPlayerObject()
        {
            if(_videoPlayerObject == null)
                return null;

            return _videoPlayerObject;
        }


        public override MediaPlayerObject CreateSpecificPlayerObject()
        {
            MediaPlayer mPlayer = new MediaPlayer();
            _videoPlayerObject = new VideoPlayerObject();
            return _videoPlayerObject;
        }

        public VideoPlayerObject GetVideoPlayer()
        {
            if(_videoPlayerObject == null)
                GetMediaPlayer(); //Initializes the Video player

            return _videoPlayerObject;
        }


        public override void LoadMedia(string resource)
        {
            SetPlayFlag(false);

            if(GetPlayerState() != PLAYER_STATE_NONE && Holder != null)
                ResetResources();

            SetPlayerState(PLAYER_STATE_LOADING);
            _resource = resource;
            if(Holder != null)
                InitializeAsync();
            else
                SetPlayerState(PLAYER_STATE_NOSCREEN);
        }
        
        public void SetHolder(ISurfaceHolder holder)
        {
            Holder = holder;
            if(Holder == null)
                SetPlayerState(PLAYER_STATE_NOSCREEN);
            else
            {
                if(GetPlayerState() == PLAYER_STATE_NOSCREEN)
                {
                    SetPlayerState(PLAYER_STATE_LOADING);
                    InitializeAsync();
                }
            }
        }

        public async void InitializeAsync()
        {        
            await Task.Run(
                () =>
                {
                    GetVideoPlayer().SetDisplay(Holder);
                    GetVideoPlayer().SetDataSource(_resource);  
                    GetVideoPlayer().SetAudioStreamType(Stream.Music);           
                    GetVideoPlayer().PrepareAsync();
                }
            );
        }

    }

    public class AndroidVideoPlayerImplementationException : Exception
    {
        public AndroidVideoPlayerImplementationException(string message) : base (message)
        {}
    }
}