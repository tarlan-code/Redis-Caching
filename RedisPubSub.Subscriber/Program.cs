using StackExchange.Redis;

ConnectionMultiplexer redis = await ConnectionMultiplexer.ConnectAsync("localhost:1453");

ISubscriber subscriber = redis.GetSubscriber();

////direct
//await subscriber.SubscribeAsync("mychannel", (channel, msg) =>
//{
//    Console.WriteLine(msg);
//});


//pattern maching
await subscriber.SubscribeAsync("mychannel.*", (channel, msg) =>
{
    Console.WriteLine(msg);
});

Console.Read(); 