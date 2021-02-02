# Kryptomania

## Overview

Kryptomania is semester project for the school subject [Web development](https://vzdelavanie.uniza.sk/vzdelavanie/planinfo.php?kod=274878&lng=sk "Web development").
The aim of the work was to create web application (frontend and backend), that allow you to check price, market share, exchanges of cryptocurrencies. \
Registered user has the option to save favourite cryptocurrency to own portfolio.

#### Technologies used
* backend : [ASP.NET MVC (5)](https://dotnet.microsoft.com/apps/aspnet/mvc)
* frontend : HTML, CSS, Blazor(mainly), JavaScript(partly)
* database : [PostgreSQL](https://www.postgresql.org/) used through [EntityFramework](https://docs.microsoft.com/en-us/ef/)
* crypto data API : [CoinGecko API](https://www.coingecko.com/en/api)
* libraries : [Chart.JS](https://www.chartjs.org/), [Npgsql](https://www.npgsql.org/)
## Description

#### Account management
	The application allows registration and login of users.
	The guest has the opportunity to view the current prices of cryptocurrencies, and exchange data.
	The logged-in user has the option to store favorite currencies in his own portfolio.
	If the user has the **Admin Role**, he has the ability to manage cryptocurrencies, change their data, delete and add new ones.

##### Cryptocurrency page (Admin view)
	This page contains a list of cryptocurrencies along with their additional information, such as price, market share, changes in the last 24 hours, number of tokens...
	The data are stored in a local database (PostgreSQL). Admin has the ability to change this data, or load the current accurate data from the CoinGecko API.
	The logged-in user has the option to add his favorite currency to portfolio.
![](https://raw.githubusercontent.com/GabrielHalvonik/SemestralkaFinalVAII/main/manual_res/crypto.png)

##### Portfolio page (User view)
	This page is accessible only to logged in users.
	The user has the ability to view his favorite cryptocurrencies, or remove them from the list of favorites.
  	The list is stored in the PostgreSQL database, where each logged in user has his own list.
![](https://raw.githubusercontent.com/GabrielHalvonik/SemestralkaFinalVAII/main/manual_res/portfolio.png)

##### Excahnges page (Host view)
	The page contains a list of exchanges with detailed information.
	The data are stored in a PostgreSQL database. Admin has the ability to change this data, or load the current accurate data from the CoinGecko API.
![](https://raw.githubusercontent.com/GabrielHalvonik/SemestralkaFinalVAII/main/manual_res/excanges.png)

##### Crypo chart page (Admin view)
	The page contains a chart with historical data(price) of the currently selected cryptocurrency.
![](https://raw.githubusercontent.com/GabrielHalvonik/SemestralkaFinalVAII/main/manual_res/charts.png)
