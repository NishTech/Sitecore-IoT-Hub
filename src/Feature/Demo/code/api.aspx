<%@ Import Namespace="System.Net" %>
<%@ Import Namespace="System.Web.Mvc" %>
<%@ Import Namespace="IoTHub.Foundation.Azure" %>
<%@ Import Namespace="IoTHub.Foundation.Azure.Deserializers" %>
<%@ Import Namespace="IoTHub.Foundation.Azure.Models.Templates" %>
<%@ Import Namespace="IoTHub.Foundation.Azure.Repositories" %>
<%@ Page Language="c#"%>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sitecore IoT Hub - API Examples</title>
</head>
<body>
<form id="form1" runat="server">
    
    <h3>Get a method and execute it</h3>
    
    <%
        var ioTHubRepository = DependencyResolver.Current.GetService<IIoTHubRepository>();
        IoTDeviceMethod method;
        var device = ioTHubRepository.GetDeviceAndMethodByName("SitecoreIoTHub.Thermometer.GetState", out method);
        var results = method.Invoke(device);
        var temperature = results.GetValue("temperature");

        var dynamicResults = (dynamic)results;
        var temperature2 = dynamicResults.temperature;
    %>

    <table border="1">
        <tr>
            <td><b>Thermometer temperature:</b></td>
            <td><%= temperature %></td>
        </tr>
    </table>

</form>
</body>
</html>
