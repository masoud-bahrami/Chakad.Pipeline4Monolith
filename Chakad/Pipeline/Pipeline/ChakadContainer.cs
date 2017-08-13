using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace Chakad.Pipeline
{
    class ChakadContainer
    {
        private static ILifetimeScope _autofac;
        private static readonly string _autofacScopeName = "chakad_pipeline";
        
        public static string AutofacScopeName
        {
            get
            {
                return _autofacScopeName;
            }
        }

        public static ILifetimeScope Autofac
        {
            internal set { _autofac = value; }
            get { return _autofac; }
        }
    }
}
