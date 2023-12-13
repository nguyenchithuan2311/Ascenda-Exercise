using Ascenda_Exercise;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

class Program
{
    static void Main(string[] args)
    {
        
        if (args.Length == 1)
        {
            Support support = new Support();
            if (support.IsDateFormatValid(args[0]))
            {
                configReadJsonFile readJson = new configReadJsonFile("input.json");

                OfferData offerData = new OfferData();
                offerData = readJson.readJsonOffer();
                Filter filter = new Filter();

                // Handle the form submission

                DateOnly dt = DateOnly.Parse(args[0]);
                List<Offer> offerDataNew = new List<Offer>();
                List<Restaurant> restaurant = new List<Restaurant>();
                List<Retail> retail = new List<Retail>();
                List<Activity> activity = new List<Activity>();
                Sort sort = new Sort();
                
                offerDataNew = filter.dateFilter(offerData, dt);
                filter.categoryFilter(offerDataNew, restaurant, retail, activity);
                sort.sortListMerchants(restaurant.Cast<merchants>().ToList());
                sort.sortListMerchants(retail.Cast<merchants>().ToList());
                sort.sortListMerchants(retail.Cast<merchants>().ToList());
                support.outputOffer(offerData, restaurant, retail, activity);
                //---------------------------------------------------..\\..\\..\\output.json
                configReadJsonFile writeJson = new configReadJsonFile("output.json");
                writeJson.writeJsonOffer(offerData);
            }
            else
            {
                Console.WriteLine("Invalid date");
            }
        }
        else
        {
            Console.WriteLine("1 Argument");
        }
    }
}
