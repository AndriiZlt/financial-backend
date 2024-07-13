using aspnetcore.ntier.BLL.Services.IServices;
using aspnetcore.ntier.DTO.DTOs;
using AutoMapper;
using Newtonsoft.Json;
using System.Text;
using Serilog;
using aspnetcore.ntier.DAL.Repositories.IRepositories;
using System.Security.Claims;
using aspnetcore.ntier.DAL.Repositories;
using aspnetcore.ntier.DAL.Entities;
using Microsoft.AspNetCore.Http;




namespace aspnetcore.ntier.BLL.Services
{
    public class AlpacaService : IAlpacaService
    {
        private readonly IMapper _mapper;
        private readonly Uri tradingUri = new Uri("https://paper-api.alpaca.markets/v2/");
        private readonly Uri dataUri = new Uri("https://data.alpaca.markets/v2/"); 
        private readonly IAlpacaRepository _alpacaRepository;
        private readonly INotificationService _notificationService;
        private readonly IHttpContextAccessor _httpContext;

        public AlpacaService(IMapper mapper, IAlpacaRepository alpacaRepository, IHttpContextAccessor httpContext, INotificationService notificationService)
        {
            _alpacaRepository = alpacaRepository;
            _httpContext = httpContext;
            _mapper = mapper;
            _notificationService = notificationService;
        }

        public async Task<List<AssetToReturn>> GetAssetsAsync(string keyId, string secretKey)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("APCA-API-KEY-ID", keyId);
                client.DefaultRequestHeaders.Add("APCA-API-SECRET-KEY", secretKey);
                var response = client.GetAsync(this.tradingUri + "assets").Result;
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"HTTP request failed with status code {response.StatusCode}");
                }
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var ConvertedResponse = JsonConvert.DeserializeObject<List<AssetToReturn>>(jsonResponse);
                return ConvertedResponse;
            }

        }

        public async Task<AssetToReturn> GetAssetAsync(string keyId, string secretKey, string assetId)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("APCA-API-KEY-ID", keyId);
                client.DefaultRequestHeaders.Add("APCA-API-SECRET-KEY", secretKey);
                var response = client.GetAsync(this.tradingUri + $"assets/{assetId}").Result;
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"HTTP request failed with status code {response.StatusCode}");
                }
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var ConvertedResponse = JsonConvert.DeserializeObject<AssetToReturn>(jsonResponse);
                return ConvertedResponse;
            }

        }

        public async Task<List<PositionToReturn>> GetPositionsAsync(string keyId, string secretKey)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("APCA-API-KEY-ID", keyId);
                client.DefaultRequestHeaders.Add("APCA-API-SECRET-KEY", secretKey);
                var response = client.GetAsync(this.tradingUri + "positions").Result;
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"HTTP request failed with status code {response.StatusCode}");
                }
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var ConvertedResponse = JsonConvert.DeserializeObject<List<PositionToReturn>>(jsonResponse);
                return ConvertedResponse;
            }

        }

        public async Task<PositionToReturn> ClosePositionAsync(string keyId, string secretKey, string asset_id)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("APCA-API-KEY-ID", keyId);
                client.DefaultRequestHeaders.Add("APCA-API-SECRET-KEY", secretKey);
                var response = client.DeleteAsync(this.tradingUri + $"positions/{asset_id}" ).Result;
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"HTTP request failed with status code {response.StatusCode}");
                }
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var ConvertedResponse = JsonConvert.DeserializeObject<PositionToReturn>(jsonResponse);
                return ConvertedResponse;
            }

        }

        public async Task<List<AlpacaTransactionDTO>> GetTransactionsAsync(string keyId, string secretKey)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("APCA-API-KEY-ID", keyId);
                client.DefaultRequestHeaders.Add("APCA-API-SECRET-KEY", secretKey);
                var response = client.GetAsync(this.tradingUri + "account/activities").Result;
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"HTTP request failed with status code {response.StatusCode}");
                }
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var ConvertedResponse = JsonConvert.DeserializeObject<List<AlpacaTransactionDTO>>(jsonResponse);

                this.NewTransactionsHandler(ConvertedResponse); 

                return ConvertedResponse;
            }

        }

        private async void NewTransactionsHandler(List<AlpacaTransactionDTO> alpacaTransactions)
        {
            
            var userId = _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var existTransactions = await _alpacaRepository.GetListAsync(Int32.Parse(userId));
            foreach (var transaction in alpacaTransactions)
            {
               var isExist = existTransactions.Find(tr=>tr.Id == transaction.Id);
                if (isExist is null && transaction.Activity_type == "FILL")
                {
                    var transactionToAdd = _mapper.Map<AlpacaTransaction>(transaction);
                    transactionToAdd.User_Id = Int32.Parse(userId);
                    Log.Information("AlpacaTransaction to add: {@tr}", transactionToAdd);
                    var newTransaction = await _alpacaRepository.AddAsync(transactionToAdd);
                    _ = _notificationService.CreateNotificationsFromAlpacaTransaction(newTransaction);
                }
            }
        } 

        public async Task<BarMonthData> GetMonthBarsAsync(string keyId, string secretKey, string symbol)
        {
            using (var client = new HttpClient())
            {
                var end = DateTime.UtcNow.AddDays(-1).ToString("yyyy-MM-dd");
                var start = DateTime.UtcNow.AddDays(-31).ToString("yyyy-MM-dd");

                client.DefaultRequestHeaders.Add("APCA-API-KEY-ID", keyId);
                client.DefaultRequestHeaders.Add("APCA-API-SECRET-KEY", secretKey);
                var response = client.GetAsync(this.dataUri+$"stocks/{symbol}/bars?timeframe=1Day&start=2024-04-01&end=2024-05-07&limit=1000&feed=sip&sort=asc").Result;
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"HTTP request failed with status code {response.StatusCode}");
                }
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var ConvertedResponse = JsonConvert.DeserializeObject<BarMonthData>(jsonResponse);
                return ConvertedResponse;
            }

        }

        public async Task<BarToReturn> GetLastBarAsync(string keyId, string secretKey, string symbol)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("APCA-API-KEY-ID", keyId);
                client.DefaultRequestHeaders.Add("APCA-API-SECRET-KEY", secretKey);
                var response = client.GetAsync(this.dataUri + $"stocks/{symbol}/bars/latest").Result;
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"HTTP request failed with status code {response.StatusCode}");
                }
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var ConvertedResponse = JsonConvert.DeserializeObject<BarToReturn>(jsonResponse);
                return ConvertedResponse;
            }

        }

        public async Task<AlpacaAccount> GetAccountAsync(string keyId, string secretKey)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("APCA-API-KEY-ID", keyId);
                client.DefaultRequestHeaders.Add("APCA-API-SECRET-KEY", secretKey);
                var response = client.GetAsync(this.tradingUri + "account").Result;
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"HTTP request failed with status code {response.StatusCode}");
                }
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var ConvertedResponse = JsonConvert.DeserializeObject<AlpacaAccount>(jsonResponse);
                return ConvertedResponse;
            }

        }

        public async Task<List<OrderToReturn>> GetOrdersAsync(string keyId, string secretKey)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("APCA-API-KEY-ID", keyId);
                client.DefaultRequestHeaders.Add("APCA-API-SECRET-KEY", secretKey);
                var response = client.GetAsync(this.tradingUri + "orders").Result;
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"HTTP request failed with status code {response.StatusCode}");
                }
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var ConvertedResponse = JsonConvert.DeserializeObject<List<OrderToReturn>>(jsonResponse);
                return ConvertedResponse;
            }

        }

        public async Task<OrderToReturn> CreateOrdersAsync(string keyId, string secretKey, OrderDTO orderToCreate)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("APCA-API-KEY-ID", keyId);
                client.DefaultRequestHeaders.Add("APCA-API-SECRET-KEY", secretKey);
                var orderJSON = JsonConvert.SerializeObject(orderToCreate);
                var payload = new StringContent(orderJSON, Encoding.UTF8, "application/json");  
                var response = client.PostAsync(this.tradingUri + "orders", payload).Result;
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"HTTP request failed with status code {response.StatusCode}");
                }
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var ConvertedResponse = JsonConvert.DeserializeObject<OrderToReturn>(jsonResponse);
                return ConvertedResponse;
            }

        }

        public async Task<TradeToReturn> GetTradesAsync(string keyId, string secretKey, string symbol)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("APCA-API-KEY-ID", keyId);
                client.DefaultRequestHeaders.Add("APCA-API-SECRET-KEY", secretKey);
               var response = client.GetAsync(this.dataUri + $"stocks/{symbol}/trades/latest").Result;
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"HTTP request failed with status code {response.StatusCode}");
                }
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var ConvertedResponse = JsonConvert.DeserializeObject<TradeToReturn>(jsonResponse);
                return ConvertedResponse;
            }

        }

    }
}