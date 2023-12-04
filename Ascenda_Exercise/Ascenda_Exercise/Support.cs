using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    public class Support
    {
        public void outputOffer(OfferData offerDatas,List<Restaurant> restaurant, List<Retail> retail, List<Activity> activity) 
        {
            List<merchants> merchants=new List<merchants>();
            List < Offer > offerDatasNew=new List<Offer>();
            if (restaurant.Count > 0)
            {
                merchants.Add(restaurant[0]);
            }
            if (retail.Count > 0)
            {
                merchants.Add(retail[0]);
            }

            if (activity.Count > 0)
            {
                merchants.Add(activity[0]);
            }
            merchants = merchants.OrderBy(a => a.distance).ToList();
            if (merchants.Count > 2)
            {
                merchants = merchants[0..2];
            }
            foreach (var merchant in merchants)
            {
                foreach (var offer in offerDatas.Offers)
                {
                    if (merchant.id == offer.id)
                    {
                        offer.merchants.Clear();
                        offer.merchants.Add(merchant);
                        offerDatasNew.Add(offer);
                        break;
                    }
                }
            }
            offerDatas.Offers.Clear();
            offerDatas.Offers = offerDatasNew;
        }
    }
}
