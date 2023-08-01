using ERP.Models.ResponseModels;

namespace ERP.Interfaces.IServices
{
    public interface IDashboardService
    {
        DashboardResponseModel GetDashboardData();
    }
}
