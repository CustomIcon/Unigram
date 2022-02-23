﻿using System;
using Telegram.Td.Api;
using Unigram.Common;
using Unigram.Controls;
using Windows.Foundation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Unigram.Views.Popups
{
    public sealed partial class ZoomableMediaPopup : Grid
    {
        private readonly ApplicationView _applicationView;

        private string _fileToken;
        private string _thumbnailToken;

        private object _lastItem;

        public ZoomableMediaPopup()
        {
            InitializeComponent();

            _applicationView = ApplicationView.GetForCurrentView();
            _applicationView.VisibleBoundsChanged += OnVisibleBoundsChanged;

            OnVisibleBoundsChanged(_applicationView, null);
        }

        public Action<int> DownloadFile { get; set; }

        public Func<int> SessionId { get; set; }

        private void OnVisibleBoundsChanged(ApplicationView sender, object args)
        {
            if (sender == null)
            {
                return;
            }

            if (/*BackgroundElement != null &&*/ Window.Current?.Bounds is Rect bounds && sender.VisibleBounds != bounds)
            {
                Margin = new Thickness(sender.VisibleBounds.X - bounds.Left, sender.VisibleBounds.Y - bounds.Top, bounds.Width - (sender.VisibleBounds.Right - bounds.Left), bounds.Height - (sender.VisibleBounds.Bottom - bounds.Top));
            }
            else
            {
                Margin = new Thickness();
            }
        }

        public void SetSticker(Sticker sticker)
        {
            _lastItem = sticker;

            Title.Text = sticker.Emoji;
            Aspect.MaxWidth = 200;
            Aspect.MaxHeight = 200;
            Aspect.Constraint = sticker;

            if (sticker.Thumbnail != null)
            {
                UpdateThumbnail(sticker.Thumbnail.File, true);
            }

            UpdateFile(sticker, sticker.StickerValue, true);
        }

        public void SetAnimation(Animation animation)
        {
            _lastItem = animation;

            Title.Text = string.Empty;
            Aspect.MaxWidth = 320;
            Aspect.MaxHeight = 420;
            Aspect.Constraint = animation;

            if (animation.Thumbnail != null && animation.Thumbnail.Format is ThumbnailFormatJpeg)
            {
                UpdateThumbnail(animation.Thumbnail.File, true);
            }

            UpdateFile(animation, animation.AnimationValue, true);
        }

        private void UpdateFile(object target, File file)
        {
            if (_lastItem is Sticker sticker && file.Local.IsDownloadingCompleted)
            {
                if (sticker.StickerValue.Id == file.Id)
                {
                    UpdateFile(sticker, file, false);
                }
                else if (sticker.Thumbnail?.File.Id == file.Id)
                {
                    UpdateThumbnail(file, false);
                }
            }
            else if (_lastItem is Animation animation && file.Local.IsDownloadingCompleted)
            {
                if (animation.AnimationValue.Id == file.Id)
                {
                    UpdateFile(animation, file, false);
                }
            }
        }

        private void UpdateFile(Sticker sticker, File file, bool download)
        {
            if (file.Local.IsDownloadingCompleted)
            {
                if (sticker.Type is StickerTypeAnimated)
                {
                    Thumbnail.Opacity = 0;
                    Texture.Source = null;
                    Container.Child = new LottieView { Source = UriEx.ToLocal(file.Local.Path) };
                }
                else if (sticker.Type is StickerTypeVideo)
                {
                    Thumbnail.Opacity = 0;
                    Texture.Source = null;
                    Container.Child = new AnimationView { Source = new LocalVideoSource(file) };
                }
                else
                {
                    Thumbnail.Opacity = 0;
                    Texture.Source = PlaceholderHelper.GetWebPFrame(file.Local.Path);
                    Container.Child = new Border();
                }
            }
            else
            {
                Thumbnail.Opacity = 1;
                Texture.Source = null;
                Container.Child = new Border();

                UpdateManager.Subscribe(this, SessionId(), file, ref _fileToken, UpdateFile, true);

                if (file.Local.CanBeDownloaded && !file.Local.IsDownloadingActive && download)
                {
                    DownloadFile?.Invoke(file.Id);
                }
            }
        }

        private void UpdateFile(Animation animation, File file, bool download)
        {
            if (file.Local.IsDownloadingCompleted)
            {
                Thumbnail.Opacity = 0;
                Texture.Source = null;
                Container.Child = new AnimationView { Source = new LocalVideoSource(file) };
            }
            else
            {
                Thumbnail.Opacity = 1;
                Texture.Source = null;
                Container.Child = new Border();

                UpdateManager.Subscribe(this, SessionId(), file, ref _fileToken, UpdateFile, true);

                if (file.Local.CanBeDownloaded && !file.Local.IsDownloadingActive && download)
                {
                    DownloadFile?.Invoke(file.Id);
                }
            }
        }

        private void UpdateThumbnail(object target, File file)
        {
            UpdateThumbnail(file, false);
        }

        private void UpdateThumbnail(File file, bool download)
        {
            if (file.Local.IsDownloadingCompleted)
            {
                Thumbnail.Source = PlaceholderHelper.GetWebPFrame(file.Local.Path);
            }
            else
            {
                Thumbnail.Source = null;

                UpdateManager.Subscribe(this, SessionId(), file, ref _thumbnailToken, UpdateThumbnail, true);

                if (file.Local.CanBeDownloaded && !file.Local.IsDownloadingActive && download)
                {
                    DownloadFile?.Invoke(file.Id);
                }
            }
        }
    }
}
