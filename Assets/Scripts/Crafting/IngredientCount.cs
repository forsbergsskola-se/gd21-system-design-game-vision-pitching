using System;
using UnityEngine;

namespace Crafting
{
    [Serializable]
    public class IngredientCount
    {
        [SerializeField] private Ingredient ingredient;
        [SerializeField] private int count;

        public Ingredient Ingredient => ingredient;
        public int Count => count;
    }
}