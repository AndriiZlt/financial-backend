using aspnetcore.ntier.DAL.Entities;
using aspnetcore.ntier.DTO.DTOs;

namespace aspnetcore.ntier.BLL.Services.IServices
{
    public class Draft
    {
        public string data { get; set; }
        public string type { get; set; }
        public DateTime date { get; set; }
    }
    public interface IAlpacaService
    {
        Task<List<AssetToReturn>> GetAssetsAsync(string keyId, string secretKey);
        Task<AssetToReturn> GetAssetAsync(string keyId, string secretKey, string assetId);
        Task<List<PositionToReturn>> GetPositionsAsync(string keyId, string secretKey);
        Task<List<TransactionToReturn>> GetTransactionsAsync(string keyId, string secretKey);
        Task<BarMonthData> GetMonthBarsAsync(string keyId, string secretKey, string symbol);
        Task<AlpacaAccount> GetAccountAsync(string keyId, string secretKey);
        Task<List<OrderToReturn>> GetOrdersAsync(string keyId, string secretKey);
        Task<OrderToReturn> CreateOrdersAsync(string keyId, string secretKey, OrderDTO order);
        Task<PositionToReturn> ClosePositionAsync(string keyId, string secretKey, string asset_id);
        Task<TradeToReturn> GetTradesAsync(string keyId, string secretKey, string symbol);
        Task<BarToReturn> GetLastBarAsync(string keyId, string secretKey , string symbol);


    }
}