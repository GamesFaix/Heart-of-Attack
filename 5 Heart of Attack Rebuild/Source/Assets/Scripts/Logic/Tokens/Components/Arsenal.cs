using System;
using System.Collections;
using System.Collections.Generic;
using HOA.Abilities;
using HOA.Collections;

namespace HOA.Tokens
{
    public class Arsenal : TokenComponent, IEnumerable<Ability>
    {
        ListSet<Ability> list;

        public Arsenal(Token thisToken)
            : base(thisToken)
        {
            list = new ListSet<Ability>(9);
        }

        public override string ToString() { return ThisToken + "'s Arsenal"; }

        public void Reset()
        {
            foreach (Ability a in list)
                a.Reset();
        }

        public bool HasAbility(string name, out Ability ability)
        {
            ability = null;
            foreach (Ability a in list)
                if (a.Name == name)
                {
                    ability = a;
                    return true;
                }
            return false;
        }

        public bool Replace(Ability oldAb, Ability newAb)
        {
            if (oldAb == null || newAb == null)
                throw new ArgumentNullException();
            else if (!list.Contains(oldAb))
                throw new ArgumentException("Ability not found");
            else
            {
                list.Remove(oldAb);
                list.Add(newAb);
                list.Sort();
                return true;
            }

        }

        public bool Replace(string oldName, Ability newAb)
        {
            Ability oldAb;
            if (HasAbility(oldName, out oldAb))
                return Replace(oldAb, newAb);
            else
                return false;
        }

        public bool Remove(string name)
        {
            Ability a;
            if (HasAbility(name, out a))
            {
                list.Remove(a);
                return true;
            }
            return false;
        }

        public IEnumerator<Ability> GetEnumerator() { return list.GetEnumerator(); }
        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

        public Ability this[int i] { get { return list[i]; } }
        public bool Perform(int i)
        {
            Debug.Log("Not implemented.");
            return false;
        }

    }
}