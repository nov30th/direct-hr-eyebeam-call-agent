using System.Collections.Generic;

namespace DHRSoftphone.IDHRSPPlugin
{
    public interface IDHRSPLoad
    {
        void InitBeforeEvents();

        void StartUp();

        void InitConfiguration(Dictionary<string, string> items);

        void End();

        void OnSettingsChanged(object sender, Dictionary<string, string> settings);

        string Test();
    }
}