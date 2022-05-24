using UnityEngine;

namespace Crafting
{
    [CreateAssetMenu(menuName = "Crafting/Recipe", fileName = "New Recipe")]
    public class Recipe : ScriptableObject
    {
        [SerializeField] private IngredientCount[] ingredientCounts;
        [SerializeField] private GameObject output; //Leaving output as a GameObject for now, so we can output anything when crafting.

        public IngredientCount[] IngredientCounts => ingredientCounts;
        public GameObject Output => output;
    }
}