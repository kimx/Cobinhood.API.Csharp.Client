﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cobinhood.API.Csharp.Client.Test
{
    [TestClass]
    public class CobinhoodWebsocketTest
    {
        private static string apiKey = ConfigurationManager.AppSettings["ApiKey"];
        private static ApiClient apiClient = new ApiClient(apiKey);
        private static CobinhoodClient cobinhoodClient = new CobinhoodClient(apiClient);

        // Orders Websocket
        private void OrderMessageHandler(dynamic messageData)
        {
            var data = messageData;
        }

        [TestMethod]
        public void TestOrderWebsocket()
        {
            cobinhoodClient.ListenOrderEndpoint(OrderMessageHandler);
            Thread.Sleep(50000);
        }

        // Trades Websocket
        private void TradeMessageHandler(dynamic messageData)
        {
            var data = messageData;
        }

        [TestMethod]
        public void TestTradesWebsocket()
        {
            cobinhoodClient.ListenTradesEndpoint("ETH", "BTC", TradeMessageHandler);
            Thread.Sleep(50000);
        }

        // OrderBook Websocket
        private void OrderBookMessageHandler(dynamic messageData)
        {
            var data = messageData;
        }

        [TestMethod]
        public void TestOrderBookWebsocket()
        {
            cobinhoodClient.ListenOrderBookEndpoint("ETH", "BTC", OrderBookMessageHandler);
            Thread.Sleep(50000);
        }

        // Ticker Websocket
        private void TickerMessageHandler(dynamic messageData)
        {
            var data = messageData;
        }

        [TestMethod]
        public void TestTickerWebsocket()
        {
            cobinhoodClient.ListenTickerEndpoint("ETH", "BTC", TickerMessageHandler);
            Thread.Sleep(50000);
        }

        // Candle Websocket
        private void CandleMessageHandler(dynamic messageData)
        {
            var data = messageData;
        }

        [TestMethod]
        public void TestCandleWebsocket()
        {
            cobinhoodClient.ListenCandleEndpoint("ETH", "BTC", Models.Enums.Timeframe.TIMEFRAME_15_MINUTES, CandleMessageHandler);
            Thread.Sleep(50000);
        }

        // PingPong Websocket
        private void PingPongMessageHandler(dynamic messageData)
        {
            var data = messageData;
        }

        [TestMethod]
        public void TestPingPongWebsocket()
        {
            cobinhoodClient.ListenPingPongEndpoint(PingPongMessageHandler);
            Thread.Sleep(50000);
        }
    }
}
