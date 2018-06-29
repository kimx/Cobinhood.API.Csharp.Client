using Cobinhood.API.Csharp.Client.Models.Market;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cobinhood.API.Csharp.Client.App
{
    class Program
    {
        private static string apiKey = ConfigurationManager.AppSettings["ApiKey"];
        private static ApiClient apiClient = new ApiClient(apiKey);
        private static CobinhoodClient cobinhoodClient = new CobinhoodClient(apiClient);

        static decimal initAmountCob = 1000;
        static void Main(string[] args)
        {
            Console.WriteLine("Begin");
            while (true)
            {
                var cobETH = cobinhoodClient.GetOrderBook("COB", "ETH").Result.Result.Orderbook;
                var usdCOB = cobinhoodClient.GetOrderBook("COB", "USDT").Result.Result.Orderbook;
                var usdETH = cobinhoodClient.GetOrderBook("ETH", "USDT").Result.Result.Orderbook;
                ShowOrderBook(cobETH, "COB-ETH");
                //ShowOrderBook(usdCOB, "USD-COB");
                //ShowOrderBook(usdETH, "USD-ETH");

                var usdAmount = CobToEthToUsd(initAmountCob, cobETH, usdETH);
                var usdCanBuyCobAmount = UsdBuyCob(usdAmount, usdCOB);
                if (usdCanBuyCobAmount > initAmountCob)
                {
                    throw new Exception(usdCanBuyCobAmount.ToString());
                    //var sellCob = cobinhoodClient.PlaceOrder("COB", "ETH", Models.Enums.OrderSide.Ask, Models.Enums.OrderType.Limit, initAmountCob.ToString(), cobETH.Asks.First().Price).Result;
                    //if (sellCob.Success)水
                    //{
                    //    var buyUsd = cobinhoodClient.PlaceOrder("ETH", "USD", Models.Enums.OrderSide.Ask, Models.Enums.OrderType.Limit, usdAmount.ToString(), usdETH.Bids.First().Price).Result;
                    //    var buyCob = cobinhoodClient.PlaceOrder("COB", "USD", Models.Enums.OrderSide.Bid, Models.Enums.OrderType.Limit, usdCanBuyCobAmount.ToString(), usdCOB.Asks.First().Price).Result;
                    //}
                }
                var usdCanBuyEthCobAmount = UsdToEthBuyCob(usdAmount, usdETH, cobETH);
                if (usdCanBuyEthCobAmount > initAmountCob)
                {
                    throw new Exception(usdCanBuyEthCobAmount.ToString());
                }

                Console.WriteLine("CobToEthToUsd:" + usdAmount);
                Console.WriteLine("UsdBuyCob:" + usdCanBuyCobAmount);
                Console.WriteLine("usdCanBuyEthCobAmount:" + usdCanBuyEthCobAmount);
                System.Threading.Thread.Sleep(10000);
                Console.WriteLine("==================================");

            }
            Console.Read();
            Console.WriteLine("End");
        }

        static void ShowOrderBook(Orderbook orderBook, string symbol)
        {
            var ask = orderBook.Asks.First();
            var bid = orderBook.Bids.First();
            Console.WriteLine($"{symbol}:{ask.Price}    ,    {ask.Size}");
            Console.WriteLine($"{symbol}:{bid.Price}    ,    {bid.Size}");
        }

        static decimal CobToEthToUsd(decimal qty, Orderbook cobETH, Orderbook usdETH)
        {
            var eth = qty * Convert.ToDecimal(cobETH.Bids.First().Price);
            var usd = eth * Convert.ToDecimal(usdETH.Bids.First().Price);
            return usd;

        }

        static decimal UsdBuyCob(decimal usdAmount, Orderbook usdCOB)
        {
            var qty = usdAmount / Convert.ToDecimal(usdCOB.Asks.First().Price);
            return qty;

        }

        static decimal UsdToEthBuyCob(decimal usdAmount, Orderbook usdETH, Orderbook cobETH)
        {
            var eth = usdAmount / Convert.ToDecimal(usdETH.Asks.First().Price);
            var qty = eth / Convert.ToDecimal(cobETH.Asks.First().Price);
            return qty;

        }
    }
}
