# ETF Portfolio Rebalancing

[![Build status master](https://ci.appveyor.com/api/projects/status/b982ewnsagvbyd5i?svg=true&passingText=master%20-%20passing&failingText=master%20-%20failing&pendingText=master%20-%20pending)](https://ci.appveyor.com/project/janniksam/kinoheld) 
[![Build status dev](https://ci.appveyor.com/api/projects/status/b982ewnsagvbyd5i/branch/dev?svg=true&passingText=dev%20-%20passing&failingText=dev%20-%20failing&pendingText=dev%20-%20pending)](https://ci.appveyor.com/project/janniksam/kinoheld/branch/dev)
[![NuGet version](https://badge.fury.io/nu/Kinoheld.Api.Client.svg)](https://badge.fury.io/nu/Kinoheld.Api.Client)

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

var input = new RebalanceInput(1000, 2, portfolio);         var algo = new FastButIntelligentAlgo();
var result = algo.Rebalance(input);
```