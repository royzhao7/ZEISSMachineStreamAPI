using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.WebSockets;
using System.Text;

namespace ZEISSMachineStreamAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StreamController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<StreamResult>> Get()
        {
            try
            {
                using var webSocket = new ClientWebSocket();
                return await Echo(webSocket);
            }catch (Exception)
            {
                return BadRequest();
            }
        }


        private  async Task<StreamResult> Echo(ClientWebSocket webSocket)
        {

            var buffer = new byte[1024 * 4];
            await webSocket.ConnectAsync(new Uri("ws://machinestream.herokuapp.com/ws"), CancellationToken.None);
            var receiveResult = await webSocket.ReceiveAsync(
                new ArraySegment<byte>(buffer), CancellationToken.None);

            var json = Encoding.UTF8.GetString(buffer, 0, receiveResult.Count);
            var streamResult=JsonConvert.DeserializeObject<StreamResult>(json);


            await webSocket.CloseAsync(
                WebSocketCloseStatus.NormalClosure,
                receiveResult.CloseStatusDescription,
                CancellationToken.None);


            return streamResult;
        }
    }
}
