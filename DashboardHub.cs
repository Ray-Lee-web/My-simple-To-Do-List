using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace WebApplication1.Hubs
{
    public class DashboardHub : Hub
    {
        public void Send(string message)
        {
            // Call the addMessage method to update clients.
            Clients.All.addMessage(message);
        }
        public void UpdateTaskStatus(int taskId, string status)
        {
            Clients.All.updateTaskStatus(taskId, status);
        }
    }
}