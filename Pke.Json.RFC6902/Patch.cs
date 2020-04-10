using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Pke.Json.RFC6902
{
    public class Patch : IEnumerable<Operation>, IPatch
    {
        public Patch(IEnumerable<Operation> ops)
        {

        }

        public Patch(JToken from, JToken to)
        {
            
        }

        public JToken Apply(JToken to)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<Operation> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
