# PredictMarketScore-dotnetcore
 
This project is developed in .net core 8.0, since i have visual studio and .net project set up with me already. The similar project can be developed in React and Node js. PredictPriceWeb is the UI project. WebApi is the web api project. Visula studio 2022 is used to develop this application.

PredictMarketScore-dotnetcore\wwwroot\js --> path for predictscore.js code

UI --> http://localhost:5204/ API --> 1. https://localhost:7077/api/PredictScore/GetDataPoints --> to get 10 consecutive random data points 2. https://localhost:7077/api/PredictScore/GetPredictionScore --> returns 3 days predictions

API calls are made using ajax call in predictscore.js. Sample excel file used for testing
