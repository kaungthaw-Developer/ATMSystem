namespace ATMSystem.Models
{
    public class CashHistoryResponseModel
    {
        public PageSettingModel PageSettingModel { get; set; }
        public List<UserHistoryModel> userHistories { get; set; }
    }
    public class PageSettingModel
    {
        public PageSettingModel()
        {
        }

        public PageSettingModel(int pageNo, int pageSize, int pageCount, string pageUrl)
        {
            PageNo = pageNo;
            PageSize = pageSize;
            PageCount = pageCount;
            PageUrl = pageUrl;
        }

        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public string PageUrl { get; set; }
    }
}
