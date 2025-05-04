This is A Project for presenting EDA 
---------------------------------------------------
Event-Driven Architecture (EDA) is a software design pattern that uses events to trigger and communicate between different parts of a system. It involves producers that generate events, a broker or event mesh that delivers those events, and consumers that react to them. This architecture promotes loose coupling between components, allowing them to operate independently and communicate asynchronously via events. 
Key Concepts:

Events: Represent significant changes or actions within a system, such as a user clicking a button, a transaction being completed, or an item being added to a cart.(In this Project some other subject)
Producers: Applications or services that generate and publish events. 
Consumers: Applications or services that listen for and react to events. 
Event Broker/Event Mesh: A messaging-oriented middleware that facilitates the delivery of events from producers to consumers. 
Loose Coupling: Components in an EDA are designed to be independent, with communication happening through events rather than direct dependencies. 

How it works:

    A producer generates an event and publishes it to an event broker or event mesh.
    The event broker distributes the event to all interested consumers.
    Consumers subscribe to specific event types and receive events that match their subscriptions.
    Consumers process the received events and take appropriate actions, potentially triggering further events. 

Benefits of EDA:

    Scalability:
    Components can be added or removed without affecting the rest of the system, making it easier to scale. 

Real-time Responsiveness:
Events allow for immediate reactions to changes within the system, enabling real-time processing. 
Flexibility:
EDA provides flexibility in how systems are designed and can be adapted to changing requirements. 
Decoupling:
Loose coupling between components makes it easier to maintain and evolve the system. 
Fault Tolerance:
If a component fails, the rest of the system can continue to function, as long as the event broker is robust. 
---------------------------------------------------
Detail related To this project:

There are  Sectors in a Company Named Color Company, each of them have a database for themselves(just in this case) and require to have be notified of changes of data in another sector.

it is required implementation of a Microservice.
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

3. Prediction_Sector(Not in this repo)
    -DB:prdiction
     -TBL:color(Date,colorcode) 
    -Publish color

4. Broadcasting_ads_Sector(Not in this repo)
    -DB:broadcasting_ads
     -TBL:broadcasting(advertisementId,Date,liveat)
    TO many Write
    -consume advertisement



SO for Example:if in the Production Comany an Item is created then the Advertisement Comapny must be notified so it can make an advertisement based on that as well as adding the product in their DB.

so in this scenario >
1.Production Company is a Publisher to kafka ,which means everytime it creates an production item it would call kafka producer 
2.Advertisement Compnay is a Cosumer from kafka
3.Kafka is a event broker(I use the image here ) [Kafka Image ](/kafka-broker/docker-compose.yml)


there is a KafkaProducerService in the Production_Company as 
[The kafka related services ](/Production_Company/Kafka/KafkaProducerService.cs)
Here the TopicName , the server are defined and message that would be broadcasted is defined,


in the Prodoction_Company there is a Fake Scheduler(Just to run the Create there is no other reason to have this)
there is an adding production function that happen in this Sceduler 
[The Scheduler](/Production_Company/BackgroundServices/ProductionSchedulerService.cs)
after the Adding production, the Kafka is called ( await _kafkaProducer.ProduceEventAsync(production);  )
so the message would be published 

there is KafkaConsumerService in the Advertisement_company as
[The kafka related services](/Advertisement_Company/Kafka/KafkaConsumerService.cs)
here the TopicName,the server that must be used are defined ,this company is subscribed to one type of Topic.and if there is an event in this spesific topic
then the compinh message would be Deserialize 
then the production item that is created in Production Company(and Come via Kafka Event) would be added in Production table of the Advertisement Company database
then a new record for advertisement would be added to the table of advertisement of Advertisement Company database

and thats the Whole Story behind EDA of this REPO


 
