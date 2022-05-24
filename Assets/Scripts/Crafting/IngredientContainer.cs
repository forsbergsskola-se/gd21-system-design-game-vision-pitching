using UnityEngine;

namespace Crafting
{
    public class IngredientContainer : MonoBehaviour
    {
        [SerializeField] private Ingredient ingredient;
        public Ingredient Ingredient => ingredient;
    }
}