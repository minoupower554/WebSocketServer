using System;
using System.Collections.Generic;
using System.IO;
using WebSocketStandards;
using System.Net;
using System.Net.WebSockets;
using System.Text.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace WebSockerServer;

class Program {
    private static async Task Main() {
        HttpListener listener = new();
        listener.Prefixes.Add("http://localhost:8080/");
        listener.Start();
        HttpListenerContext httpContext =  await listener.GetContextAsync();
        if (httpContext.Request.IsWebSocketRequest) {
            HttpListenerWebSocketContext context = await httpContext.AcceptWebSocketAsync(null);
        } else {
            httpContext.Response.StatusCode = 426;
            httpContext.Response.ContentEncoding = Encoding.ASCII;
            httpContext.Response.Headers.Add(HttpRequestHeader.Upgrade, "websocket");
            httpContext.Response.Headers.Add()
        }
    }
}