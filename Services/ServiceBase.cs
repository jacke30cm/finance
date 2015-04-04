using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Services
{
    public abstract class ServiceBase
    {
        internal DataWorker uow;

        protected ServiceBase()
        {

            uow = new DataWorker(); 

        }

    }

}
