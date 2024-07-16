using System;
using System.Collections.Generic;
using Logic.Entities;

namespace Infrastructure.Data
{
    [Serializable]
    public class PermaDeck
    {
        public List<Card> Cards = new();
    }
}