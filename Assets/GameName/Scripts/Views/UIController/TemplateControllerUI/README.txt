This UI Will Receive Input, send to its Controller, receive infor and show to Screen
This should implement MonoBehaviour or StateMachine to decide how it interact with the Controller
If this UI may just exist one on the whole scene, it should implement SingletonMono
If this UI may exist many exist many instances, for example, child item on a table, do not implement SingletonMono