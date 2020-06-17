using GalaSoft.MvvmLight.Ioc;
using MyFbAppLib.Services;
using MyFbAppLib.Sqlite;
using System.Threading.Tasks;
//using MyFbAppFSharp;

namespace MyFbAppLib.Logic
{
    public class LoginLogic
    {
        private DatabaseManager _dbmanager;
        private FacebookServices _facebookServices;

        public LoginLogic()
        {
            _dbmanager = SimpleIoc.Default.GetInstance<DatabaseManager>();
            _facebookServices = SimpleIoc.Default.GetInstance<FacebookServices>();
        }

        public async Task<bool> CheckToken()
        {
            if (_dbmanager.CheckToken())
                return await _facebookServices.CheckTokenValidity();
            else
                return false;
        }

        public async Task SetTokenAsync(string token)
        {
            var LongLiveToken = await _facebookServices.getLongLiveToken(token);
            _dbmanager.UpdateLongLiveToken(LongLiveToken.access_token);
        }
    }
}
