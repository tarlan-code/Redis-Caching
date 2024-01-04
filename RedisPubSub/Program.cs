
using StackExchange.Redis;

ConnectionMultiplexer redis = await ConnectionMultiplexer.ConnectAsync("localhost:1453");

ISubscriber publisher = redis.GetSubscriber();

////direct
//while (true)
//{
//    Console.Write("Mesaj:");

//    string mesaj = Console.ReadLine();

//    await publisher.PublishAsync("mychannel", mesaj); 
//}


//pattern maching
while (true)
{
    Console.Write("Mesaj:");

    string mesaj = Console.ReadLine();

    await publisher.PublishAsync("mychannel.google", mesaj);
}