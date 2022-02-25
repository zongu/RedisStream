# RedisStream

* RedisStream: RedisStream工具
    * Applibs
        * RedisStreamConsumer: Stream消費者，廣播用途
        * RedisStreamDispatcher: Stream事件調度員
        * RedisStreamFactory: Stream運作中心
        * RedisStreamGroupConsumer: StreamGroup消費者，群組調配訊息用
        * RedisStreamProducer: Stream事件生產者
    * Model
        * EventStream: 事件基底
        * IPubSubDispatcher: 調度員介面
        * IPubSubHandler: 事件處理介面
        * RedisStreamEventStream: Redis Stream事件
* RedisStream.Ap: 範例程式