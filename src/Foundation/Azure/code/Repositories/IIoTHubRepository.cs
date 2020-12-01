using System.Collections.Generic;
using IoTHub.Foundation.Azure.Models.Templates;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace IoTHub.Foundation.Azure.Repositories
{
    public interface IIoTHubRepository
    {
        /// <summary>
        /// Cast Item to IoTHub
        /// </summary>
        /// <param name="hubItem"></param>
        /// <returns></returns>
        Models.Templates.IoTHub CastToHub(Item hubItem);

        /// <summary>
        /// Get IoTHub from Sitecore.ID
        /// </summary>
        /// <param name="hubId"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        Models.Templates.IoTHub GetHub(ID hubId, Database database = null);

        /// <summary>
        /// Get IoTHub from string reference (Sitecore path or ID)
        /// </summary>
        /// <param name="hubPath"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        Models.Templates.IoTHub GetHub(string hubPath, Database database = null);

        /// <summary>
        /// Get IoTHub with a certain Name
        /// </summary>
        /// <param name="hubName"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        Models.Templates.IoTHub GetHubByName(string hubName, Database database = null);

        /// <summary>
        /// Get a list of all available Hubs
        /// </summary>
        /// <param name="database"></param>
        /// <returns></returns>
        List<Models.Templates.IoTHub> GetHubs(Database database = null);

        /*
        /// <summary>
        /// Get IoTDevice with a certain Name
        /// </summary>
        /// <param name="devicePath"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        IoTDevice GetDeviceByName(string devicePath, Database database = null);

        /// <summary>
        /// Get IoTDevice with a certain Name
        /// </summary>
        /// <param name="hubName"></param>
        /// <param name="deviceName"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        IoTDevice GetDeviceByName(string hubName, string deviceName, Database database = null);

        /// <summary>
        /// Get IoTDeviceMethod with a certain Name
        /// </summary>
        /// <param name="hubName"></param>
        /// <param name="deviceName"></param>
        /// <param name="methodName"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        IoTDeviceMethod GetMethodByName(string hubName, string deviceName, string methodName, Database database = null);

        /// <summary>
        /// Get IoTDeviceMethod with a certain Name (HubName.DeviceName.MethodName) or Sitecore path/Id
        /// </summary>
        /// <param name="methodNameOrPath">Can be either a name (Eg: HubName.DeviceName.MethodName) or a Sitecore path/ID</param>
        /// <param name="database"></param>
        /// <returns></returns>
        IoTDeviceMethod GetMethodByName(string methodNameOrPath, Database database = null);

        /// <summary>
        /// Get IoTDevice and IoTDeviceMethod with a certain Name (HubName.DeviceName.MethodName)
        /// </summary>
        /// <param name="deviceMethodName"></param>
        /// <param name="method"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        IoTDevice GetDeviceAndMethodByName(string deviceMethodName, out IoTDeviceMethod method, Database database = null);
        */
    }
}
