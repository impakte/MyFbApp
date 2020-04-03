using System;
using System.Collections.Generic;
using System.Text;
using MyFbApp.Services;
using GalaSoft.MvvmLight.Ioc;

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
            //InjectServices();
            InjectViewModels();
        }

        private void InjectViewModels()
        {
            //TODO To improve using interfaces ?
            SimpleIoc.Default.Register<LoginPageViewModel>();
            SimpleIoc.Default.Register<FacebookViewModel>();
            SimpleIoc.Default.Register<PostDetailViewModel>();
            SimpleIoc.Default.Register<FacebookServices>();
        }
    }
}
