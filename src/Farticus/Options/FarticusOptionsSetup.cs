using Microsoft.Framework.OptionsModel;

namespace Farticus
{
    public class FarticusOptionsSetup : ConfigureOptions<FarticusOptions>
    {
        public FarticusOptionsSetup() : base(ConfigureFarticus)
        {
            Order = 0;
        }

        public static void ConfigureFarticus(FarticusOptions options)
        {
            options.NumberOfMessages = 1;
        }
    }
}