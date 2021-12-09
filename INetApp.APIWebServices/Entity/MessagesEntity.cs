using System.Collections.Generic;
using INetApp.APIWebServices.Responses;

namespace INetApp.APIWebServices.Entity
{
    public class MessagesEntity : Response
    {
        public List<MessageEntity> MessagesEntities;
    }
}