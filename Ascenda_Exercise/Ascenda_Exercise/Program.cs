using ConsoleApp3;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

class Program
{
    static void Main()
    {
        // Create an HTTP listener and start listening for requests
        HttpListener listener = new HttpListener();
        listener.Prefixes.Add("http://localhost:8080/");
        listener.Start();

        Console.WriteLine("Listening for requests...");

        while (true)
        {
            // Wait for a request to come in
            HttpListenerContext context = listener.GetContext();

            // Process the request
            if (context.Request.HttpMethod == "GET")
            {
                // Send the HTML form to the client
                string formHtml = GetFormHtml();
                byte[] formBytes = System.Text.Encoding.UTF8.GetBytes(formHtml);

                context.Response.ContentLength64 = formBytes.Length;
                context.Response.OutputStream.Write(formBytes, 0, formBytes.Length);
            }
            else if (context.Request.HttpMethod == "POST")
            {
                //-------------------------------------------------..\\..\\..\\input.json
                configReadJsonFile readJson = new configReadJsonFile("input.json");

                OfferData offerData = new OfferData();
                offerData = readJson.readJsonOffer();
                Filter filter = new Filter();
                
                // Handle the form submission
                using (StreamReader reader = new StreamReader(context.Request.InputStream))
                {
                    string requestBody = reader.ReadToEnd();
                    string selectedDate = GetSelectedDateFromRequestBody(requestBody);
                    if (selectedDate.Length != 16)
                    {
                        DateOnly dt=DateOnly.Parse(selectedDate);
                        List<Offer> offerDataNew = new List<Offer>();
                        List<Restaurant> restaurant= new List<Restaurant>();
                        List< Retail > retail = new List<Retail>();
                        List<Activity> activity = new List<Activity>();
                        Sort sort = new Sort();
                        Support support = new Support();
                        offerDataNew =filter.dateFilter(offerData, dt);
                        filter.categoryFilter(offerDataNew, restaurant, retail, activity);
                        sort.sortListMerchants(restaurant.Cast<merchants>().ToList());
                        sort.sortListMerchants(retail.Cast<merchants>().ToList());
                        sort.sortListMerchants(retail.Cast<merchants>().ToList());
                        support.outputOffer(offerData, restaurant, retail, activity);
                        //---------------------------------------------------..\\..\\..\\output.json
                        configReadJsonFile writeJson = new configReadJsonFile("output.json");
                        writeJson.writeJsonOffer(offerData);

                    }
                    string output = "";
                    foreach (var offer in offerData.Offers)
                    {
                        output =output+ "\n{id: " + offer.id +
                            ", title: " + offer.title +
                            ", description: " + offer.description +
                            ", category: " + offer.category +
                            ", merchants: [{id: " + offer.merchants[0].id +
                            ", name: " + offer.merchants[0].name +
                            ", distance" + offer.merchants[0].distance +
                            "}], valid_to: " + offer.valid_to +
                            "}<br>";
                    }
                    // Construct the response content
                    string responseString = $"<html><body><h1>{output}</h1></body></html>";
                    byte[] responseBytes = System.Text.Encoding.UTF8.GetBytes(responseString);

                    // Get the response stream and write the response
                    context.Response.ContentLength64 = responseBytes.Length;
                    context.Response.OutputStream.Write(responseBytes, 0, responseBytes.Length);
                }
            }

            // Close the response stream
            context.Response.OutputStream.Close();
        }
    }

    static string GetFormHtml()
    {
        return @"
            <html>
<body>
    <h1>Select a Date</h1>
    <form method='post'>
        <label for='selectedDate'>Date:</label>
        <input type='date' id='selectedDate' name='selectedDate' pattern='\d{4}/\d{2}/\d{2}' placeholder='YYYY/MM/DD' required>
        <br>
        <input type='submit' value='Submit'>
    </form>
</body>
</html>";
    }

    static string GetSelectedDateFromRequestBody(string requestBody)
    {
        // Extract the selected date from the form submission
        string[] keyValuePairs = requestBody.Split('&');
        foreach (var pair in keyValuePairs)
        {
            string[] keyValue = pair.Split('=');
            if (keyValue.Length == 2 && keyValue[0] == "selectedDate")
            {
                return WebUtility.UrlDecode(keyValue[1]);
            }
        }
        return "No date selected";
    }
}
