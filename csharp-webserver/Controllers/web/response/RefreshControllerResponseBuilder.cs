using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace csharp_webserver
{
    public class RefreshControllerResponseBuilder : AbstractResponseBuilder
    {

        public RefreshControllerResponseBuilder():base()
        {

        }

        public override string getResponse()
        {

            return Global
                .messageStoreService
                    .changedSinceLastRequest();
        }
    }
}