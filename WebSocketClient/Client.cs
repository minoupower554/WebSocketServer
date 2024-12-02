using System;
using WebSocketStandards;
using System.Net.WebSockets;
using System.Text.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics.Tracing;
namespace WebSocketClient;

public class Program {
    public static async Task Main() {
        Uri URI = new("ws://localhost:8080");
        ClientWebSocket webSocket = new();
        await webSocket.ConnectAsync(URI, CancellationToken.None);
        
    }

    private static async Task SendMessage(ClientWebSocket webSocket, string fileId, int chunkIndex, int maxChunks, byte[] chunkData) {
        var header = new {
            fileId,
            chunkIndex,
            maxChunks,
        };

        var jsonHeader = JsonSerializer.Serialize(header);
        var byteHeader = Encoding.UTF8.GetBytes(jsonHeader);

        
    }
}
