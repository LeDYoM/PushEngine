using System;
using System.Collections.Generic;

namespace PushEngine
{
    public class ModelProperties
    {
        private PENamedPropertyList defaults = new PENamedPropertyList();

        public ModelProperties(PENamedPropertyList defaults_)
        {
            defaults = defaults_;
        }

        public void AddDefaults(PENamedPropertyList moreDefaults_)
        {
            foreach (PENamedProperty pr in moreDefaults_)
            {
                if (!set(pr))
                {
                    defaults.Add(pr);
                }
            }
        }

        public PENamedPropertyList getList()
        {
            return defaults;
        }

        public bool set(PENamedProperty pr_)
        {
            if (defaults.ContainsKey(pr_.name))
            {
                defaults.setValue(pr_.name, pr_.property);
                return true;
            }

            return false;
        }

        public int setList(PENamedPropertyList prList_)
        {
            int count = 0;
            foreach (PENamedProperty pr_ in prList_)
            {
                if (set(pr_))
                    count++;
            }
            return count;
        }
    }
}
