using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight;

namespace MyFbAppLib.ViewModel
{
    public abstract class BaseViewModel : ViewModelBase
    {
        private bool _isLoading;
        private bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set { Set(ref _isRefreshing, value); }
        }

        public bool IsLoading
        {
            get { return _isLoading; }
            set { Set(ref _isLoading, value); }
        }
    }

    public abstract class BaseViewModel<T> : BaseViewModel, IBaseViewModelParameter<T>
    {
        public abstract void InjectParameter(T parameter);
    }


    public interface IBaseViewModelParameter<T>
    {
        void InjectParameter(T parameter);
    }
}
