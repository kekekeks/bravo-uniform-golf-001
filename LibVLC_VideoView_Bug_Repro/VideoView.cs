using System;
using System.Diagnostics;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Platform;
using System.Reactive;
namespace LibVLC_VideoView_Bug_Repro
{
    public class VideoView : NativeControlHost
    {
        public static readonly StyledProperty<string?> SourceArgsProperty = AvaloniaProperty.Register<VideoView, string?>(
            "SourceArgs");

        private IPlatformHandle? _xid;
        private Process? _mplayer;

        public string? SourceArgs
        {
            get => GetValue(SourceArgsProperty);
            set => SetValue(SourceArgsProperty, value);
        }

        static VideoView()
        {
            SourceArgsProperty.Changed.Subscribe(args => ((VideoView) args.Sender).UpdatePlayer());
        }

        protected override IPlatformHandle CreateNativeControlCore(IPlatformHandle parent)
        {
            _xid = base.CreateNativeControlCore(parent);
            UpdatePlayer();
            return _xid;
        }

        void UpdatePlayer()
        {
            try
            {
                _mplayer?.Kill();
            }
            catch
            {
                // Ignore
            }
            _mplayer = null;
            if (_xid != null && SourceArgs != null)
            {
                /*_mplayer = Process.Start("mplayer",
                    "-vo x11 -zoom -wid " + _xid.Handle.ToInt64() + " " + SourceArgs);*/
            }
        }

        protected override void DestroyNativeControlCore(IPlatformHandle control)
        {
            _xid = null;
            UpdatePlayer();
            base.DestroyNativeControlCore(control);
        }
    }
}