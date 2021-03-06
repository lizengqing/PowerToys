﻿// Copyright (c) Brice Lambson
// The Brice Lambson licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.  Code forked from Brice Lambson's https://github.com/bricelam/ImageResizer/
// ShowAdvancedCommand = new RelayCommand(ShowAdvanced);

using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using ImageResizer.Models;
using ImageResizer.Properties;
using ImageResizer.Views;

namespace ImageResizer.ViewModels
{
    public class InputViewModel : ViewModelBase
    {
        private readonly ResizeBatch _batch;
        private readonly MainViewModel _mainViewModel;
        private readonly IMainView _mainView;

        public InputViewModel(
            Settings settings,
            MainViewModel mainViewModel,
            IMainView mainView,
            ResizeBatch batch)
        {
            _batch = batch;
            _mainViewModel = mainViewModel;
            _mainView = mainView;

            Settings = settings;
            settings.CustomSize.PropertyChanged += (sender, e) => settings.SelectedSize = (CustomSize)sender;

            ResizeCommand = new RelayCommand(Resize);
            CancelCommand = new RelayCommand(Cancel);
        }

        public Settings Settings { get; }

        public ICommand ResizeCommand { get; }

        public ICommand CancelCommand { get; }

        public ICommand ShowAdvancedCommand { get; }

        public void Resize()
        {
            Settings.Save();
            _mainViewModel.CurrentPage = new ProgressViewModel(_batch, _mainViewModel, _mainView);
        }

        public void Cancel()
            => _mainView.Close();
    }
}
