using System;
using System.Collections.Generic;
using System.IO;
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

  private static IEnumerable<Tuple<byte[], int>> PrepareMessage(string fileId, FileStream fileData)
  {
    long dataLength = fileData.Length;
    int maxChunks = (int)Math.Ceiling((double)dataLength / Standards.FrameSize);
    byte[] delimiter = Encoding.UTF8.GetBytes(Standards.Delimiter);
    byte[] data = new byte[Standards.FrameSize];
    
    for (int chunkIndex = 1; chunkIndex <= maxChunks; chunkIndex++) {
      int previouslyReadCount = 0;
      var header = new
      { fileId,
        chunkIndex,
        maxChunks, };

      string jsonHeader = JsonSerializer.Serialize(header);
      byte[] byteHeader = new byte[Encoding.ASCII.GetBytes(jsonHeader).Length + delimiter.Length];
      Buffer.BlockCopy(Encoding.ASCII.GetBytes(jsonHeader), 0, byteHeader, 0, byteHeader.Length - delimiter.Length);

      Buffer.BlockCopy(delimiter, 0, byteHeader, byteHeader.Length - delimiter.Length, delimiter.Length);

      byte[] buffer = new byte[Standards.FrameSize - byteHeader.Length];
      Buffer.BlockCopy(byteHeader, 0, data, 0, byteHeader.Length);
      previouslyReadCount = fileData.Read(buffer, 0, data.Length - byteHeader.Length);
      Buffer.BlockCopy(buffer, 0, data, byteHeader.Length, buffer.Length);
      yield return new Tuple<byte[], int>(data, previouslyReadCount);
      Array.Clear(data);
    }
  }

  private static async Task SendMessage(ClientWebSocket webSocket, byte[] data)
  {
    await webSocket.SendAsync(new ArraySegment<byte>(data, 0, data.Length), WebSocketMessageType.Binary, false, CancellationToken.None);
  }
}