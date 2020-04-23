using System;
using System.Collections.Generic;
using System.Text;
using MyFbApp.Services;
using GalaSoft.MvvmLight.Ioc;
using MyFbApp.Configuration;

namespace MyFbApp.ViewModel
{
    public class Bootstrap
    {
        public static Bootstrap Instance
        {
            get
            {
                if (instance == null)
                    instance = new Bootstrap();

                return instance;
            }
        }

        private static Bootstrap instance;

        public void Setup()
        {
            InjectViewModels();
        }

        private void InjectViewModels()
        {
            SimpleIoc.Default.Register<LoginPageViewModel>();
            SimpleIoc.Default.Register<FacebookViewModel>();
            SimpleIoc.Default.Register<PostDetailViewModel>();
            SimpleIoc.Default.Register<FacebookServices>();
        }
    }
}
