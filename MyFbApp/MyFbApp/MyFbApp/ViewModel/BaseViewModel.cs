using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight;

namespace MyFbApp.ViewModel
{
    public abstract class BaseViewModel : ViewModelBase
    {
        /*private bool isBusy = false;
        private string title = string.Empty;

        public bool IsBusy
        {
            get { return isBusy; }
            set { Set(ref isBusy, value); }
        }

        public string Title
        {
            get { return title; }
            set { Set(ref title, value); }
        }*/
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
