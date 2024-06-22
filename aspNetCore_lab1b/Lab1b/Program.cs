using System.Diagnostics;
using System.Net.WebSockets;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        app.MapGet("/", async (context) =>
        {
            context.Response.ContentType = "text/html; charset=utf-8";
            await context.Response.SendFileAsync("html/index.html");
        });

        app.UseWebSockets();

        app.MapGet("/testws", async (HttpContext context) =>
        {
            Thread hSendCycle = new Thread(new System.Threading.ParameterizedThreadStart(SendCycle));

            Thread hReciveCycle = new Thread(new System.Threading.ParameterizedThreadStart(ReceiveCycle));
            byte[] buffer = new byte[4096];
            if (context.WebSockets.IsWebSocketRequest)                                                                     
            {
                WebSocket ws = await context.WebSockets.AcceptWebSocketAsync();
                WebSocketReceiveResult result = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                string message = System.Text.Encoding.UTF8.GetString(buffer, 0, result.Count);
                hSendCycle.Start(ws);
                hReciveCycle.Start(ws);

                hSendCycle.Join();
                hReciveCycle.Join();
            }
        });

        app.Run();
    }

    public static ParameterizedThreadStart SendCycle = async (Object? ws) =>
    {
        int k = 0;
        WebSocket? xws = (WebSocket?)ws;
        byte[] buffer = new byte[4096];
        while (xws != null && xws.State == WebSocketState.Open)
        {
            var currentTime = DateTime.Now.ToString("HH:mm:ss");
            buffer = Encoding.ASCII.GetBytes(string.Format("[Send Cycle: {0}], [Current Time: {1}]", ++k, currentTime));
            await xws.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
            Thread.Sleep(1000);
        }
    };


    public static ParameterizedThreadStart ReceiveCycle = async (Object? ws) =>
    {
        int k = 0;
        WebSocket? xws = (WebSocket?)ws;
        byte[] buffer = new byte[4096];
        string message = string.Empty;
        Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
        Trace.AutoFlush = true;                                                                                             
        Trace.WriteLine("[Start Trace]");
        while (xws != null && xws.State == WebSocketState.Open)
        {
            WebSocketReceiveResult result = await xws.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            message = System.Text.Encoding.UTF8.GetString(buffer, 0, result.Count);
            Trace.WriteLine(message);
        }
        Trace.WriteLine("Finish Trace");
    };
}