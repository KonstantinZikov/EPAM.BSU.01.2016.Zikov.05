using System.Configuration;
using Task1;

namespace Task1Console
{
    static class DaoFactory
    {
        private static BookDao _dao;

        public static BookDao Dao
        {
            get
            {
                if (_dao == null)
                {
                    switch (ConfigurationSettings.AppSettings["dao"])
                    {
                        case "BinaryFileBookDao":
                            _dao = new BinaryFileBookDao(ConfigurationSettings.AppSettings["filePath"]);
                            break;
                        default:
                            throw new SettingsPropertyNotFoundException("Dao settings are incorrect. Check the App.config file.");
                    }
                }
                return _dao;
            }
        }
    }
}
