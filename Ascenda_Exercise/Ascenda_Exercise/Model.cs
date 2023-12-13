using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;


public class OfferData
{
    public List<Offer> Offers { get; set; }
}
public class Offer
{
    public int id { get; set; }
    public string title { get; set; }
    public string description { get; set; }
    public int category { get; set; }
    public List<merchants> merchants { get; set; }
    public string valid_to { get; set; }
    /*": "2020-02-01"*/
}
public class merchants
{
    public int id { get; set; }
    public string name { get; set; }
    public float distance { get; set; }
}

public class Restaurant : merchants
{
    public Restaurant(merchants merchant)
    {
        this.id = merchant.id;
        this.name = merchant.name;
        this.distance = merchant.distance;
    }
    private int idOffer { get; set; }
}
public class Retail : merchants
{
    public Retail(merchants merchants)
    {
        this.id = merchants.id;
        this.name = merchants.name;
        this.distance = merchants.distance;
    }
    private int idOffer { get; set; }
}
public class Activity : merchants
{
    public Activity(merchants merchants)
    {
        this.id = merchants.id;
        this.name = merchants.name;
        this.distance = merchants.distance;
    }
    private int idOffer { get; set; }
}