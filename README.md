# NotifyNativeListener

The core of the project lies in the API: [Notification listener](https://learn.microsoft.com/en-us/windows/apps/design/shell/tiles-and-notifications/notification-listener). Based on limited research, only WPF development facilitates the aforementioned operations. Therefore, this project is only a demo.

该项目的核心在于 Api： [Notification listener](https://learn.microsoft.com/en-us/windows/apps/design/shell/tiles-and-notifications/notification-listener)

据有限查证，仅有 WPF 的开发便于进行上述操作，正因如此，该项目仅为demo。


The demo demonstrates intercepting system notifications from QQNt, which has enabled system notifications. If interception is not required, the corresponding notification can be left uncleared. For message forwarding, it is achieved through the use of websocket.client.

该 demo 实现了对启用了系统通知的 QQNt 的系统通知拦截（若不需拦截，不清除对应的通知即可），并结合开发效率及个人需求，对消息的转发通过 websocket.client 的形式。


The purpose is solely to obtain new information from applications like QQ without interfering with the source program. Considering the protection measures of the source program, it is possible to achieve this by obtaining or directly intercepting system push service information. This demo is only suitable for Windows. If you wish to achieve the same or stronger effects on other platforms or in different ways on Windows, you can refer to [this issue](https://github.com/Mrs4s/go-cqhttp/issues/2471).

仅希望获取QQ等应用的新信息及并不希望介入对源程序的操作，考虑源程序的保护措施，均可以通过获取或直接拦截系统推送服务信息的形式实现。本demo仅适用于windows，若希望在他端或在windows以其他方式实现相同或更强的效果，可以参考 [该issue](https://github.com/Mrs4s/go-cqhttp/issues/2471) 。


The implementation path mentioned has its drawbacks. On one hand, applications like QQNt provide very limited information through the system push service, and the structure and semantics of this information are particularly challenging to analyze directly due to the reuse of push service calls. On the other hand, the effectiveness of this implementation path heavily relies on the support and implementation of system notifications by these applications. If you have other requirements or need more precise results, it is not recommended to use this implementation path.

该实现路径的缺点是，一方面 QQNt 等应用对系统推送服务仅提供及其有限的信息，并因为推送服务的调用复用，其信息的结构语义尤其难以直接分析；另一方面，该路径的实现效果强依赖于这些应用对系统通知的支持及其实现方式。若有其他需求或对精确度的要求不那么粗糙，及其不建议通过该实现路径实现。


The demo uses the websocket library [websocket-sharp](https://github.com/sta/websocket-sharp) for establishing a websocket connection.

该 demo 使用了websocket库 [websocket-sharp](https://github.com/sta/websocket-sharp) 进行websocket连接。








