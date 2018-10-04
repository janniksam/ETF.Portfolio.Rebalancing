using ETF.Portfolio.Rebalancing.Data;

namespace ETF.Portfolio.Rebalancing.Rebalancing
{
    public interface IRebalancer
    {
        RebalanceResult Rebalance(RebalanceInput input);
    }
}