using System.Configuration;
using Task1;

namespace Task1Console
{
    static class RepositoryFactory
    {
        private static BookRepository _repository;

        public static BookRepository Repository
        {
            get
            {
                if (_repository == null)
                {
                    switch (ConfigurationSettings.AppSettings["repository"])
                    {
                        case "BinaryFileBookRepository":
                            _repository = new BinaryFileBookRepository(ConfigurationSettings.AppSettings["filePath"]);
                            break;
                        default:
                            throw new SettingsPropertyNotFoundException("Dao settings are incorrect. Check the App.config file.");
                    }
                }
                return _repository;
            }
        }
    }
}
