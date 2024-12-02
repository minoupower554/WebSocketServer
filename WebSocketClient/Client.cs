using System;
using WebSocketStandards;
using System.Net.WebSockets;
using System.Text.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebSocketClient;

public static class Program
{
  public static async Task Main()
  {
    Uri uri = new("ws://localhost:8080");
    ClientWebSocket webSocket = new();
    await webSocket.ConnectAsync(uri, CancellationToken.None);
  }

  private static byte[] PrepareMessage(string fileId, int chunkIndex, int maxChunks, byte[] chunkData)
  {
    var header = new
    { fileId,
      chunkIndex,
      maxChunks, };

    string jsonHeader = JsonSerializer.Serialize(header);
    byte[] byteHeader = Encoding.UTF8.GetBytes(jsonHeader);
    
    
  }

  private static async Task SendMessage(ClientWebSocket webSocket, byte[] data)
  { }
}