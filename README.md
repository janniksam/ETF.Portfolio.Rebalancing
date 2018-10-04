# ETF Portfolio Rebalancing

[![Build status master](https://ci.appveyor.com/api/projects/status/x517htpkhgcx2dqv?svg=true&passingText=master%20-%20passing&failingText=master%20-%20failing&pendingText=master%20-%20pending)](https://ci.appveyor.com/project/janniksam/etf-portfolio-rebalancing) 
[![Build status dev](https://ci.appveyor.com/api/projects/status/x517htpkhgcx2dqv/branch/dev?svg=true&passingText=dev%20-%20passing&failingText=dev%20-%20failing&pendingText=dev%20-%20pending)](https://ci.appveyor.com/project/janniksam/etf-portfolio-rebalancing/branch/dev)
[![NuGet version](https://badge.fury.io/nu/ETF.Portfolio.Rebalancing.svg)](https://badge.fury.io/nu/ETF.Portfolio.Rebalancing)

## Description

Collection of portfolio rebalance algorythms.

## Basis usage:
 
### Fast and intelligent algorythm

```cs
var portfolio = new List<ETFItem>
{
    new ETFItem("ISIN_1", 0, 10.0m, 0.2m),
    new ETFItem("ISIN_2", 0, 20.0m, 0.8m),
};

var input = new RebalanceInput(1000, 2, portfolio);
var algo = new FastButIntelligentAlgo();
var result = algo.Rebalance(input);
foreach(var order in result.Orders)
{
   Console.WriteLine($"You should {order.OrderAmount}x {order.ISIN} for {order.CurrentPrice*order.OrderAmount:C}");
}
```
