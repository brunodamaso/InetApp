using System.Collections.Generic;
using System.Text;
using INetApp.APIWebServices.Responses;
//using Newtonsoft.Json;

namespace INetApp.APIWebServices.Entity
{
    public class OptionsEntity : Response
    {
        public List<OptionEntity> OptionsEntities;
    }
}