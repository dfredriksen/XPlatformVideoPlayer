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
using System.Threading;
using System.Threading.Tasks;
using Android.Graphics;

namespace CYINT.XPlatformVideoPlayer
{
    public class PCLSurfaceView : SurfaceView
    {
        public PCLSurfaceView(Context myContext) : base(myContext)
        {    
        }
    }
}