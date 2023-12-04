using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    public class Filter
    {
        public List<Offer> dateFilter(OfferData offerDatas, DateOnly date)
        {
            
            List<Offer> offerDatasNew=new List<Offer>();
            foreach (Offer offer in offerDatas.Offers)
            {
                DateOnly dt = DateOnly.Parse(offer.valid_to);
                DateOnly valid_date = date.AddDays(5);
                if (valid_date <= dt)
                {
                    offerDatasNew.Add(offer);
                }
            }
            return offerDatasNew;
        }
        public void categoryFilter(List<Offer> offerDatas, List<Restaurant> restaurant, List<Retail> retail, List<Activity> activity)
        {
            foreach (Offer offer in offerDatas)
            {
                foreach (merchants merchant in offer.merchants)
                {
                    if (offer.category == 1)
                    {
                        Restaurant restaurantTemp = new Restaurant(merchant);
                        restaurantTemp.id = offer.id;
                        restaurant.Add(restaurantTemp);
                    }
                    else if (offer.category == 2)
                    {
                        Retail retailTemp = new Retail(merchant);
                        retailTemp.id = offer.id;
                        retail.Add(retailTemp);
                    }
                    else if (offer.category == 4)
                    {
                        Activity activityTemp = new Activity(merchant);
                        activityTemp.id = offer.id;
                        activity.Add(activityTemp);
                    }
                }
            }
        }
    }
}
