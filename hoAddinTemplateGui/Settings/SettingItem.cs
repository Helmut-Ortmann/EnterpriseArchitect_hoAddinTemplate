
// ReSharper disable InconsistentNaming

using Newtonsoft.Json;

namespace hoAddinTemplateGui.Settings
{
    public class SettingItem
    {
        /// <summary>
        /// Root of SWADM *.eap file
        /// </summary>
        [JsonIgnore]
        public string RootSwadm
        {
            get => _rootSwadm?.Replace(@"\", "/") ?? "";
            set => _rootSwadm = value;
        }
        [JsonProperty("RootSwadm")]
        private string _rootSwadm;

        /// <summary>
        /// Root of Component *.eap file
        /// </summary>
        [JsonIgnore]
        public string RootComponent
        {
            get => _rootComponent?.Replace(@"\", "/") ?? "";
            set => _rootComponent = value;
        }
        [JsonProperty("RootComponent")]
        private string _rootComponent;
        


    }
}
