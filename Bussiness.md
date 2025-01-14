here I explain What exsits in this project and Why
---------------------------------------------------



There are  Sector in a Company Named Color Company all of them have a database for themselves and require some data from another sector.
its implementation of a Microservice.
List Of Sectors:
1. Production_Sector
    -DB:production
     -TBL:production_type,production(Count,production_type,Date) 
       publish production

2. Advertisement_Sector
    -DB:advertisement
     -TBL:advertisement(production_id,Date,Text,colorCode) 
     -Publish advertisement
     -consume production\color

3. Prediction_Sector
    -DB:prdiction
     -TBL:color(Date,colorcode) 
    -Publish color

4. Broadcasting_ads_Sector
    -DB:broadcasting_ads
     -TBL:broadcasting(advertisementId,Date,liveat)
    TO many Write
    -consume advertisement

------------------------------------------- Documentation For D.D.D ------------------------------------------------------------------
Domain

Color Company use the application(s) to setup an advertise for (choosen color,and produced product ) and then Broadcast the ad.

Sub-domains

    Production_Sector: This domain is responsible for creating products and publish to message brocker.
    Advertisement_Sector: This domain is responsible for recieve the color and product and create an ads each time that recive a color and publish the ads to message brocker .
    Prediction_Sector: This domain is responsible for choosing a color for now randomly and publish the color to message brocker .
    Broadcasting_ads_Sector: This domain is responsible for just setting record in its database each time an ad is published on the message brocker.
 
