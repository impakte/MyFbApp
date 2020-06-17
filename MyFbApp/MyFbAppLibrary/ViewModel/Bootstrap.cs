using System;
using System.Collections.Generic;
using System.Text;
using MyFbAppLib.Services;
using GalaSoft.MvvmLight.Ioc;
using MyFbAppLib.Configuration;

namespace MyFbAppLib.ViewModel
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
