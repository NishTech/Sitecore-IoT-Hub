using System.Web.Mvc;
using IoTHub.Feature.Demo.Models;
using IoTHub.Foundation.Azure.Models.Templates;
using IoTHub.Foundation.Azure.Repositories;
using Sitecore;
using Sitecore.Mvc.Controllers;
using Sitecore.Mvc.Presentation;
using Sitecore.Data.Items;
using System.Linq;
using IoTHub.Foundation.Azure.Deserializers;

namespace IoTHub.Feature.Demo.Controllers
{
    public class SmartDisplayController : SitecoreController
    {
        private readonly IoTDeviceMethod _method;
        private readonly IoTDevice _device;
        private const string MethodPath = "SitecoreIoTHub.SmartDisplay.GetSelectedObject";

        public SmartDisplayController(IIoTHubRepository ioTHubRepository)
        {
            _method = ioTHubRepository.GetMethodByName(MethodPath);
            _device = ioTHubRepository.GetDeviceByName(MethodPath);
        }

        public override ActionResult Index()
        {
            var dataSourceItem = GetDeviceItem();
            return dataSourceItem == null ? View() : View(new __ProductEntry(dataSourceItem));
        }

        [HttpPost]
        public JsonResult HasChanges(string currentState)
        {
            if (_method == null || _device == null)
                return Json(false);

            var selectedObject = GetSelectedObject();
            var hasChanges = selectedObject != currentState;

            return Json(hasChanges);
        }

        private Item GetDeviceItem()
        {
            // Item from Personalization
            var dataSourceId = RenderingContext.Current.Rendering.DataSource;
            var dataSourceItem = !string.IsNullOrEmpty(dataSourceId)
                ? Context.Database.GetItem(dataSourceId)
                : Context.Item;

            // Selected object from device
            var selectedObject = GetSelectedObject();

            // If they are different, device will prevail (Sitecore bug?)
            if (dataSourceItem.Name != selectedObject)
            {
                dataSourceItem = dataSourceItem.Parent.Children.FirstOrDefault(p => p.Name == selectedObject);
                if (dataSourceItem == null)
                    dataSourceItem = Context.Item;
            }

            return dataSourceItem;
        }

        private string GetSelectedObject()
        {
            dynamic response = _method.Invoke(_device);
            if (response==null)
                return "Empty";
            var selectedObject = response.currentObject;
            if (string.IsNullOrEmpty(selectedObject))
                selectedObject = "Empty";
            return selectedObject;
        }
    }
}