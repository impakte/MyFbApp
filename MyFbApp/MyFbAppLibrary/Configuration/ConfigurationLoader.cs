using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using GalaSoft.MvvmLight.Ioc;
using System.Threading.Tasks;

namespace MyFbAppLib.Configuration
{
    public class ConfigurationLoader
    {
        public void LoadConfig(Stream embeddedResourceStream)
        {
            if (embeddedResourceStream == null)
                return;

            using (var streamReader = new StreamReader(embeddedResourceStream))
            {
                var jsonString = streamReader.ReadToEnd();
               
                var configuration = JsonConvert.DeserializeObject<Config>(jsonString);

                if (configuration == null)
                    return;

                SimpleIoc.Default.Register<IConfiguration>(() => configuration);
            }
        }
    }
}
