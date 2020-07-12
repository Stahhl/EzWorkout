using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Reflection;

namespace EzWorkout.Services
{
    //https://www.andrewhoefling.com/Blog/Post/xamarin-app-configuration-control-your-app-settings
    public class AppSettingsManager
    {
        private static AppSettingsManager _instance;
        private JObject _secrets;

        private readonly string Namespace = "EzWorkout.Configuration";
        private readonly string FileName = "appsettings.json";

        private AppSettingsManager()
        {
            #region conditional compilation
#if RELEASE
            FileName = "appsettings.RELEASE.json";
#else
            FileName = "appsettings.DEBUG.json";
#endif
            #endregion
            try
            {

                var assembly = IntrospectionExtensions.GetTypeInfo(typeof(AppSettingsManager)).Assembly;
                var stream = assembly.GetManifestResourceStream($"{Namespace}.{FileName}");
                using (var reader = new StreamReader(stream))
                {
                    var json = reader.ReadToEnd();
                    _secrets = JObject.Parse(json);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static AppSettingsManager Settings
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AppSettingsManager();
                }

                return _instance;
            }
        }

        public string this[string name]
        {
            get
            {
                try
                {
                    var path = name.Split(':');

                    JToken node = _secrets[path[0]];
                    for (int index = 1; index < path.Length; index++)
                    {
                        node = node[path[index]];
                    }

                    return node.ToString();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
