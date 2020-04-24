using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using ScrumProject.Models;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace ScrumProject
{
    [HubName("ChatHub")]
    public class ChatHub : Hub
    {
        BlogDbContext db = new BlogDbContext();
        Message m = new Message();
        User u = new User();
        Profile p = new Profile();
        public override System.Threading.Tasks.Task OnConnected()
        {
            Clients.All.user(Context.User.Identity.Name);
            return base.OnConnected();
        }
        public void send(string message)
        {
            var datum = DateTime.UtcNow;
            Clients.Caller.message("<strong>You</strong> <i>(" + datum + ")</i> <br />" + message + "<hr />");
            Clients.Others.message("<strong>" + Context.User.Identity.Name + "</strong><i> (" + datum + ")</i> <br />" + message + "<hr />"); 

            m.ProfileID = Context.User.Identity.GetUserId();
            m.Text = message;
            m.Date = datum;
            db.Messages.Add(m);
            db.SaveChanges();

        }
    }
}
