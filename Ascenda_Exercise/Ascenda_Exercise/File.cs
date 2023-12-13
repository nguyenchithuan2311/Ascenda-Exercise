using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;


public class configReadJsonFile
{
    private readonly string _sampleJsonFilePath;
    OfferData OfferData = new OfferData()
    {
        Offers = new List<Offer>()
    };
    public configReadJsonFile(string sampleJsonFilePath)
    {
        _sampleJsonFilePath = sampleJsonFilePath;
    }
    public OfferData readJsonOffer()
    {
        using (StreamReader sr = File.OpenText(_sampleJsonFilePath))
        {
            var obj = sr.ReadToEnd();
            OfferData = JsonConvert.DeserializeObject<OfferData>(obj);
        }
        
        return OfferData;
    }
    public void writeJsonOffer(OfferData offerData)
    {
        string json = JsonConvert.SerializeObject(offerData);
        System.IO.File.WriteAllText(_sampleJsonFilePath, json);
    }
}
