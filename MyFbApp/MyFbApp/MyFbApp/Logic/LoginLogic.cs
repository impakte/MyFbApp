using GalaSoft.MvvmLight.Ioc;
using MyFbApp.Services;
using MyFbApp.Sqlite;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MyFbApp.Logic
{
    class LoginLogic
    {
        private DatabaseManager _dbmanager;
        private FacebookServices _facebookServices;


        public LoginLogic()
        {
            _dbmanager = SimpleIoc.Default.GetInstance<DatabaseManager>();
            _facebookServices = SimpleIoc.Default.GetInstance<FacebookServices>();
        }

        private async Task<bool> checkToken()
        {
            if (_dbmanager.CheckToken())
            {
                string Token = _dbmanager.getLongLiveToken();
                return (await _facebookServices.CheckTokenValidity());
            }
            else
                return false;
        }

        public async Task<bool> getToken()
        {
            return (await checkToken());
        }

        public async Task SetTokenAsync(string token)
        {
            var LongLiveToken = await _facebookServices.getLongLiveToken(token);
            _dbmanager.UpdateLongLiveToken(LongLiveToken.access_token);
        }
    }
}
