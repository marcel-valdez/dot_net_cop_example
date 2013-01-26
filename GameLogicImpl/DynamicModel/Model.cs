namespace Game.Logic.DynamicModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web.Caching;
    using System.Web;
    internal class Model
    {
        private static ModelCache mCache = new ModelCache();

        public static ModelCache Cache
        {
            get
            {
                return mCache;
            }
        }

        private Model()
        {

        }
    }
}
