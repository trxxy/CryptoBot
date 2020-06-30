using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace SushiHouseParser
{
    public class Parser : IParser
    {
        private const string siteUrl = "https://sushihouse.by";
        private const string rootUrl = "https://sushihouse.by/catalog/sushi-i-rolly/";
        private const string mainNodeXpath = ".//div[contains(@class, 'row product-list')]";
        private const string nameNodeXpath = ".//span[contains(@itemprop, 'name')]";
        private const string descriptionNodeXpath = ".//div[contains(@class, 'description')]";
        private const string priceNodeXpath = ".//span[contains(@itemprop, 'price')]";
        private const string weightNodeXpath = ".//span[contains(@class, 'weight')]";
        private const string exceptNodeString = "product banner";
        HtmlNode node;

        public Parser()
        {
            var web = new HtmlWeb();
            var doc = web.Load(rootUrl);
            node = doc.DocumentNode;
        }

        public List<ParsedSushi> ParseCatalog()
        {
            var sushiList = new List<ParsedSushi>();
            var mainNode = node.SelectSingleNode(mainNodeXpath);

            foreach (var productNode in mainNode.ChildNodes)
            {
                bool isContainAdsNode = productNode.InnerHtml.Contains(exceptNodeString);
                bool hasAnyContent = productNode.FirstChild != null;
                if (!isContainAdsNode && hasAnyContent)
                {
                    bool isTargetNods = productNode.ChildNodes[1].ChildNodes.Count > 3;
                    if (isTargetNods)
                    {
                        var productInfoNode = productNode.ChildNodes[1]?.ChildNodes[3];
                        if (productInfoNode != null)
                        {
                            var sushi = new ParsedSushi()
                            {
                                ImageUrl = siteUrl + productNode.Descendants("a")
                                    .FirstOrDefault().Attributes["href"].Value,
                                Name = productInfoNode.SelectNodes(nameNodeXpath)
                                    .LastOrDefault()
                                    .InnerText,
                                Description = productInfoNode.SelectSingleNode(descriptionNodeXpath)
                                    .InnerText
                                    .ValidateDescription(),
                                Price = productInfoNode.SelectSingleNode(priceNodeXpath)
                                    .InnerText
                                    .ValidatePrice(),
                                Weight = productInfoNode.SelectSingleNode(weightNodeXpath)
                                    .InnerText
                                    .ValidateWeight()
                            };
                            sushiList.Add(sushi);
                        }
                    }
                }
            }
            return sushiList;
        }
    }
}